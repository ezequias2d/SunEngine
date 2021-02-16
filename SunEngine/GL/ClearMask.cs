// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/. --%>
using System;

namespace SunEngine.GL
{
    [Flags]
    public enum ClearMask
    {
        None = 0,
        DepthBufferBit = 1,
        AccumBufferBit = 1 << 1,
        StencilBufferBit = 1 << 2,
        ColorBufferBit = 1 << 3,
    }
}
