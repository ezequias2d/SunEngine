// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
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
using System.Threading.Tasks;

namespace SunEngine.SolarSystem
{
    public class SunSystem
    {
        internal readonly ISunWindow Window;
        internal readonly IGL GL;

        internal World World;

        internal Model Sphere;
        internal Model SaturnRingsModel;

        internal Shader DiffuseShader;
        internal Shader DiffuseDarkShader;
        internal Shader SunShader;

        #region planet textures
        internal GpuTexture MercuryTexture;
        internal GpuTexture VenusTexture;
        internal GpuTexture EarthTexture;
        internal GpuTexture EarthDarkTexture;
        internal GpuTexture MarsTexture;
        internal GpuTexture JupiterTexture;
        internal GpuTexture SaturnTexture;
        internal GpuTexture SaturnRingTexture;
        internal GpuTexture UranusTexture;
        internal GpuTexture NeptuneTexture;
        internal GpuTexture PlutoTexture;
        #endregion

        #region moon textures
        internal GpuTexture MoonTexture;

        /// <summary>
        /// source: https://www.deviantart.com/askaniy/art/Phobos-Texture-Map-784146561
        /// </summary>
        internal GpuTexture PhobosTexture;

        /// <summary>
        /// source: https://www.deviantart.com/askaniy/art/Deimos-Texture-Map-784881224
        /// </summary>
        internal GpuTexture DeimosTexture;

        /// <summary>
        /// source: https://www.deviantart.com/fargetanik/art/Io-True-Color-Texture-Map-8k-787014794
        /// </summary>
        internal GpuTexture IoTexture;

        /// <summary>
        /// source: https://www.deviantart.com/askaniy/art/Europa-Texture-Map-20K-769790967
        /// </summary>
        internal GpuTexture EuropaTexture;

        /// <summary>
        /// source: https://www.deviantart.com/askaniy/art/Ganymede-Texture-Map-11K-808732114
        /// </summary>
        internal GpuTexture GanymedeTexture;

        /// <summary>
        /// source: https://www.deviantart.com/askaniy/art/Callisto-Texture-Map-8K-808772741
        /// </summary>
        internal GpuTexture CallistoTexture;

        /// <summary>
        /// source: http://www.celestiamotherlode.net/addon/addon_1508.html
        /// </summary>
        internal GpuTexture TitanTexture;

        /// <summary>
        /// source: https://www.deviantart.com/fargetanik/art/Rhea-Albedo-Texture-Map-8k-708835810
        /// </summary>
        internal GpuTexture RheaTexture;

        /// <summary>
        /// source: https://www.deviantart.com/fargetanik/art/Iapetus-Truecolor-Texture-Map-8k-814001056
        /// </summary>
        internal GpuTexture IapetusTexture;

        /// <summary>
        /// source: https://www.deviantart.com/oleg-pluton/art/Dione-texture-map-improved-770832745
        /// </summary>
        internal GpuTexture DioneTexture;

        /// <summary>
        /// source: https://photojournal.jpl.nasa.gov/catalog/PIA18439
        /// </summary>
        internal GpuTexture TethysTexture;

        /// <summary>
        /// source: https://photojournal.jpl.nasa.gov/catalog/PIA18435
        /// </summary>
        internal GpuTexture EnceladusTexture;

        /// <summary>
        /// source: https://www.deviantart.com/oleg-pluton/art/Mimas-texture-map-786933508
        /// </summary>
        internal GpuTexture MimasTexture;

        /// <summary>
        /// source: https://planet-texture-maps.fandom.com/wiki/Miranda
        /// </summary>
        internal GpuTexture MirandaTexture;

        /// <summary>
        /// source: https://planet-texture-maps.fandom.com/wiki/Ariel
        /// </summary>
        internal GpuTexture ArielTexture;

        /// <summary>
        /// source: https://planet-texture-maps.fandom.com/wiki/Umbriel
        /// </summary>
        internal GpuTexture UmbrielTexture;

        /// <summary>
        /// source: https://www.deviantart.com/magentameteorite/art/Titania-Texture-Map-filled-missing-Data-793854798
        /// </summary>
        internal GpuTexture TitaniaTexture;

