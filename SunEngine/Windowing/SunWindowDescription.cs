// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/. --%>
using System.Drawing;

namespace SunEngine.Windowing
{
    public struct SunWindowDescription
    {
        public bool IsVisible;
        public Point Position;
        public Size Size;
        public WindowState WindowState;
        public WindowBorder WindowBorder;
        public double FramesPerSecond;
        public double UpdatesPerSecond;
        public bool VSync;
        public string Title;

        public static SunWindowDescription Default => new SunWindowDescription
        {
            IsVisible = true,
            Position = new Point(32, 32),
            Size = new Size(640, 480),
            WindowState = WindowState.Normal,
            WindowBorder = WindowBorder.Fixed,
            FramesPerSecond = 60d,
            UpdatesPerSecond = 60d,
            VSync = true,
            Title = "SunWindow"
        };
    }
}
