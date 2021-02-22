﻿// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;

namespace SunEngine.Graphics
{
    public class ElapsedTimeEventArgs : EventArgs
    {
        public readonly double Time;

        public ElapsedTimeEventArgs(double time)
        {
            Time = time;
        }
    }
}
