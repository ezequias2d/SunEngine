// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/. --%>
using SunEngine.Core;
using SunEngine.Core.Components;
using SunEngine.Graphics;
using System;
using System.Numerics;

namespace SunEngine.SolarSystem
{
    public sealed class PlanetObject : IComponent
    {
        /// <summary>
        /// Julian Ephemeris Data in J2000
        /// </summary>
        private double T { get; set; }
        private double A { get; set; }
        private double E { get; set; }
        private double I { get; set; }
        private double L { get; set; }
        private double OmegaDash { get; set; }
        private double Omega { get; set; }

        /// <summary>
        /// Argument of perihelion
        /// </summary>
        private double LittleOmega { get; set; }

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

        public PlanetObject(string name, World world, Model sphere, Shader shader, string projectionViewBindingName, string modelBindingName, params GpuTexture[] textures)
        {
            Time = CurrentUnixTime();

            var gameObject = world.New(name, "Planet");
            world.Attach(this, gameObject);

            ModelComponent modelComponent = new ModelComponent(sphere, shader, projectionViewBindingName, modelBindingName)
            {
                Textures = textures
            };

            world.Attach(modelComponent, gameObject);
            Scale = 1;
            AxialTilt = 0;
            RotationHours = 1;
        }
        

        public float Scale { get; set; }
        public Vector3 Center { get; set; }

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
        /// Upper Omega
        /// </summary>
        public (double value, double dot) AscendingLongidute { get; set; }

        public float RotationHours { get; set; }
        public float AxialTilt { get; set; }

        public void Update(GameObject sender, ElapsedTimeEventArgs e)
        {
            // 525600 => 1 year per 1 minute.
            Time += e.Time * 525600;
            Vector3 position = ComputeCoordinates();
            
            sender.Position = position + Center;
            sender.Scale = Vector3.One * Scale;
            

            (Vector3 axis, Quaternion rotation) = GetAxialTiltVector(AxialTilt);
            double daytime = (Time / (RotationHours * 3600) + 0.125) % 1;
            sender.Rotation = Quaternion.Concatenate(rotation, Quaternion.CreateFromAxisAngle(axis, (float)(daytime * 360.0* SunMath.Deg2RadD)));
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

        private void UpdateParameters()
        {
            // ref: https://ssd.jpl.nasa.gov/txt/aprx_pos_planets.pdf

            T = ToJ2000(ToJulianEphemerisDate(Time));

            A = SemiMajorAxis.value + SemiMajorAxis.dot * T;
            E = Eccentricity.value + Eccentricity.dot * T;
            I = Inclination.value + Inclination.dot * T;
            L = MeanLogitude.value + MeanLogitude.dot * T;
            OmegaDash = PerihelionLongidute.value + PerihelionLongidute.dot * T;
            Omega = AscendingLongidute.value + AscendingLongidute.dot * T;

            LittleOmega = OmegaDash - Omega;
            M = L - OmegaDash;

            BiggerE = SoluteKeperEquation2();

            R = ComputePlanetHeliocentricCoordinates();
        }

        private Vector3 ComputeCoordinates()
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

            return new Vector3((float)y, (float)z, (float)x);
        }

        private (Vector3, Quaternion) GetAxialTiltVector(float angle)
        {
            Quaternion rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitZ, angle * SunMath.Deg2RadF);
            return (Vector3.Transform(Vector3.UnitY, rotation), rotation);
        }
    }
}
