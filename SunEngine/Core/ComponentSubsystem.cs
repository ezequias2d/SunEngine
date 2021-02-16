// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/. --%>
using SunEngine.Graphics;

namespace SunEngine.Core
{
    public sealed class ComponentSubsystem : BaseSubsystem<IComponent>, IComponent
    {
        internal ComponentSubsystem(World world) : base(world)
        {

        }

        public void Update(GameObject sender, ElapsedTimeEventArgs e)
        {
            foreach(var pair in this)
                pair.Key.Update(World.Get(pair.Value), e);
        }
    }
}
