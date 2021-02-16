// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/. --%>
using SunEngine.SolarSystem;
using SunEngine.Windowing;
using System.Drawing;

namespace SunEngine.Application
{
    class Program
    {
        static void Main(string[] _)
        {
            var description = SunWindowDescription.Default;
            description.Size = new Size(1280, 720);
            description.VSync = false;
            description.FramesPerSecond = 240;
            ISunWindow window = new SunWindow(description);

            
            var sunSystem = new SunSystem(window);

            window.Load += sunSystem.OnLoad;
            window.Update += sunSystem.OnUpdate;
            window.RenderFrame += sunSystem.OnRenderFrame;

            window.Run();
        }
    }
}
