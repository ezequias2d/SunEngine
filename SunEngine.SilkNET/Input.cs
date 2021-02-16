// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/. --%>
using Silk.NET.Input;
using SunEngine.Inputs;
using System.Linq;

namespace SunEngine.SilkNET
{
    public sealed class Input : IInput
    {
        private readonly Keyboard _keyboard;

        public Input(IInputContext context)
        {
            _keyboard = new Keyboard(context.Keyboards.FirstOrDefault());
        }

        public IKeyboardState GetKeyboardState() => _keyboard.GetSnapshot();

        internal void Update()
        {
            _keyboard.Update();
        }
    }
}
