// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/. --%>

namespace SunEngine.Inputs
{
    public enum MouseButtons
    {
        None,
        /// <summary>
        ///     The first button.
        /// </summary>
        Button1,

        /// <summary>
        ///     The second button.
        /// </summary>
        Button2,

        /// <summary>
        ///     The third button.
        /// </summary>
        Button3,

        /// <summary>
        ///     The fourth button.
        /// </summary>
        Button4,

        /// <summary>
        ///     The fifth button.
        /// </summary>
        Button5,

        /// <summary>
        ///     The sixth button.
        /// </summary>
        Button6,

        /// <summary>
        ///     The seventh button.
        /// </summary>
        Button7,

        /// <summary>
        ///     The eighth button.
        /// </summary>
        Button8,


        /// <summary>
        ///     The highest mouse button available.
        /// </summary>
        Last = Button8,

        /// <summary>
        ///     The left mouse button. This corresponds to <see cref="Button1"/>.
        /// </summary>
        Left = Button1,

        /// <summary>
        ///     The right mouse button. This corresponds to <see cref="Button2"/>.
        /// </summary>
        Right = Button2,

        /// <summary>
        ///     The middle mouse button. This corresponds to <see cref="Button3"/>.
        /// </summary>
        Middle = Button3
    }
}
