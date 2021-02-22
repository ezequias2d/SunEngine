// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Silk.NET.Input;
using SunEngine.Inputs;
using System;

namespace SunEngine.SilkNET
{
    public sealed class Keyboard
    {
        private const int KeyEventsLength = (int)Inputs.Key.LastKey + 1;
        private readonly IKeyboard _keyboard;
        private readonly KeyEvent[] _keyEvents;
        private readonly KeyEvent[] _previousKeyEvents;

        public Keyboard(IKeyboard keyboard)
        {
            _keyboard = keyboard;
            _keyEvents = new KeyEvent[KeyEventsLength];
            _previousKeyEvents = new KeyEvent[KeyEventsLength];
        }

        public void Update()
        {
            unsafe
            {
                fixed (void* src = _keyEvents, dst = _previousKeyEvents)
                    Buffer.MemoryCopy(src, dst, KeyEventsLength, KeyEventsLength);
            }

            for (Inputs.Key i = Inputs.Key.Space; i <= Inputs.Key.LastKey; i++)
            {
                bool value = _keyboard.IsKeyPressed(i.ToSilk());

                KeyEvent previous = _previousKeyEvents[(int)i];
                KeyEvent result = previous;
                if (value)
                    switch (previous)
                    {
                        case KeyEvent.Down:
                        case KeyEvent.Repeat:
                            result = KeyEvent.Repeat;
                            break;
                        case KeyEvent.Up:
                        case KeyEvent.None:
                            result = KeyEvent.Down;
                            break;
                    }
                else
                    switch (previous)
                    {
                        case KeyEvent.Up:
                            result = KeyEvent.None;
                            break;
                        case KeyEvent.Down:
                        case KeyEvent.Repeat:
                            result = KeyEvent.Up;
                            break;
                    }
                
                _keyEvents[(int)i] = result;
            }
        }

        public IKeyboardState GetSnapshot() => new KeyboardState(_keyEvents, _previousKeyEvents);
    }
}
