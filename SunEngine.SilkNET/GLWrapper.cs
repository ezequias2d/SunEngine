// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/. --%>
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using SunEngine.GL;
using SunEngine.SilkNET;
using System;
using System.Drawing;
using System.Numerics;

namespace SunEngine
{
    public sealed class GLWrapper : IGL
    {
        private readonly Silk.NET.OpenGL.GL _gl;
        internal GLWrapper(IWindow window)
        {
            _gl = window.CreateOpenGL();
        }

        public void ActiveTexture(int textureUnit) =>
            _gl.ActiveTexture(TextureUnit.Texture0 + textureUnit);

        public void AttachShader(int program, int shader) =>
            _gl.AttachShader((uint)program, (uint)shader);

        public void BindBuffer(BufferTarget target, int buffer) =>
            _gl.BindBuffer(target.ToSilk(), (uint)buffer);

        public void BindBufferBase(BufferTarget target, int index, int buffer) =>
            _gl.BindBufferBase(target.ToSilk(), (uint)index, (uint)buffer);

        public void BindBufferRange(BufferTarget target, int index, int buffer, IntPtr offset, UIntPtr size) =>
            _gl.BindBufferRange(target.ToSilk(), (uint)index, (uint)buffer, offset, size);

        public void BindFramebuffer(GL.FramebufferTarget target, int framebuffer)
        {
            _gl.BindFramebuffer(target.ToSilk(), (uint)framebuffer);
        }

        public void BindRenderbuffer(GL.RenderbufferTarget target, int renderbuffer)
        {
            _gl.BindRenderbuffer(target.ToSilk(), (uint)renderbuffer);
        }

        public void BindTexture(GL.TextureTarget target, int texture) =>
            _gl.BindTexture(target.ToSilk(), (uint)texture);

        public void BindVertexArray(int array) =>
            _gl.BindVertexArray((uint)array);

        public void BlitFramebuffer(Rectangle src, Rectangle dst, ClearMask mask, GL.BlitFramebufferFilter filter) =>
            _gl.BlitFramebuffer(src.Left, src.Top, src.Right, src.Bottom, dst.Left, dst.Top, dst.Right, dst.Bottom, (uint)mask.ToSilk(), filter.ToSilk());

        public void BufferData<T>(BufferTarget target, Span<T> data, BufferUsage usage) where T : unmanaged
        {
            unsafe
            {
                fixed (void* ptr = data)
                    _gl.BufferData(target.ToSilk(), (uint)(sizeof(T) * data.Length), ptr, usage.ToSilk());
            }
        }

        public void BufferSubData<T>(BufferTarget target, IntPtr offset, ReadOnlySpan<T> data) where T : unmanaged
        {
            unsafe
            {
                fixed (void* ptr = data)
                    _gl.BufferSubData(target.ToSilk(), offset, (uint)(sizeof(T) * data.Length), ptr);
            }
        }

        public GL.FramebufferStatus CheckFramebufferStatus(GL.FramebufferTarget target)
        {
            return _gl.CheckFramebufferStatus(target.ToSilk()).ToSunFramebufferStatus();
        }

        public void Clear(ClearMask mask) =>
            _gl.Clear((uint)mask.ToSilk());

        public void ClearColor(float red, float green, float blue, float alpha) =>
            _gl.ClearColor(red, green, blue, alpha);

        public void ClearColor(Color color) =>
            _gl.ClearColor(color);

        public void ClearColor(Vector4 color) =>
            _gl.ClearColor(color.X, color.Y, color.Z, color.W);

        public void CompileShader(int shader) =>
            _gl.CompileShader((uint)shader);

        public int CreateProgram() => (int)_gl.CreateProgram();

        public int CreateShader(GL.ShaderType type) => (int)_gl.CreateShader(type.ToSilk());

        public void DeleteBuffer(int buffer) => _gl.DeleteBuffer((uint)buffer);

        public void DeleteBuffers(Span<int> buffers)
        {
            unsafe
            {
                fixed(void* ptr = buffers)
                    _gl.DeleteBuffers((uint)buffers.Length, (uint*)ptr);
            }
        }

        public void DeleteFramebuffer(int framebuffer)
        {
            _gl.DeleteFramebuffer((uint)framebuffer);
        }

        public void DeleteProgram(int program) => 
            _gl.DeleteProgram((uint)program);

        public void DeleteRenderbuffer(int renderbuffer)
        {
            _gl.DeleteRenderbuffer((uint)renderbuffer);
        }

        public void DeleteShader(int shader) => 
            _gl.DeleteShader((uint)shader);

        public void DeleteTexture(int texture) => 
            _gl.DeleteTexture((uint)texture);

        public void DeleteVertexArray(int array) => 
            _gl.DeleteVertexArray((uint)array);

        public void DetachShader(int program, int shader) => 
            _gl.DetachShader((uint)program, (uint)shader);

        public void DrawArrays(GL.PrimitiveType mode, int first, int count)
        {
            _gl.DrawArrays(mode.ToSilk(), first, (uint)count);
        }

        public void DrawElements(GL.PrimitiveType mode, int count, GL.DrawElementsType type, int offset)
        {
            unsafe
            {
                _gl.DrawElements(mode.ToSilk(), (uint)count, type.ToSilk(), (void*)offset);
            }
        }

        public void Enable(GL.EnableCap cap)
        {
            _gl.Enable(cap.ToSilk());
        }

        public void EnableVertexAttribArray(uint index) =>
            _gl.EnableVertexAttribArray(index);

