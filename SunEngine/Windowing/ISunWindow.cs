// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using SunEngine.GL;
using SunEngine.Graphics;
using SunEngine.Inputs;
using System;
using System.Drawing;

namespace SunEngine.Windowing
{
    public interface ISunWindow
    {
        IGL GL { get; }
        IInput Input { get; }
        bool IsVisible { get; set; }
        Point Position { get; set; }
        Size Dimension { get; set; }
        double FramesPerSecond { get; set; }
        double UpdatesPerSecond { get; set; }
        bool VSync { get; set; }
        string Title { get; set; }

        EventHandler Load { get; set; }
        EventHandler<ElapsedTimeEventArgs> Update { get; set; }
        EventHandler<ElapsedTimeEventArgs> RenderFrame { get; set; }

        void Run();
        void SwapBuffers();
    }
}
