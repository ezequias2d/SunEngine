// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/. --%>
using SunEngine.Core;
using SunEngine.Data;
using SunEngine.Data.Loader;
using SunEngine.GL;
using SunEngine.Graphics;
using SunEngine.Windowing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace SunEngine.SolarSystem
{
    public class SunSystem
    {
        internal readonly ISunWindow Window;
        internal readonly IGL GL;

        internal World World;

        internal Model Sphere;

        internal Shader DiffuseShader;
        internal Shader DiffuseDarkShader;
        internal Shader SunShader;

        internal GpuTexture MercuryTexture;
        internal GpuTexture VenusTexture;
        internal GpuTexture EarthTexture;
        internal GpuTexture EarthDarkTexture;
        internal GpuTexture MarsTexture;
        internal GpuTexture JupiterTexture;
        internal GpuTexture SaturnTexture;
        internal GpuTexture UranusTexture;
        internal GpuTexture NeptuneTexture;
        internal GpuTexture PlutoTexture;
        internal GpuTexture SunTexture;

        internal PlanetObject Sun;
        internal List<PlanetObject> Planets;

        internal CameraObject CameraObject;
        public SunSystem(ISunWindow window)
        {
            Window = window;
            GL = window.GL;
            Planets = new List<PlanetObject>();
        }

        public void OnLoad(object sender, EventArgs e)
        {
            World = new World(GL, Window.Input, Window.Dimension);
            CameraObject = new CameraObject(World, Window.Dimension.Width / (float)Window.Dimension.Height);

            SunTexture = LoadTexture("Resources/textures/2k_sun.jpg");

            MercuryTexture = LoadTexture("Resources/textures/2k_mercury.jpg");
            VenusTexture = LoadTexture("Resources/textures/2k_venus_surface.jpg");
            EarthTexture = LoadTexture("Resources/textures/2k_earth_daymap.jpg");
            EarthDarkTexture = LoadTexture("Resources/textures/2k_earth_nightmap.jpg");
            MarsTexture = LoadTexture("Resources/textures/2k_mars.jpg");
            JupiterTexture = LoadTexture("Resources/textures/2k_jupiter.jpg");
            SaturnTexture = LoadTexture("Resources/textures/2k_saturn.jpg");
            UranusTexture = LoadTexture("Resources/textures/2k_uranus.jpg");
            NeptuneTexture = LoadTexture("Resources/textures/2k_neptune.jpg");
            PlutoTexture = LoadTexture("Resources/textures/2k_pluto.jpg");

            SunShader = LoadShader("Resources/shaders/diffuse.vert.glsl", "Resources/shaders/sun.frag.glsl");
            DiffuseDarkShader = LoadShader("Resources/shaders/diffuse.vert.glsl", "Resources/shaders/diffuse_dark.frag.glsl");
            DiffuseShader = LoadShader("Resources/shaders/diffuse.vert.glsl", "Resources/shaders/diffuse.frag.glsl");
            Sphere = LoadModel("Resources/models/sphere.dae");

            Planets.Add(new PlanetObject(CameraFocus.Mercury.ToString(), World, Sphere, DiffuseShader, "Matrices", "Model", MercuryTexture)
            {
                Scale = (float)ToAU(2439.7) * 1000,
                SemiMajorAxis = (0.38709927, 0.00000037),
                Eccentricity = (0.20563593, 0.00001906),
                Inclination = (7.00497902, -0.00594749),
                MeanLogitude = (252.2503235, 149472.67411175),
                PerihelionLongidute = (77.45779628, 0.16047689),
                AscendingLongidute = (48.33076593, -0.12534081),
                AxialTilt = 0.03f,
                RotationHours = 1407.6f
            });

            Planets.Add(new PlanetObject(CameraFocus.Venus.ToString(), World, Sphere, DiffuseShader, "Matrices", "Model", VenusTexture)
            {
                Scale = (float)ToAU(6051.8) * 1000,
                SemiMajorAxis = (0.72333566, 0.0000039),
                Eccentricity = (0.00677672, -0.00004107),
                Inclination = (3.39467605, -0.0007889),
                MeanLogitude = (181.9790995, 58517.81538729),
                PerihelionLongidute = (131.60246718, 0.00268329),
                AscendingLongidute = (76.67984255, -0.27769418),
                AxialTilt = 2.64f,
                RotationHours = -5832.6f
            });

            Planets.Add(new PlanetObject(CameraFocus.Earth.ToString(), World, Sphere, DiffuseDarkShader, "Matrices", "Model", EarthTexture, EarthDarkTexture)
            {
                Scale = (float)ToAU(6371.0) * 1000,
                SemiMajorAxis = (1.00000261, 0.00000562),
                Eccentricity = (0.01671123, -0.00004392),
                Inclination = (-0.00001531, -0.01294668),
                MeanLogitude = (100.46457166, 35999.37244981),
                PerihelionLongidute = (102.93768193, 0.32327364),
                AscendingLongidute = (0, 0),
                AxialTilt = 23.44f,
                RotationHours = 23.93f
            });

            Planets.Add(new PlanetObject(CameraFocus.Mars.ToString(), World, Sphere, DiffuseShader, "Matrices", "Model", MarsTexture)
            {
                Scale = (float)ToAU(3389.5) * 1000,
                SemiMajorAxis = (1.52371034, 0.00001847),
                Eccentricity = (0.0933941, 0.00007882),
                Inclination = (1.84969142, -0.00813131),
                MeanLogitude = (-4.55343205, 19140.30268499),
                PerihelionLongidute = (-23.94362959, 0.44441088),
                AscendingLongidute = (49.55953891, -0.29257343),
                AxialTilt = 23.44f,
                RotationHours = 23.93f
            });

            Planets.Add(new PlanetObject(CameraFocus.Jupiter.ToString(), World, Sphere, DiffuseShader, "Matrices", "Model", JupiterTexture)
            {
                Scale = (float)ToAU(96911) * 100,
                SemiMajorAxis = (5.202887, -0.00011607),
                Eccentricity = (0.04838624, -0.00013253),
                Inclination = (1.30439695, -0.00183714),
                MeanLogitude = (34.39644051, 3034.74612775),
                PerihelionLongidute = (14.72847983, 0.21252668),
                AscendingLongidute = (100.47390909, 0.20469106),
                AxialTilt = 3.13f,
                RotationHours = 9.93f,
            });

            Planets.Add(new PlanetObject(CameraFocus.Saturn.ToString(), World, Sphere, DiffuseShader, "Matrices", "Model", SaturnTexture)
            {
                Scale = (float)ToAU(58232) * 100,
                SemiMajorAxis = (9.53667594, -0.00125060),
                Eccentricity = (0.05386179, -0.00050991),
                Inclination = (2.48599187, 0.00193609),
                MeanLogitude = (49.95424423, 1222.49362201),
                PerihelionLongidute = (92.59887831, -0.41897216),
                AscendingLongidute = (113.66242448, -0.28867794),
                AxialTilt = 26.73f,
                RotationHours = 10.56f,
            });

            Planets.Add(new PlanetObject(CameraFocus.Uranus.ToString(), World, Sphere, DiffuseShader, "Matrices", "Model", UranusTexture)
            {
                Scale = (float)ToAU(25362) * 100,
                SemiMajorAxis = (19.18916464, -0.00196176),
                Eccentricity = (0.04725744, -0.00004397),
                Inclination = (0.77263783, -0.00242939),
                MeanLogitude = (313.23810451, 428.48202785),
                PerihelionLongidute = (170.9542763, 0.40805281),
                AscendingLongidute = (74.01692503, 0.04240589),
                AxialTilt = 82.23f,
                RotationHours = -17.24f,
            });

            Planets.Add(new PlanetObject(CameraFocus.Neptune.ToString(), World, Sphere, DiffuseShader, "Matrices", "Model", NeptuneTexture)
            {
                Scale = (float)ToAU(24622) * 100,
                SemiMajorAxis = (30.06992276, 0.00026291),
                Eccentricity = (0.00859048, 0.00005105),
                Inclination = (1.77004347, 0.00035372),
                MeanLogitude = (-55.12002969, 218.45945325),
                PerihelionLongidute = (44.96476227, -0.32241464),
                AscendingLongidute = (131.78422574, -0.00508664),
                AxialTilt = 28.32f,
                RotationHours = 16.11f
            });

            Planets.Add(new PlanetObject(CameraFocus.Pluto.ToString(), World, Sphere, DiffuseShader, "Matrices", "Model", PlutoTexture)
            {
                Scale = (float)ToAU(1188.3) * 100,
                SemiMajorAxis = (39.48211675, -0.00031596),
                Eccentricity = (0.2488273, 0.0000517),
                Inclination = (17.14001206, 0.00004818),
                MeanLogitude = (238.92903833, 145.20780515),
                PerihelionLongidute = (224.06891629, -0.04062942),
                AscendingLongidute = (110.30393684, -0.01183482),
                AxialTilt = 57.47f,
                RotationHours = -153.29f,
            });


            Sun = new PlanetObject("Sun", World, Sphere, SunShader, "Matrices", "Model", SunTexture)
            {
                Scale = (float)ToAU(696340.0) * 10,
                AxialTilt = 7.25f,
                RotationHours = 609.12f
            };
        }

        public void OnUpdate(object sender, ElapsedTimeEventArgs e)
        {
            World.ComponentSubsystem.Update(null, e);
        }

        public void OnRenderFrame(object sender, ElapsedTimeEventArgs e)
        {
            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.DepthTest);

            GL.Clear(ClearMask.ColorBufferBit | ClearMask.DepthBufferBit);

            World.DrawSubsystem.Draw(null, e);

            Window.SwapBuffers();
        }

        private Model LoadModel(string path)
        {
            using (var stream = File.OpenRead(path))           
                return new Model(GL, SunLoader.LoadMesh(stream)[0]);
        }

        private GpuTexture LoadTexture(string path)
        {
            using (var stream = File.OpenRead(path))
            {
                Texture texture = SunLoader.LoadTexture(stream);
                return new GpuTexture(GL, texture, PixelInternalFormat.RGBA8);
            }
        }

        private Shader LoadShader(string vertexPath, string fragmentPath)
        {
            ShaderProgram vertex = LoadShaderProgra(ShaderType.VertexShader, true, vertexPath);
            ShaderProgram fragment = LoadShaderProgra(ShaderType.FragmentShader, true, fragmentPath);
            return new Shader(GL, vertex, fragment);
        }

        private ShaderProgram LoadShaderProgra(ShaderType type, bool isAscii, string path)
        {
            using (var stream = File.OpenRead(path))
                using (var reader = new StreamReader(stream))
                    return new ShaderProgram(type, isAscii, Encoding.ASCII.GetBytes(reader.ReadToEnd()));
        }

        /// <summary>
        /// Kilometers to astronomical unit.
        /// </summary>
        /// <param name="value">Value in kilometers</param>
        /// <returns>AU value.</returns>
        internal static double ToAU(double km) =>
            km / 149597870.7;
    }
}
