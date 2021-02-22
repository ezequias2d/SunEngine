// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System.Numerics;

namespace SunEngine.Inputs
{
    public interface IMouseState
    {
        /// <summary>
        /// Gets a <see cref="Vector2"/> representing the absolute position of the pointer
        /// in the current frame, relative to the top-left corner of the contents of the window.
        /// </summary>
        Vector2 Position { get; }

        /// <summary>
        /// Gets a <see cref="Vector2"/> representing the absolute position of the pointer
        /// in the previous frame, relative to the top-left corner of the contents of the window.
        /// </summary>
        Vector2 PreviousPosition { get; }

        /// <summary>
        /// Gets a <see cref="Vector2"/> representing the amount that the mouse moved since the last frame.
        /// This does not necessarily correspond to pixels, for example in the case of raw input.
        /// </summary>
        Vector2 Delta { get; }

        /// <summary>
        /// Get a Vector2 representing the position of the mouse wheel.
        /// </summary>
        Vector2 Scroll { get; }

        /// <summary>
        /// Get a Vector2 representing the position of the mouse wheel.
        /// </summary>
        Vector2 PreviousScroll { get; }

        /// <summary>
        /// Get a Vector2 representing the amount that the mouse wheel moved since the last frame.
        /// </summary>
        Vector2 ScrollDelta { get; }

        /// <summary>
        /// Gets a <see cref="KeyEvent" /> indicating whether the specified
        ///  <see cref="MouseButtons" /> is up, down or pressed.
        /// </summary>
        /// <param name="button">The <see cref="MouseButtons" /> to check.</param>
        /// <returns> The <see cref="KeyEvent"/> of the <see cref="MouseButtons"/>.</returns>
        KeyEvent this[MouseButtons button] { get; }

        /// <summary>
        /// Gets an integer representing the absolute x position of the pointer, in window pixel coordinates.
        /// </summary>
        float X { get; }

        /// <summary>
        /// Gets an integer representing the absolute y position of the pointer, in window pixel coordinates.
        /// </summary>
        float Y { get; }

        /// <summary>
        /// Gets an integer representing the absolute x position of the pointer, in window pixel coordinates.
        /// </summary>
        float PreviousX { get; }

        /// <summary>
        /// Gets an integer representing the absolute y position of the pointer, in window pixel coordinates.
        /// </summary>
        float PreviousY { get; }

        bool IsAnyButtonDown { get; }
        bool IsAnyButtonRepeat { get; }
        bool IsAnyButtonUp { get; }

        KeyEvent GetButton(MouseButtons button);
        KeyEvent GetPreviousButton(MouseButtons button);
        IMouseState GetSnapshot();
    }
}
