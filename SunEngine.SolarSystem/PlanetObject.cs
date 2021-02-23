// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using SunEngine.Core;
using SunEngine.Core.Components;
using SunEngine.Graphics;
using System;
using System.Numerics;

namespace SunEngine.SolarSystem
{
    public class PlanetObject : IComponent
    {
        /// <summary>
        /// Julian Ephemeris Data in J2000
        /// </summary>
        protected double T { get; set; }

        /// <summary>
        /// Current semi-major axis.
        /// </summary>
        protected double A { get; set; }
        /// <summary>
        /// Current eccentricity;
        /// </summary>
        protected double E { get; set; }
        /// <summary>
        /// Current inclination.
        /// </summary>
        protected double I { get; set; }
        /// <summary>
        /// Current mean longitude.
        /// </summary>
        protected double L { get; set; }
        /// <summary>
        /// Current Longitude of perielion.
        /// </summary>
        protected double OmegaDash { get; set; }

        /// <summary>
        /// Current longitude of the ascending node.
        /// </summary>
        protected double Omega { get; set; }

        /// <summary>
        /// Argument of perihelion
        /// </summary>
        public double LittleOmega { get; set; }

        /// <summary>
        /// Mean anomaly
        /// </summary>
        private double M { get; set; }

        /// <summary>
        /// Eccentric anomaly
        /// </summary>
        private double BiggerE { get; set; }

        /// <summary>
        /// Planet's heliocentric coordinates in its orbital plane, r', with the x'-axis aligned from the focus to the perihelion.
        /// </summary>
        private (double X, double Y) R { get; set; }

        public PlanetObject(SolarObject solarObject, World world, Model sphere, Shader shader, string projectionViewBindingName, string modelBindingName, params GpuTexture[] textures)
        {
            Time = CurrentUnixTime();

            SolarObject = solarObject;
            var gameObject = world.New(solarObject.ToString(), "Planet");
            world.Attach(this, gameObject);

            ModelComponent modelComponent = new ModelComponent(sphere, shader, projectionViewBindingName, modelBindingName)
            {
                Textures = textures
            };

            world.Attach(modelComponent, gameObject);
            PlanetSize = 1;
            Scale = 1;
            AxialTilt = 0;
            RotationHours = 1;

            //525600 => 1 year per 1 minute.
            TimeRate = 1;
        }
        
        public SolarObject SolarObject { get; }
        public float PlanetSize { get; set; }
        public float Scale { get; set; }
        public virtual (double X, double Y, double Z) Center { get; set; }

        /// <summary>
        /// Time in seconds.
        /// </summary>
        public double Time { get; set; }

        /// <summary>
        /// A
        /// </summary>
        public (double value, double dot) SemiMajorAxis  { get; set; }

        /// <summary>
        /// E
        /// </summary>
        public (double value, double dot) Eccentricity { get; set; }

        /// <summary>
        /// I
        /// </summary>
        public (double value, double dot) Inclination { get; set; }

        /// <summary>
        /// L
        /// </summary>
        public (double value, double dot) MeanLogitude { get; set; }

        /// <summary>
        /// Omega Dash
        /// </summary>
        public (double value, double dot) PerihelionLongidute { get; set; }

        /// <summary>
        /// Upper Omega(node, n)
        /// </summary>
        public (double value, double dot) AscendingLongidute { get; set; }
        /// <summary>
        /// Argument of perihelion (exclusive with longitude of perihelion and longitude of ascending).
        /// W
        /// </summary>
        public (double value, double dot) ArgumentOfPerihelion { get; set; }
        public (double value, double dot) MeanAnomaly { get; set; }
        public bool EnableArgumentOfPerihelionAndMeanLongitude { get; set; }

        public float RotationHours { get; set; }
        public float AxialTilt { get; set; }

        public double TimeRate { get; set; }

        public (double X, double Y, double Z) Position { get; set; }
        public Quaternion Rotation { get; set; }

        public virtual void Update(GameObject sender, ElapsedTimeEventArgs e)
        {
            Time += e.Time * TimeRate;
            var position = ComputeCoordinates();
            Position = (position.x + Center.X, position.y + Center.Y, position.z + Center.Z);

            sender.Position = (new Vector3((float)Position.X, (float)Position.Y, (float)Position.Z) + sender.Position) / 2f;
            sender.Scale = Vector3.One * Scale * PlanetSize;
            

            (Vector3 axis, Quaternion rotation) = GetAxialTiltVector(AxialTilt);
            double daytime = (Time / (RotationHours * 3600)) % 1;
            Rotation = sender.Rotation = Quaternion.Concatenate(rotation, Quaternion.CreateFromAxisAngle(axis, (float)(daytime * 360.0* SunMath.Deg2RadD)));
        }

