// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Silk.NET.Windowing;
using Silk.NET.Input;
using SunEngine.GL;
using SunEngine.Graphics;
using SunEngine.Inputs;
using SunEngine.SilkNET;
using SunEngine.Windowing;
using System;
using System.Drawing;

namespace SunEngine
{
    public sealed class SunWindow : ISunWindow
    {
        private readonly IWindow _window;
        private Input _input;
        public SunWindow(SunWindowDescription description)
        {            
            _window = Window.Create(new WindowOptions
            {
                IsVisible = description.IsVisible,
                Position = description.Position.ToSilk(),
                Size = description.Size.ToSilk(),
                WindowState = description.WindowState.ToSilk(),
                WindowBorder = description.WindowBorder.ToSilk(),
                TransparentFramebuffer = false,
                FramesPerSecond = description.FramesPerSecond,
                UpdatesPerSecond = description.UpdatesPerSecond,
                VSync = description.VSync,
                Title = description.Title,

                ShouldSwapAutomatically = false,
                IsEventDriven = false,
                API = new GraphicsAPI(ContextAPI.OpenGL, ContextProfile.Core, ContextFlags.Debug, new APIVersion(3, 3)),
                VideoMode = VideoMode.Default,
                PreferredDepthBufferBits = 32,
                PreferredStencilBufferBits = -1,
            });
            

            _window.Load += () => 
            {
                _input = new Input(_window.CreateInput());
                Load?.Invoke(this, EventArgs.Empty);
            };

            _window.Update += (elapsed) =>
            {
                _input.Update();
                Update?.Invoke(this, new ElapsedTimeEventArgs(elapsed));
            };

            _window.Render += (elapsed) =>
            {
                RenderFrame?.Invoke(this, new ElapsedTimeEventArgs(elapsed));
            };

            GL = new GLWrapper(_window);            
        }


        public IGL GL { get; }

        public bool IsVisible { get => _window.IsVisible; set => _window.IsVisible = value; }
        public Point Position { get => _window.Position.ToPoint(); set => _window.Position = value.ToSilk(); }
        public Size Dimension { get => _window.Size.ToSize(); set => _window.Size = value.ToSilk(); }
        public double FramesPerSecond { get => _window.FramesPerSecond; set => _window.FramesPerSecond = value; }
        public double UpdatesPerSecond { get => _window.UpdatesPerSecond; set => _window.UpdatesPerSecond = value; }
        public bool VSync { get => _window.VSync; set => _window.VSync = value; }
        public string Title { get => _window.Title; set => _window.Title = value; }
        public EventHandler Load { get; set; }
        public EventHandler<ElapsedTimeEventArgs> Update { get; set; }
        public EventHandler<ElapsedTimeEventArgs> RenderFrame { get; set; }

        public IInput Input => _input;

        public void Run() =>
            _window.Run();

        public void SwapBuffers() =>
            _window.SwapBuffers();
    }
}
