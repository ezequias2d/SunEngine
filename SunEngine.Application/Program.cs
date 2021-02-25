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
        static float currentScale;
        static double currentTimeRate;
        static void Main(string[] _)
        {
            currentScale = 50f;
            currentTimeRate = 1f;

            var description = SunWindowDescription.Default;
            description.Size = new Size(640, 480);
            description.VSync = true;
            description.FramesPerSecond = 60;
            Window = new SunWindow(description);

            Thread menuThread = new Thread(Menu);
            menuThread.IsBackground = true;

            SunSystem = new SunSystem(Window, currentScale);

            Window.Load += SunSystem.OnLoad;
            Window.Update += SunSystem.OnUpdate;
            Window.RenderFrame += SunSystem.OnRenderFrame;
            Window.Resize += SunSystem.OnResize;

            menuThread.Start();
            Window.Run();
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
                Console.WriteLine("3 - Set celestial scale.");
                Console.WriteLine("4 - Help");

                Console.Write(">");

                try
                {
                    int op = int.Parse(Console.ReadLine());
                    switch (op)
                    {
                        case 0:
                            Window.Close();
                            flag = false;
                            break;
                        case 1:
                            SetResolution();
                            break;
                        case 2:
                            SetTimeRate();
                            break;
                        case 3:
                            SetScales();
                            break;
                        case 4:
                            Help();
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void SetResolution()
        {
            Console.WriteLine("Input is a resolution in format 'width height'");
            Console.WriteLine($"(Current resolution: {Window.Dimension})");
            Console.Write(">");
            var aux = Console.ReadLine().Split(' ');
            Window.Dimension = new Size(int.Parse(aux[0]), int.Parse(aux[1]));
        }

        private static void SetTimeRate()
        {
            Console.WriteLine("Input is in seconds the rate of time to update the solar system.");
            Console.WriteLine($"(Current TimeRate {currentTimeRate}) (525600 to simulate 1 year per 1 minute.)");
            Console.Write(">");
            currentTimeRate = double.Parse(Console.ReadLine());
            SunSystem.SetTimeRate(currentTimeRate);
        }

        private static void SetScales()
        {
            Console.WriteLine("Inputs is the scale of celestial bodies.");
            Console.WriteLine($"(Current: {currentScale})");
            Console.Write(">");
            currentScale = float.Parse(Console.ReadLine());
            SunSystem.SetScales(currentScale);
        }

        private static void Help()
        {
            Console.WriteLine(@"FreeCamera mode: END
Switch between mode focused on a celestial body / free camera: PAGE UP and PAGE DOWN.

FreeCamera mode:
    QWEASD: to move the camera along the three axes of freedom.
    UP, DOWN, LEFT, RIGHT: Rotate camera.

Focused on a celestial body:
    W: Zoom in.
    S: Zoom out.
    A: Maximum zoom.
    D: Zoom in 1 AU away.
    UP, DOWN, LEFT, RIGHT: Rotate around the celestial body.
");
        }
    }
}