        public static double CurrentUnixTime() => (DateTime.UtcNow.Ticks - new DateTime(1970, 1, 1).Ticks) / (double)TimeSpan.TicksPerSecond;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time">Time in unix epoch</param>
        /// <returns></returns>
        private static double ToJulianEphemerisDate(double time)
        {
            return (time / 86400) + 2440587.5;
        }

        private static double ToJ2000(double julianEphemerisDate) =>
            (julianEphemerisDate - 2451545.0) / 36525;

        private (double, double) ComputePlanetHeliocentricCoordinates() =>
            (A * (Math.Cos(SunMath.Deg2RadD * BiggerE) - E),
                A * Math.Sqrt(1 - E * E) * Math.Sin(SunMath.Deg2RadD * BiggerE));

        /// <summary>
        /// Solution of M = E = e* sin E
        /// </summary>
        /// <returns>E</returns>
        private double SoluteKeperEquation2()
        {
            double eStar = SunMath.Rad2DegD * E;

            double EDelta = double.MaxValue;
            double MDelta;
            double En = M + eStar * Math.Sin(SunMath.Deg2RadD * M);

            while(Math.Abs(EDelta) > 0.000001)
            {
                MDelta = M - (En - eStar * Math.Sin(SunMath.Deg2RadD * En));
                EDelta = MDelta / (1.0 - E * Math.Cos(SunMath.Deg2RadD * En));
                En += EDelta;
            }

            return En;
        }

        protected void UpdateParameters()
        {
            // ref: https://ssd.jpl.nasa.gov/txt/aprx_pos_planets.pdf

            T = ToJ2000(ToJulianEphemerisDate(Time));

            A = SemiMajorAxis.value + SemiMajorAxis.dot * T;
            E = Eccentricity.value + Eccentricity.dot * T;
            I = Inclination.value + Inclination.dot * T;
            Omega = AscendingLongidute.value + AscendingLongidute.dot * T;
            if(!EnableArgumentOfPerihelionAndMeanLongitude)
            {
                L = MeanLogitude.value + MeanLogitude.dot * T;
                OmegaDash = PerihelionLongidute.value + PerihelionLongidute.dot * T;
                
                LittleOmega = OmegaDash - Omega;

                M = L - OmegaDash;
            }
            else
            {
                LittleOmega = ArgumentOfPerihelion.value + ArgumentOfPerihelion.dot * T;
                M = MeanAnomaly.value + MeanAnomaly.dot * T;
            }


            BiggerE = SoluteKeperEquation2();

            R = ComputePlanetHeliocentricCoordinates();
        }

        protected (double x, double y, double z) ComputeCoordinates()
        {
            UpdateParameters();
            double littleOmegaCos = Math.Cos(SunMath.Deg2RadD * LittleOmega);
            double littleOmegaSin = Math.Sin(SunMath.Deg2RadD * LittleOmega);

            double omegaCos = Math.Cos(SunMath.Deg2RadD * Omega);
            double omegaSin = Math.Sin(SunMath.Deg2RadD * Omega);

            double iCos = Math.Cos(SunMath.Deg2RadD * I);
            double iSin = Math.Sin(SunMath.Deg2RadD * I);

            double x = (littleOmegaCos * omegaCos - littleOmegaSin * omegaSin * iCos) * R.X + (-littleOmegaSin * omegaCos - littleOmegaCos * omegaSin * iCos) * R.Y;
            double y = (littleOmegaCos * omegaSin + littleOmegaSin * omegaCos * iCos) * R.X + (-littleOmegaSin * omegaSin + littleOmegaCos * omegaCos * iCos) * R.Y;
            double z = (littleOmegaSin * iSin) * R.X + (littleOmegaCos * iSin) * R.Y;

            return (y, z, x);
        }

        protected (Vector3, Quaternion) GetAxialTiltVector(float angle)
        {
            Quaternion rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitZ, angle * SunMath.Deg2RadF);
            return (Vector3.Transform(Vector3.UnitY, rotation), rotation);
        }

        public void LateUpdate(GameObject sender, ElapsedTimeEventArgs e)
        {
            
        }
    }
}
