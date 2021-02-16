﻿// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/. --%>
namespace SunEngine.Inputs
{
    /// <summary>
    /// Specifies key codes and modifiers in US keyboard layout.
    /// </summary>
    public enum Key
    {
        /// <summary>
        /// An unknown key.
        /// </summary>
        Unknown,

        /// <summary>
        /// The spacebar key.
        /// </summary>
        Space,

        /// <summary>
        /// The apostrophe( ' ) key.
        /// </summary>
        Apostrophe,

        /// <summary>
        /// The comma( , ) key.
        /// </summary>
        Comma,

        /// <summary>
        /// The minus( - ) key.
        /// </summary>
        Minus,

        /// <summary>
        /// The period( . ) key.
        /// </summary>
        Period,

        /// <summary>
        /// The slash( / ) key.
        /// </summary>
        Slash,

        /// <summary>
        /// The 0 key.
        /// </summary>
        D0,

        /// <summary>
        /// The 1 key.
        /// </summary>
        D1,

        /// <summary>
        /// The 2 key.
        /// </summary>
        D2,

        /// <summary>
        /// The 3 key.
        /// </summary>
        D3,

        /// <summary>
        /// The 4 key.
        /// </summary>
        D4,

        /// <summary>
        /// The 5 key.
        /// </summary>
        D5,

        /// <summary>
        /// The 6 key.
        /// </summary>
        D6,

        /// <summary>
        /// The 7 key.
        /// </summary>
        D7,

        /// <summary>
        /// The 8 key.
        /// </summary>
        D8,

        /// <summary>
        /// The 9 key.
        /// </summary>
        D9,

        /// <summary>
        /// The semicolon( ; ) key.
        /// </summary>
        Semicolon,

        /// <summary>
        /// The equal( = ) key.
        /// </summary>
        Equal,

        /// <summary>
        /// The A key.
        /// </summary>
        A,

        /// <summary>
        /// The B key.
        /// </summary>
        B,

        /// <summary>
        /// The C key.
        /// </summary>
        C,

        /// <summary>
        /// The D key.
        /// </summary>
        D,

        /// <summary>
        /// The E key.
        /// </summary>
        E,

        /// <summary>
        /// The F key.
        /// </summary>
        F,

        /// <summary>
        /// The G key.
        /// </summary>
        G,

        /// <summary>
        /// The H key.
        /// </summary>
        H,

        /// <summary>
        /// The I key.
        /// </summary>
        I,

        /// <summary>
        /// The J key.
        /// </summary>
        J,

        /// <summary>
        /// The K key.
        /// </summary>
        K,

        /// <summary>
        /// The L key.
        /// </summary>
        L,

        /// <summary>
        /// The M key.
        /// </summary>
        M,

        /// <summary>
        /// The N key.
        /// </summary>
        N,

        /// <summary>
        /// The O key.
        /// </summary>
        O,

        /// <summary>
        /// The P key.
        /// </summary>
        P,

        /// <summary>
        /// The Q key.
        /// </summary>
        Q,

        /// <summary>
        /// The R key.
        /// </summary>
        R,

        /// <summary>
        /// The S key.
        /// </summary>
        S,

        /// <summary>
        /// The T key.
        /// </summary>
        T,

        /// <summary>
        /// The U key.
        /// </summary>
        U,

        /// <summary>
        /// The V key.
        /// </summary>
        V,

        /// <summary>
        /// The W key.
        /// </summary>
        W,

        /// <summary>
        /// The X key.
        /// </summary>
        X,

        /// <summary>
        /// The Y key.
        /// </summary>
        Y,

        /// <summary>
        /// The Z key.
        /// </summary>
        Z,

        /// <summary>
        /// The left bracket(opening bracket [ ) key.
        /// </summary>
        LeftBracket,

        /// <summary>
        /// The backslash( \ ).
        /// </summary>
        Backslash,

        /// <summary>
        /// The right bracket(closing bracket ] ) key.
        /// </summary>
        RightBracket,

        /// <summary>
        /// The grave accent( ` ) key.
        /// </summary>
        GraveAccent,

        /// <summary>
        /// The escape key.
        /// </summary>
        Escape,

        /// <summary>
        /// The enter key.
        /// </summary>
        Enter,

        /// <summary>
        /// The tab key.
        /// </summary>
        Tab,

        /// <summary>
        /// The backspace key.
        /// </summary>
        Backspace,

        /// <summary>
        /// The insert key.
        /// </summary>
        Insert,

        /// <summary>
        /// The delete key.
        /// </summary>
        Delete,

        /// <summary>
        /// The right arrow key.
        /// </summary>
        Right,

        /// <summary>
        /// The left arrow key.
        /// </summary>
        Left,

        /// <summary>
        /// The down arrow key.
        /// </summary>
        Down,

        /// <summary>
        /// The up arrow key.
        /// </summary>
        Up,

        /// <summary>
        /// The page up key.
        /// </summary>
        PageUp,

        /// <summary>
        /// The page down key.
        /// </summary>
        PageDown,

        /// <summary>
        /// The home key.
        /// </summary>
        Home,

