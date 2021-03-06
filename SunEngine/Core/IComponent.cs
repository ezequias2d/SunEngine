﻿// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using SunEngine.Graphics;

namespace SunEngine.Core
{
    public interface IComponent
    {
        void Update(GameObject sender, ElapsedTimeEventArgs e);
        void LateUpdate(GameObject sender, ElapsedTimeEventArgs e);
    }
}
