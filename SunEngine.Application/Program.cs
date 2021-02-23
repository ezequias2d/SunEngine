// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using SunEngine.SolarSystem;
using SunEngine.Windowing;
using System;
using System.Drawing;
using System.Threading;

namespace SunEngine.Application
{
    class Program
    {
        static SunSystem SunSystem;
        static ISunWindow Window;
        static void Main(string[] _)
        {
            var description = SunWindowDescription.Default;
            description.Size = new Size(640, 480);
            description.VSync = true;
            description.FramesPerSecond = 60;
            Window = new SunWindow(description);

            Thread menuThread = new Thread(Menu);
            menuThread.IsBackground = true;

            SunSystem = new SunSystem(Window);

            Window.Load += SunSystem.OnLoad;
            Window.Update += SunSystem.OnUpdate;
            Window.RenderFrame += SunSystem.OnRenderFrame;
            Window.Resize += SunSystem.OnResize;

            menuThread.Start();
            Window.Run();

            menuThread.Join();
        }

        static void Menu()
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Menu");
                Console.WriteLine("0 - Exit");
                Console.WriteLine("1 - Set resolution");
                Console.WriteLine("2 - Set time rate");

                int op = int.Parse(Console.ReadLine());
                switch (op)
                {
                    case 0:
                        Window.Close();
                        flag = false;
                        break;
                    case 1:
                        {
                            Console.WriteLine("Inputs resolution in format 'width height'");
                            Console.WriteLine(">");
                            var aux = Console.ReadLine().Split(' ');
                            Window.Dimension = new Size(int.Parse(aux[0]), int.Parse(aux[1]));
                        }
                        break;
                    case 2:
                        {
                            Console.WriteLine("Inputs in seconds the time rate to update the solar system.");
                            Console.WriteLine(">");
                            var aux = int.Parse(Console.ReadLine());
                            SunSystem.SetTimeRate(aux);
                        }
                        break;
                }
            }
        }
    }
}