        public void FramebufferRenderbuffer(GL.FramebufferTarget target, GL.FramebufferAttachment attachment, GL.RenderbufferTarget renderbufferTarget, int renderbuffer)
        {
            _gl.FramebufferRenderbuffer(target.ToSilk(), attachment.ToSilk(), renderbufferTarget.ToSilk(), (uint)renderbuffer);
        }

        public void FramebufferTexture2D(GL.FramebufferTarget target, GL.FramebufferAttachment attachment, GL.TextureTarget textureTarget, int texture, int level)
        {
            _gl.FramebufferTexture2D((GLEnum)target.ToSilk(), (GLEnum)attachment.ToSilk(), textureTarget.ToSilk(), (uint)texture, level);
        }

        public int GenBuffer() =>
            (int)_gl.GenBuffer();

        public void GenBuffers(Span<int> buffers)
        {
            unsafe
            {
                fixed (void* ptr = buffers)
                    _gl.GenBuffers((uint)buffers.Length, (uint*)ptr);
            }
        }

        public void GenerateMipmap(GL.TextureTarget target) =>
            _gl.GenerateMipmap(target.ToSilk());

        public int GenFramebuffer()
        {
            return (int)_gl.GenFramebuffer();
        }

        public int GenRenderbuffer()
        {
            return (int)_gl.GenRenderbuffer();
        }

        public int GenTexture() =>
            (int)_gl.GenTexture();

        public int GenVertexArray() =>
            (int)_gl.GenVertexArray();

        public void GetBufferSubData<T>(BufferTarget target, IntPtr offset, Span<T> data) where T : unmanaged
        {
            unsafe
            {
                fixed (void* ptr = data)
                    _gl.GetBufferSubData(target.ToSilk(), offset, (uint)(data.Length * sizeof(T)), ptr);
            }
        }

        public GLError GetError() =>
            _gl.GetError().ToSunGLError();

        public string GetProgramInfoLog(int program)
        {
            return _gl.GetProgramInfoLog((uint)program);
        }

        public int GetUniformBlockIndex(int program, string uniformBlockName) =>
            (int)_gl.GetUniformBlockIndex((uint)program, uniformBlockName);

        public void LinkProgram(int program) =>
            _gl.LinkProgram((uint)program);

        public void RenderbufferStorage(GL.RenderbufferTarget target, PixelInternalFormat internalFormat, int width, int height)
        {
            _gl.RenderbufferStorage(target.ToSilk(), internalFormat.ToSilk(), (uint)width, (uint)height);
        }

        public void RenderbufferStorageMultisample(GL.RenderbufferTarget target, int samples, PixelInternalFormat internalFormat, int width, int height)
        {
            _gl.RenderbufferStorageMultisample(target.ToSilk(), (uint)samples, internalFormat.ToSilk(), (uint)width, (uint)height);
        }

        public void ShaderSource(int shader, string sourceCode) =>
            _gl.ShaderSource((uint)shader, sourceCode);

        public void TexImage2D<T>(GL.TextureTarget target, int level, PixelInternalFormat internalFormat, int width, int height, int border, GL.PixelFormat pixelFormat, GL.PixelType type, ReadOnlySpan<T> pixels) where T : unmanaged =>
            _gl.TexImage2D(target.ToSilk(), level, (int)internalFormat.ToSilk(), (uint)width, (uint)height, border, pixelFormat.ToSilk(), type.ToSilk(), pixels);

        public void TexImage2DMultisample(GL.TextureTarget target, int samples, PixelInternalFormat internalFormat, int width, int height, bool fixedSampleLocations)
        {
            _gl.TexImage2DMultisample(target.ToSilk(), (uint)samples, internalFormat.ToSilk(), (uint)width, (uint)height, fixedSampleLocations);
        }

        public void TexParameter(GL.TextureTarget target, GL.TextureParameterName pname, float param) =>
            _gl.TexParameter(target.ToSilk(), pname.ToSilk(), param);

        public void TexParameter(GL.TextureTarget target, GL.TextureParameterName pname, ReadOnlySpan<float> @params) =>
            _gl.TexParameter(target.ToSilk(), pname.ToSilk(), @params);

        public void TexParameter(GL.TextureTarget target, GL.TextureParameterName pname, int param) =>
            _gl.TexParameter(target.ToSilk(), pname.ToSilk(), param);

        public void TexParameter(GL.TextureTarget target, GL.TextureParameterName pname, ReadOnlySpan<int> @params) =>
            _gl.TexParameter(target.ToSilk(), pname.ToSilk(), @params);

        public void TexParameter(GL.TextureTarget target, GL.TextureParameterName pname, GL.TextureMinFilter param) =>
            _gl.TexParameter(target.ToSilk(), pname.ToSilk(), (int)param.ToSilk());

        public void TexParameter(GL.TextureTarget target, GL.TextureParameterName pname, GL.TextureMagFilter param) =>
            _gl.TexParameter(target.ToSilk(), pname.ToSilk(), (int)param.ToSilk());

        public void UniformBlockBinding(int program, int uniformBlockIndex, int uniformBlockBinding) =>
            _gl.UniformBlockBinding((uint)program, (uint)uniformBlockIndex, (uint)uniformBlockBinding);

        public void UseProgram(int program) =>
            _gl.UseProgram((uint)program);

        public void ValidateProgram(int program)
        {
            _gl.ValidateProgram((uint)program);
        }

        public void VertexAttribPointer(uint index, int size, GL.VertexAttribPointerType type, bool normalized, int stride, int offset)
        {
            unsafe
            {
                _gl.VertexAttribPointer(index, size, type.ToSilk(), normalized, (uint)stride, (void*)offset);
            }
        }

    }
}