        /// <summary>
        /// source: https://planet-texture-maps.fandom.com/wiki/Oberon
        /// </summary>
        internal GpuTexture OberonTexture;

        /// <summary>
        /// source: https://www.deviantart.com/askaniy/art/Triton-Texture-Map-14K-761940736
        /// </summary>
        internal GpuTexture TritonTexture;

        /// <summary>
        /// source: https://www.deviantart.com/magentameteorite/art/Charon-Texture-Map-794040506
        /// </summary>
        internal GpuTexture CharonTexture;

        #endregion
        internal GpuTexture SunTexture;

        internal IDictionary<SolarObject, PlanetObject> SolarObjects;
        internal IList<PlanetRing> Rings;

        internal CameraObject CameraObject;
        internal readonly float _scale;
        internal readonly double _timeRate;
        public SunSystem(ISunWindow window, float scale = 50f, double timeRate = 1f)
        {
            _scale = scale;
            _timeRate = timeRate;
            Window = window;
            GL = window.GL;
            SolarObjects = new Dictionary<SolarObject, PlanetObject>();
            Rings = new List<PlanetRing>();
        }

        public void OnLoad(object sender, EventArgs e)
        {
            World = new World(GL, Window.Input, Window.Dimension);
            CameraObject = new CameraObject(World, Window, SolarObjects);


            #region load textures

            #region async load
            {
                var sunTexture = LoadTexture("Resources/textures/2k_sun.jpg");
                var mercuryTexture = LoadTexture("Resources/textures/2k_mercury.jpg");
                var venusTexture = LoadTexture("Resources/textures/2k_venus_surface.jpg");
                var earthTexture = LoadTexture("Resources/textures/2k_earth_daymap.jpg");
                var earthDarkTexture = LoadTexture("Resources/textures/2k_earth_nightmap.jpg");
                var marsTexture = LoadTexture("Resources/textures/2k_mars.jpg");
                var jupiterTexture = LoadTexture("Resources/textures/2k_jupiter.jpg");
                var saturnTexture = LoadTexture("Resources/textures/2k_saturn.jpg");
                var saturnRingTexture = LoadTexture("Resources/textures/2k_saturn_ring_alpha.png");
                var uranusTexture = LoadTexture("Resources/textures/2k_uranus.jpg");
                var neptuneTexture = LoadTexture("Resources/textures/2k_neptune.jpg");
                var plutoTexture = LoadTexture("Resources/textures/2k_pluto.jpg");

                var moonTexture = LoadTexture("Resources/textures/moons/2k_moon.jpg");

                var phobosTexture = LoadTexture("Resources/textures/moons/mars/phobos.png");
                var deimosTexture = LoadTexture("Resources/textures/moons/mars/deimos.png");

                var callistoTexture = LoadTexture("Resources/textures/moons/jupiter/callisto.png");
                var europaTexture = LoadTexture("Resources/textures/moons/jupiter/europa.png");
                var ganymedeTexture = LoadTexture("Resources/textures/moons/jupiter/ganymede.png");
                var ioTexture = LoadTexture("Resources/textures/moons/jupiter/io.png");

                var dioneTexture = LoadTexture("Resources/textures/moons/saturn/dione.png");
                var enceladusTexture = LoadTexture("Resources/textures/moons/saturn/enceladus.png");
                var iapetusTexture = LoadTexture("Resources/textures/moons/saturn/iapetus.png");
                var mimasTexture = LoadTexture("Resources/textures/moons/saturn/mimas.png");
                var rheaTexture = LoadTexture("Resources/textures/moons/saturn/rhea.png");
                var tethysTexture = LoadTexture("Resources/textures/moons/saturn/tethys.png");
                var titanTexture = LoadTexture("Resources/textures/moons/saturn/titan.png");

                var arielTexture = LoadTexture("Resources/textures/moons/uranus/ariel.jpg");
                var mirandaTexture = LoadTexture("Resources/textures/moons/uranus/miranda.jpg");
                var oberonTexture = LoadTexture("Resources/textures/moons/uranus/oberon.png");
                var titaniaTexture = LoadTexture("Resources/textures/moons/uranus/titania.png");
                var umbrielTexture = LoadTexture("Resources/textures/moons/uranus/umbriel.jfif");

                var tritonTexture = LoadTexture("Resources/textures/moons/neptune/triton.png");

                var charonTexture = LoadTexture("Resources/textures/moons/pluto/charon.jpg");
                SunTexture = LoadGpuTexture(sunTexture);
                MercuryTexture = LoadGpuTexture(mercuryTexture);
                VenusTexture = LoadGpuTexture(venusTexture);
                EarthTexture = LoadGpuTexture(earthTexture);
                EarthDarkTexture = LoadGpuTexture(earthDarkTexture);
                MarsTexture = LoadGpuTexture(marsTexture);
                JupiterTexture = LoadGpuTexture(jupiterTexture);
                SaturnTexture = LoadGpuTexture(saturnTexture);
                SaturnRingTexture = LoadGpuTexture(saturnRingTexture);
                UranusTexture = LoadGpuTexture(uranusTexture);
                NeptuneTexture = LoadGpuTexture(neptuneTexture);
                PlutoTexture = LoadGpuTexture(plutoTexture);
                MoonTexture = LoadGpuTexture(moonTexture);
                PhobosTexture = LoadGpuTexture(phobosTexture);
                DeimosTexture = LoadGpuTexture(deimosTexture);
                CallistoTexture = LoadGpuTexture(callistoTexture);
                EuropaTexture = LoadGpuTexture(europaTexture);
                GanymedeTexture = LoadGpuTexture(ganymedeTexture);
                IoTexture = LoadGpuTexture(ioTexture);
                DioneTexture = LoadGpuTexture(dioneTexture);
                EnceladusTexture = LoadGpuTexture(enceladusTexture);
                IapetusTexture = LoadGpuTexture(iapetusTexture);
                MimasTexture = LoadGpuTexture(mimasTexture);
                RheaTexture = LoadGpuTexture(rheaTexture);
                TethysTexture = LoadGpuTexture(tethysTexture);
                TitanTexture = LoadGpuTexture(titanTexture);
                ArielTexture = LoadGpuTexture(arielTexture);
                MirandaTexture = LoadGpuTexture(mirandaTexture);
                OberonTexture = LoadGpuTexture(oberonTexture);
                TitaniaTexture = LoadGpuTexture(titaniaTexture);
                UmbrielTexture = LoadGpuTexture(umbrielTexture);
                TritonTexture = LoadGpuTexture(tritonTexture);
                CharonTexture = LoadGpuTexture(charonTexture);
            }
            #endregion

            #endregion

            SunShader = LoadShader("Resources/shaders/diffuse.vert.glsl", "Resources/shaders/sun.frag.glsl");
            DiffuseDarkShader = LoadShader("Resources/shaders/diffuse.vert.glsl", "Resources/shaders/diffuse_dark.frag.glsl");
            DiffuseShader = LoadShader("Resources/shaders/diffuse.vert.glsl", "Resources/shaders/diffuse.frag.glsl");
            Sphere = LoadModel("Resources/models/sphere.dae");
            SaturnRingsModel = LoadModel("Resources/models/saturn_rings.dae");

            #region planets
            SolarObjects.Add(new PlanetObject(SolarObject.Mercury, World, Sphere, DiffuseShader, "Matrices", "Model", MercuryTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(2439.7),
                Scale = _scale,
                SemiMajorAxis = (0.38709927, 0.00000037),
                Eccentricity = (0.20563593, 0.00001906),
                Inclination = (7.00497902, -0.00594749),
                MeanLogitude = (252.2503235, 149472.67411175),
                PerihelionLongidute = (77.45779628, 0.16047689),
                AscendingLongidute = (48.33076593, -0.12534081),
                AxialTilt = 0.03f,
                RotationHours = 1407.6f
            });

            SolarObjects.Add(new PlanetObject(SolarObject.Venus, World, Sphere, DiffuseShader, "Matrices", "Model", VenusTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(6051.8),
                Scale = _scale,
                SemiMajorAxis = (0.72333566, 0.0000039),
                Eccentricity = (0.00677672, -0.00004107),
                Inclination = (3.39467605, -0.0007889),
                MeanLogitude = (181.9790995, 58517.81538729),
                PerihelionLongidute = (131.60246718, 0.00268329),
                AscendingLongidute = (76.67984255, -0.27769418),
                AxialTilt = 2.64f,
                RotationHours = -5832.6f
            });

            SolarObjects.Add(new PlanetObject(SolarObject.Earth, World, Sphere, DiffuseDarkShader, "Matrices", "Model", EarthTexture, EarthDarkTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(6371.0),
                Scale = _scale,
                SemiMajorAxis = (1.00000261, 0.00000562),
                Eccentricity = (0.01671123, -0.00004392),
                Inclination = (-0.00001531, -0.01294668),
                MeanLogitude = (100.46457166, 35999.37244981),
                PerihelionLongidute = (102.93768193, 0.32327364),
                AscendingLongidute = (0, 0),
                AxialTilt = 23.44f,
                RotationHours = 23.93f
            });

            SolarObjects.Add(new PlanetObject(SolarObject.Mars, World, Sphere, DiffuseShader, "Matrices", "Model", MarsTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(3389.5),
                Scale = _scale,
                SemiMajorAxis = (1.52371034, 0.00001847),
                Eccentricity = (0.0933941, 0.00007882),
                Inclination = (1.84969142, -0.00813131),
                MeanLogitude = (-4.55343205, 19140.30268499),
                PerihelionLongidute = (-23.94362959, 0.44441088),
                AscendingLongidute = (49.55953891, -0.29257343),
                AxialTilt = 23.44f,
                RotationHours = 23.93f
            });

            SolarObjects.Add(new PlanetObject(SolarObject.Jupiter, World, Sphere, DiffuseShader, "Matrices", "Model", JupiterTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(96911),
                Scale = _scale,
                SemiMajorAxis = (5.202887, -0.00011607),
                Eccentricity = (0.04838624, -0.00013253),
                Inclination = (1.30439695, -0.00183714),
                MeanLogitude = (34.39644051, 3034.74612775),
                PerihelionLongidute = (14.72847983, 0.21252668),
                AscendingLongidute = (100.47390909, 0.20469106),
                AxialTilt = 3.13f,
                RotationHours = 9.93f,
            });

            SolarObjects.Add(new PlanetObject(SolarObject.Saturn, World, Sphere, DiffuseShader, "Matrices", "Model", SaturnTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(58232),
                Scale = _scale,
                SemiMajorAxis = (9.53667594, -0.00125060),
                Eccentricity = (0.05386179, -0.00050991),
                Inclination = (2.48599187, 0.00193609),
                MeanLogitude = (49.95424423, 1222.49362201),
                PerihelionLongidute = (92.59887831, -0.41897216),
                AscendingLongidute = (113.66242448, -0.28867794),
                AxialTilt = 26.73f,
                RotationHours = 10.56f,
            });

            Rings.Add(new PlanetRing(World, SolarObjects[SolarObject.Saturn], "SaturnRing", SaturnRingsModel, DiffuseShader, "Matrices", "Model", SaturnRingTexture));
            

            SolarObjects.Add(new PlanetObject(SolarObject.Uranus, World, Sphere, DiffuseShader, "Matrices", "Model", UranusTexture)
            {
                PlanetSize = (float)ToAU(25362),
                Scale = _scale,
                SemiMajorAxis = (19.18916464, -0.00196176),
                Eccentricity = (0.04725744, -0.00004397),
                Inclination = (0.77263783, -0.00242939),
                MeanLogitude = (313.23810451, 428.48202785),
                PerihelionLongidute = (170.9542763, 0.40805281),
                AscendingLongidute = (74.01692503, 0.04240589),
                AxialTilt = 82.23f,
                RotationHours = -17.24f,
            });

            SolarObjects.Add(new PlanetObject(SolarObject.Neptune, World, Sphere, DiffuseShader, "Matrices", "Model", NeptuneTexture)
            {
                PlanetSize = (float)ToAU(24622),
                Scale = _scale,
                SemiMajorAxis = (30.06992276, 0.00026291),
                Eccentricity = (0.00859048, 0.00005105),
                Inclination = (1.77004347, 0.00035372),
                MeanLogitude = (-55.12002969, 218.45945325),
                PerihelionLongidute = (44.96476227, -0.32241464),
                AscendingLongidute = (131.78422574, -0.00508664),
                AxialTilt = 28.32f,
                RotationHours = 16.11f
            });

            SolarObjects.Add(new PlanetObject(SolarObject.Pluto, World, Sphere, DiffuseShader, "Matrices", "Model", PlutoTexture)
            {
                PlanetSize = (float)ToAU(1188.3),
                Scale = _scale,
                SemiMajorAxis = (39.48211675, -0.00031596),
                Eccentricity = (0.2488273, 0.0000517),
                Inclination = (17.14001206, 0.00004818),
                MeanLogitude = (238.92903833, 145.20780515),
                PerihelionLongidute = (224.06891629, -0.04062942),
                AscendingLongidute = (110.30393684, -0.01183482),
                AxialTilt = 57.47f,
                RotationHours = -153.29f,
            });
            #endregion

            #region moons
            SolarObjects.Add(new MoonObject(SolarObjects[SolarObject.Earth], SolarObject.Moon,
                World, Sphere, DiffuseShader, "Matrices", "Model", MoonTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(1737.1),
                SemiMajorAxis = (ToAU(384400), 0),
                Eccentricity = (0.0554, 0),
                Inclination = (5.16, 0),
                ArgumentOfPerihelion = (318.15, 0),
                AscendingLongidute = (125.08, ToDegCentury(13.176358)),
                AxialTilt = 6.68f,
                RotationHours = 655.73f,
                EnableArgumentOfPerihelionAndMeanLongitude = true
            });

            SolarObjects.Add(new MoonObject(SolarObjects[SolarObject.Mars], SolarObject.Phobos,
                World, Sphere, DiffuseShader, "Matrices", "Model", PhobosTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(11.267),
                SemiMajorAxis = (ToAU(9376), 0),
                Eccentricity = (0.0151, 0),
                Inclination = (1.075, 0),
                ArgumentOfPerihelion = (150.057, 0),
                AscendingLongidute = (207.784, ToDegCentury(1128.8447569)),
                AxialTilt = 0,
                RotationHours = 7.65f,
                EnableArgumentOfPerihelionAndMeanLongitude = true
            });

            SolarObjects.Add(new MoonObject(SolarObjects[SolarObject.Mars], SolarObject.Deimos,
               World, Sphere, DiffuseShader, "Matrices", "Model", DeimosTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(6.2),
                SemiMajorAxis = (ToAU(23458), 0),
                Eccentricity = (0.0002, 0),
                Inclination = (1.788, 0),
                ArgumentOfPerihelion = (260.729, 0),
                AscendingLongidute = (24.525, ToDegCentury(285.1618790)),
                AxialTilt = 0,
                RotationHours = 30.3f,
                EnableArgumentOfPerihelionAndMeanLongitude = true
            });

            SolarObjects.Add(new MoonObject(SolarObjects[SolarObject.Jupiter], SolarObject.Callisto,
                World, Sphere, DiffuseShader, "Matrices", "Model", CallistoTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(2410.3),
                SemiMajorAxis = (ToAU(1_882_700), 0),
                Eccentricity = (0.0074, 0),
                Inclination = (0.192, 0),
                ArgumentOfPerihelion = (52.643, 0),
                AscendingLongidute = (298.848, ToDegCentury(21.5710728)),
                AxialTilt = 0,
                RotationHours = 400.5364416f,
                EnableArgumentOfPerihelionAndMeanLongitude = true
            });

            SolarObjects.Add(new MoonObject(SolarObjects[SolarObject.Jupiter], SolarObject.Europa,
                World, Sphere, DiffuseShader, "Matrices", "Model", EuropaTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(1560.8),
                SemiMajorAxis = (ToAU(671_100), 0),
                Eccentricity = (0.0094, 0),
                Inclination = (0.466, 0),
                ArgumentOfPerihelion = (88.97, 0),
                AscendingLongidute = (219.106, ToDegCentury(101.3747242)),
                AxialTilt = 0.1f,
                RotationHours = 85.228344f,
                EnableArgumentOfPerihelionAndMeanLongitude = true
            });

            SolarObjects.Add(new MoonObject(SolarObjects[SolarObject.Jupiter], SolarObject.Ganymede,
                World, Sphere, DiffuseShader, "Matrices", "Model", GanymedeTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(2634.1),
                SemiMajorAxis = (ToAU(1_070_400), 0),
                Eccentricity = (0.0013, 0),
                Inclination = (0.177, 0),
                ArgumentOfPerihelion = (192.417, 0),
                AscendingLongidute = (63.552, ToDegCentury(50.3176072)),
                AxialTilt = 0.33f,
                RotationHours = 171.70927104f,
                EnableArgumentOfPerihelionAndMeanLongitude = true
            });

            SolarObjects.Add(new MoonObject(SolarObjects[SolarObject.Jupiter], SolarObject.Io,
                World, Sphere, DiffuseShader, "Matrices", "Model", IoTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(1821.6),
                SemiMajorAxis = (ToAU(421_800), 0),
                Eccentricity = (0.0041, 0),
                Inclination = (0.036, 0),
                ArgumentOfPerihelion = (84.129, 0),
                AscendingLongidute = (43.977, ToDegCentury(203.4889583)),
                AxialTilt = 0f,
                RotationHours = 42.459306864f,
                EnableArgumentOfPerihelionAndMeanLongitude = true
            });

            SolarObjects.Add(new MoonObject(SolarObjects[SolarObject.Saturn], SolarObject.Dione,
                World, Sphere, DiffuseShader, "Matrices", "Model", DioneTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(561.4),
                SemiMajorAxis = (ToAU(377_415), 0),
                Eccentricity = (0.0022, 0),
                Inclination = (0.028, 0),
                ArgumentOfPerihelion = (284.315, 0),
                AscendingLongidute = (290.415, ToDegCentury(131.5349307)),
                AxialTilt = 0f,
                RotationHours = 65.68596f,
                EnableArgumentOfPerihelionAndMeanLongitude = true
            });

            SolarObjects.Add(new MoonObject(SolarObjects[SolarObject.Saturn], SolarObject.Enceladus,
                World, Sphere, DiffuseShader, "Matrices", "Model", EnceladusTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(252.1),
                SemiMajorAxis = (ToAU(238_042), 0),
                Eccentricity = (0, 0),
                Inclination = (0.003, 0),
                ArgumentOfPerihelion = (0.076, 0),
                AscendingLongidute = (342.507, ToDegCentury(262.7318978)),
                AxialTilt = 0f,
                RotationHours = 32.885232f,
                EnableArgumentOfPerihelionAndMeanLongitude = true
            });

            SolarObjects.Add(new MoonObject(SolarObjects[SolarObject.Saturn], SolarObject.Iapetus,
                World, Sphere, DiffuseShader, "Matrices", "Model", IapetusTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(734.5),
                SemiMajorAxis = (ToAU(3_560_854), 0),
                Eccentricity = (0.0293, 0),
                Inclination = (8.298, 0),
                ArgumentOfPerihelion = (271.606, 0),
                AscendingLongidute = (81.105, ToDegCentury(4.5379416)),
                AxialTilt = 0f,
                RotationHours = 1903.716f,
                EnableArgumentOfPerihelionAndMeanLongitude = true
            });

            SolarObjects.Add(new MoonObject(SolarObjects[SolarObject.Saturn], SolarObject.Mimas,
                World, Sphere, DiffuseShader, "Matrices", "Model", MimasTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(396.4),
                SemiMajorAxis = (ToAU(185_539), 0),
                Eccentricity = (0.0196, 0),
                Inclination = (1.574, 0),
                ArgumentOfPerihelion = (332.499, 0),
                AscendingLongidute = (173.027, ToDegCentury(381.9944948)),
                AxialTilt = 0f,
                RotationHours = 22.608f,
                EnableArgumentOfPerihelionAndMeanLongitude = true
            });

            SolarObjects.Add(new MoonObject(SolarObjects[SolarObject.Saturn], SolarObject.Rhea,
                World, Sphere, DiffuseShader, "Matrices", "Model", RheaTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(763.8),
                SemiMajorAxis = (ToAU(527_068), 0),
                Eccentricity = (0.0002, 0),
                Inclination = (0.333, 0),
                ArgumentOfPerihelion = (241.619, 0),
                AscendingLongidute = (351.042, ToDegCentury(79.6900459)),
                AxialTilt = 0f,
                RotationHours = 108.437088f,
                EnableArgumentOfPerihelionAndMeanLongitude = true
            });

            SolarObjects.Add(new MoonObject(SolarObjects[SolarObject.Saturn], SolarObject.Tethys,
                World, Sphere, DiffuseShader, "Matrices", "Model", TethysTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(531),
                SemiMajorAxis = (ToAU(294_672), 0),
                Eccentricity = (0.0001, 0),
                Inclination = (1.091, 0),
                ArgumentOfPerihelion = (45.202, 0),
                AscendingLongidute = (259.842, ToDegCentury(190.6979109)),
                AxialTilt = 0f,
                RotationHours = 45.307248f,
                EnableArgumentOfPerihelionAndMeanLongitude = true
            });

            SolarObjects.Add(new MoonObject(SolarObjects[SolarObject.Saturn], SolarObject.Titan,
                World, Sphere, DiffuseShader, "Matrices", "Model", TitanTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(2574.7),
                SemiMajorAxis = (ToAU(1_221_865), 0),
                Eccentricity = (0.0288, 0),
                Inclination = (0.306, 0),
                ArgumentOfPerihelion = (180.532, 0),
                AscendingLongidute = (28.060, ToDegCentury(22.5769756)),
                AxialTilt = 0f,
                RotationHours = 382.68f,
                EnableArgumentOfPerihelionAndMeanLongitude = true
            });

            SolarObjects.Add(new MoonObject(SolarObjects[SolarObject.Uranus], SolarObject.Ariel,
                World, Sphere, DiffuseShader, "Matrices", "Model", ArielTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(578.9),
                SemiMajorAxis = (ToAU(190_900), 0),
                Eccentricity = (0.0012, 0),
                Inclination = (0.041, 0),
                ArgumentOfPerihelion = (115.349, 0),
                AscendingLongidute = (22.394, ToDegCentury(142.8356579)),
                AxialTilt = 0f,
                RotationHours = 60.48f,
                EnableArgumentOfPerihelionAndMeanLongitude = true
            });

            SolarObjects.Add(new MoonObject(SolarObjects[SolarObject.Uranus], SolarObject.Miranda,
                World, Sphere, DiffuseShader, "Matrices", "Model", MirandaTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(235.8),
                SemiMajorAxis = (ToAU(129_900), 0),
                Eccentricity = (0.0013, 0),
                Inclination = (4.338, 0),
                ArgumentOfPerihelion = (68.312, 0),
                AscendingLongidute = (326.438, ToDegCentury(254.6906576)),
                AxialTilt = 0f,
                RotationHours = 33.923496f,
                EnableArgumentOfPerihelionAndMeanLongitude = true
            });

            SolarObjects.Add(new MoonObject(SolarObjects[SolarObject.Uranus], SolarObject.Oberon,
                World, Sphere, DiffuseShader, "Matrices", "Model", OberonTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(761.4),
                SemiMajorAxis = (ToAU(583_500), 0),
                Eccentricity = (0.0014, 0),
                Inclination = (0.068, 0),
                ArgumentOfPerihelion = (104.400, 0),
                AscendingLongidute = (279.771, ToDegCentury(26.7394888)),
                AxialTilt = 0f,
                RotationHours = 323.117616f,
                EnableArgumentOfPerihelionAndMeanLongitude = true
            });

            SolarObjects.Add(new MoonObject(SolarObjects[SolarObject.Uranus], SolarObject.Titania,
                World, Sphere, DiffuseShader, "Matrices", "Model", TitaniaTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(788.4),
                SemiMajorAxis = (ToAU(436_300), 0),
                Eccentricity = (0.0011, 0),
                Inclination = (0.079, 0),
                ArgumentOfPerihelion = (284.400, 0),
                AscendingLongidute = (99.771, ToDegCentury(41.3514246)),
                AxialTilt = 0f,
                RotationHours = 208.949616f,
                EnableArgumentOfPerihelionAndMeanLongitude = true
            });

            SolarObjects.Add(new MoonObject(SolarObjects[SolarObject.Uranus], SolarObject.Umbriel,
                World, Sphere, DiffuseShader, "Matrices", "Model", UmbrielTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(584.7),
                SemiMajorAxis = (ToAU(266_000), 0),
                Eccentricity = (0.0039, 0),
                Inclination = (0.128, 0),
                ArgumentOfPerihelion = (84.709, 0),
                AscendingLongidute = (33.485, ToDegCentury(86.8688879)),
                AxialTilt = 0f,
                RotationHours = 99.456f,
                EnableArgumentOfPerihelionAndMeanLongitude = true
            });

            SolarObjects.Add(new MoonObject(SolarObjects[SolarObject.Neptune], SolarObject.Triton,
                World, Sphere, DiffuseShader, "Matrices", "Model", TritonTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(1353.4),
                SemiMajorAxis = (ToAU(354_759), 0),
                Eccentricity = (0, 0),
                Inclination = (156.865, 0),
                ArgumentOfPerihelion = (66.142, 0),
                AscendingLongidute = (177.608, ToDegCentury(61.2572638)),
                AxialTilt = 0f,
                RotationHours = 141.044496f,
                EnableArgumentOfPerihelionAndMeanLongitude = true
            });

            SolarObjects.Add(new MoonObject(SolarObjects[SolarObject.Pluto], SolarObject.Charon,
                World, Sphere, DiffuseShader, "Matrices", "Model", CharonTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(606),
                SemiMajorAxis = (ToAU(19_591), 0),
                Eccentricity = (0.0002, 0),
                Inclination = (0.08, 0),
                ArgumentOfPerihelion = (146.106, 0),
                AscendingLongidute = (26.928, ToDegCentury(56.3625210)),
                AxialTilt = 0f,
                RotationHours = 153.2935296f,
                EnableArgumentOfPerihelionAndMeanLongitude = true
            });

            #endregion

            SolarObjects.Add(new PlanetObject(SolarObject.Sun, World, Sphere, SunShader, "Matrices", "Model", SunTexture)
            {
                TimeRate = _timeRate,
                PlanetSize = (float)ToAU(696340.0),
                Scale = _scale,
                AxialTilt = 7.25f,
                RotationHours = 609.12f
            });
            
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void OnUpdate(object sender, ElapsedTimeEventArgs e)
        {
            World.ComponentSubsystem.Update(null, e);
            World.ComponentSubsystem.LateUpdate(null, e);
        }

        public void OnRenderFrame(object sender, ElapsedTimeEventArgs e)
        {
            GL.ClearColor(Color.FromArgb(23, 29, 31));
            GL.Enable(EnableCap.DepthTest);

            GL.Clear(ClearMask.ColorBufferBit | ClearMask.DepthBufferBit);

            World.DrawSubsystem.BeforeDraw(null, e);
            World.DrawSubsystem.Draw(null, e);

            Window.SwapBuffers();
        }
        public void OnResize(object _, Size size)
        {
            CameraObject.Camera.Camera.AspectRatio = size.Width / (float)size.Height;
        }

        public void SetTimeRate(double timeRate)
        {
            foreach (var obj in SolarObjects.Values)
                obj.TimeRate = timeRate;
        }

        public void SetScales(float scale)
        {
            foreach (var obj in SolarObjects.Values)
                if(!(obj is MoonObject))
                    obj.Scale = scale;
        }

        private Model LoadModel(string path)
        {
            using (var stream = File.OpenRead(path))           
                return new Model(GL, SunLoader.LoadMesh(stream)[0]);
        }

        private async Task<Texture> LoadTexture(string path)
        {
            Task<Texture> task = new Task<Texture>(() => 
            {
                using (var stream = File.OpenRead(path))
                {
                    Texture texture = SunLoader.LoadTexture(stream);
                    return texture;
                }
            });
            task.Start();

            await task;
            return task.Result;
        }

        private GpuTexture LoadGpuTexture(Task<Texture> texture)
        {
            texture.Wait();
            return new GpuTexture(GL, texture.Result, PixelInternalFormat.RGBA8);
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

        internal static double ToDegCentury(double degDays) =>
            degDays * 36500;
    }
}
