// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/. --%>
using SunEngine.Inputs;
using System;

namespace SunEngine.SilkNET
{
    public unsafe struct KeyboardState : IKeyboardState
    {
        private static bool IsAny(byte* keyEvents, KeyEvent keyEvent)
        {
            for (int i = 0; i < KeyEventsLength; i++)
            {
                if ((KeyEvent)keyEvents[i] == keyEvent)
                    return true;
            }
            return false;
        }

        private const int KeyEventsLength = (int)Inputs.Key.LastKey + 1;
        private fixed byte _previousKeyEvents[KeyEventsLength];
        private fixed byte _keyEvents[KeyEventsLength];

        private KeyboardState(ReadOnlySpan<KeyEvent> keyEvents, ReadOnlySpan<KeyEvent> previousKeyEvents, bool isAnyKeyDown, bool isAnyKeyRepeat, bool isAnyKeyUp)
        {
            fixed (void* dst = _keyEvents, src = keyEvents, previousDst = _previousKeyEvents, previousSrc = previousKeyEvents)
            {
                Buffer.MemoryCopy(src, dst, KeyEventsLength, keyEvents.Length);
                Buffer.MemoryCopy(previousSrc, previousDst, KeyEventsLength, previousKeyEvents.Length);

                IsAnyKeyDown = isAnyKeyDown;
                IsAnyKeyRepeat = isAnyKeyRepeat;
                IsAnyKeyUp = isAnyKeyUp;
            }
        }

        internal KeyboardState(ReadOnlySpan<KeyEvent> keyEvents, ReadOnlySpan<KeyEvent> previousKeyEvents)
        {
            fixed(void* dst = _keyEvents, src = keyEvents, previousDst = _previousKeyEvents, previousSrc = previousKeyEvents)
            {
                Buffer.MemoryCopy(src, dst, KeyEventsLength, keyEvents.Length);
                Buffer.MemoryCopy(previousSrc, previousDst, KeyEventsLength, previousKeyEvents.Length);

                IsAnyKeyDown = IsAny((byte*)dst, KeyEvent.Down);
                IsAnyKeyRepeat = IsAny((byte*)dst, KeyEvent.Repeat);
                IsAnyKeyUp = IsAny((byte*)dst, KeyEvent.Up);
            }
        }

        public KeyEvent this[Inputs.Key key] => (KeyEvent)_keyEvents[(int)key];

        public bool IsAnyKeyDown { get; }

        public bool IsAnyKeyUp { get; }

        public bool IsAnyKeyRepeat { get; }

        public KeyEvent GetKey(Inputs.Key key) => (KeyEvent)_keyEvents[(int)key];

        public KeyEvent GetPreviousKey(Inputs.Key key) => (KeyEvent)_previousKeyEvents[(int)key];

        public IKeyboardState GetSnapshot()
        {
            fixed(byte* keyEventsPtr = _keyEvents, previousKeyEventPtr = _previousKeyEvents)
            {
                return new KeyboardState(
                    new ReadOnlySpan<KeyEvent>(keyEventsPtr, KeyEventsLength), 
                    new ReadOnlySpan<KeyEvent>(previousKeyEventPtr, KeyEventsLength), 
                    IsAnyKeyDown, 
                    IsAnyKeyRepeat, 
                    IsAnyKeyUp);
            }
        }
    }
}
