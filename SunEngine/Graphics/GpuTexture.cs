// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using SunEngine.Data;
using SunEngine.GL;

namespace SunEngine.Graphics
{
    public class GpuTexture : GraphicObject
    {
        public GpuTexture(IGL gl, Texture texture, PixelInternalFormat internalFormat) : base(gl, gl.GenTexture())
        {
            Bind();
            GL.TexImage2D<byte>(TextureTarget.Texture2D, 0, internalFormat, texture.Width, texture.Height, 0, texture.PixelFormat, texture.PixelType, texture.Data);
            GL.CheckErros();

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, TextureMinFilter.LinearMipmapLinear);
            GL.CheckErros();

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, TextureMagFilter.Linear);
            GL.CheckErros();

            GL.GenerateMipmap(TextureTarget.Texture2D);
            GL.CheckErros();
        }

        public void Bind(int textureUnit = 0)
        {
            GL.ActiveTexture(textureUnit);
            GL.CheckErros();
            GL.BindTexture(TextureTarget.Texture2D, Handle);
            GL.CheckErros();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                GL.DeleteTexture(Handle);
                GL.CheckErros();
            }
        }
    }
}
