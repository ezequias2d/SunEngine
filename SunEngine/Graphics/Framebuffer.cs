// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/. --%>
using SunEngine.GL;

namespace SunEngine.Graphics
{
    public sealed class Framebuffer : GraphicObject
    {
        public Framebuffer(IGL gl) : base(gl, gl.GenFramebuffer())
        {
            
        }

        public void Attach(GpuTexture texture, FramebufferAttachment attachment, int level = 0)
        {
            Bind(FramebufferTarget.Framebuffer);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, attachment, TextureTarget.Texture2D, texture.Handle, level);
            GL.CheckErros();
            Unbind(FramebufferTarget.Framebuffer);
        }

        public void Attach(Renderbuffer renderbuffer, FramebufferAttachment attachment)
        {
            Bind(FramebufferTarget.Framebuffer);
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, attachment, RenderbufferTarget.Renderbuffer, renderbuffer.Handle);
            Unbind(FramebufferTarget.Framebuffer);
        }

        public void Bind(FramebufferTarget target)
        {
            GL.BindFramebuffer(target, Handle);
            GL.CheckErros();
        }

        public void Unbind(FramebufferTarget target)
        {
            GL.BindFramebuffer(target, 0);
            GL.CheckErros();
        }

        protected override void Dispose(bool disposing)
        {
            GL.DeleteFramebuffer(Handle);
        }
    }
}
