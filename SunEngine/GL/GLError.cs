// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
namespace SunEngine.GL
{
    public enum GLError
    {
        /// <summary>
        /// No error has been recorded. The value of this symbolic constant is guaranteed to be 0.
        /// </summary>
        NoError,
        /// <summary>
        /// An unacceptable value is specified for an enumerated argument. The offending command is ignored and has no other side effect than to set the error flag.
        /// </summary>
        InvalidEnum,
        /// <summary>
        /// A numeric argument is out of range. The offending command is ignored and has no other side effect than to set the error flag.
        /// </summary>
        InvalidValue,
        /// <summary>
        /// The specified operation is not allowed in the current state. The offending command is ignored and has no other side effect than to set the error flag.
        /// </summary>
        InvalidOperation,
        /// <summary>
        /// The framebuffer object is not complete. The offending command is ignored and has no other side effect than to set the error flag.
        /// </summary>
        InvalidFramebufferOperation,
        /// <summary>
        /// There is not enough memory left to execute the command. The state of the GL is undefined, except for the state of the error flags, after this error is recorded.
        /// </summary>
        OutOfMemory,
        /// <summary>
        /// An attempt has been made to perform an operation that would cause an internal stack to underflow.
        /// </summary>
        StackUnderflow,
        /// <summary>
        /// An attempt has been made to perform an operation that would cause an internal stack to overflow.
        /// </summary>
        StackOverflow
    }
}