        /// <summary>
        /// The end key.
        /// </summary>
        End,

        /// <summary>
        /// The caps lock key.
        /// </summary>
        CapsLock,

        /// <summary>
        /// The scroll lock key.
        /// </summary>
        ScrollLock,

        /// <summary>
        /// The num lock key.
        /// </summary>
        NumLock,

        /// <summary>
        /// The print screen key.
        /// </summary>
        PrintScreen,

        /// <summary>
        /// The pause key.
        /// </summary>
        Pause,

        /// <summary>
        /// The F1 key.
        /// </summary>
        F1,

        /// <summary>
        /// The F2 key.
        /// </summary>
        F2,

        /// <summary>
        /// The F3 key.
        /// </summary>
        F3,

        /// <summary>
        /// The F4 key.
        /// </summary>
        F4,

        /// <summary>
        /// The F5 key.
        /// </summary>
        F5,

        /// <summary>
        /// The F6 key.
        /// </summary>
        F6,

        /// <summary>
        /// The F7 key.
        /// </summary>
        F7,

        /// <summary>
        /// The F8 key.
        /// </summary>
        F8,

        /// <summary>
        /// The F9 key.
        /// </summary>
        F9,

        /// <summary>
        /// The F10 key.
        /// </summary>
        F10,

        /// <summary>
        /// The F11 key.
        /// </summary>
        F11,

        /// <summary>
        /// The F12 key.
        /// </summary>
        F12,

        /// <summary>
        /// The F13 key.
        /// </summary>
        F13,

        /// <summary>
        /// The F14 key.
        /// </summary>
        F14,

        /// <summary>
        /// The F15 key.
        /// </summary>
        F15,

        /// <summary>
        /// The F16 key.
        /// </summary>
        F16,

        /// <summary>
        /// The F17 key.
        /// </summary>
        F17,

        /// <summary>
        /// The F18 key.
        /// </summary>
        F18,

        /// <summary>
        /// The F19 key.
        /// </summary>
        F19,

        /// <summary>
        /// The F20 key.
        /// </summary>
        F20,

        /// <summary>
        /// The F21 key.
        /// </summary>
        F21,

        /// <summary>
        /// The F22 key.
        /// </summary>
        F22,

        /// <summary>
        /// The F23 key.
        /// </summary>
        F23,

        /// <summary>
        /// The F24 key.
        /// </summary>
        F24,

        /// <summary>
        /// The F25 key.
        /// </summary>
        F25,

        /// <summary>
        /// The 0 key on the key pad.
        /// </summary>
        KeyPad0,

        /// <summary>
        /// The 1 key on the key pad.
        /// </summary>
        KeyPad1,

        /// <summary>
        /// The 2 key on the key pad.
        /// </summary>
        KeyPad2,

        /// <summary>
        /// The 3 key on the key pad.
        /// </summary>
        KeyPad3,

        /// <summary>
        /// The 4 key on the key pad.
        /// </summary>
        KeyPad4,

        /// <summary>
        /// The 5 key on the key pad.
        /// </summary>
        KeyPad5,

        /// <summary>
        /// The 6 key on the key pad.
        /// </summary>
        KeyPad6,

        /// <summary>
        /// The 7 key on the key pad.
        /// </summary>
        KeyPad7,

        /// <summary>
        /// The 8 key on the key pad.
        /// </summary>
        KeyPad8,

        /// <summary>
        /// The 9 key on the key pad.
        /// </summary>
        KeyPad9,

        /// <summary>
        /// The decimal key on the key pad.
        /// </summary>
        KeyPadDecimal,

        /// <summary>
        /// The divide key on the key pad.
        /// </summary>
        KeyPadDivide,

        /// <summary>
        /// The multiply key on the key pad.
        /// </summary>
        KeyPadMultiply,

        /// <summary>
        /// The subtract key on the key pad.
        /// </summary>
        KeyPadSubtract,

        /// <summary>
        /// The add key on the key pad.
        /// </summary>
        KeyPadAdd,

        /// <summary>
        /// The enter key on the key pad.
        /// </summary>
        KeyPadEnter,

        /// <summary>
        /// The equal key on the key pad.
        /// </summary>
        KeyPadEqual,

        /// <summary>
        /// The left shift key.
        /// </summary>
        LeftShift,

        /// <summary>
        /// The left control key.
        /// </summary>
        LeftControl,

        /// <summary>
        /// The left alt key.
        /// </summary>
        LeftAlt,

        /// <summary>
        /// The left super key.
        /// </summary>
        LeftSuper,

        /// <summary>
        /// The right shift key.
        /// </summary>
        RightShift,

        /// <summary>
        /// The right control key.
        /// </summary>
        RightControl,

        /// <summary>
        /// The right alt key.
        /// </summary>
        RightAlt,

        /// <summary>
        /// The right super key.
        /// </summary>
        RightSuper,

        /// <summary>
        /// The menu key.
        /// </summary>
        Menu,

        /// <summary>
        /// The last valid key in this enum.
        /// </summary>
        LastKey = Menu
    }
}
