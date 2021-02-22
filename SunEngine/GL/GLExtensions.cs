// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Diagnostics;

namespace SunEngine.GL
{
    public static class GLExtensions
    {
        [Conditional("DEBUG")]
        [DebuggerNonUserCode]
        public static void CheckErros(this IGL gl)
        {
            GLError error = gl.GetError();
            if (error != GLError.NoError)
            {
                Console.WriteLine($"OpenGL Error: {error}");
                throw new GLException(error.ToString());
            }
        }
    }
}
