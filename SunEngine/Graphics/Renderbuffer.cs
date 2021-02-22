// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using SunEngine.GL;
using System.Drawing;

namespace SunEngine.Graphics
{
    public sealed class Renderbuffer : GraphicObject
    {
        public Renderbuffer(IGL gl, PixelInternalFormat internalFormat, Size size) : base(gl, gl.GenRenderbuffer())
        {
            Bind();
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, internalFormat, size.Width, size.Height);
            Unbind();
        }

        public void Bind()
        {
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, Handle);
            GL.CheckErros();
        }

        public void Unbind()
        {
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer, 0);
            GL.CheckErros();
        }

        protected override void Dispose(bool disposing)
        {
            GL.DeleteRenderbuffer(Handle);
        }
    }
}
