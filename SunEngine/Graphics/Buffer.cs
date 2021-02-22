// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using SunEngine.GL;
using System;

namespace SunEngine.Graphics
{
    public class Buffer<T> : GraphicObject where T : unmanaged
    {
        private readonly BufferTarget bufferTarget;
        private readonly BufferUsage _bufferUsage;

        public readonly int Count;

        public T[] Content
        {
            get
            {
                T[] content = new T[Count];
                GL.BindBuffer(BufferTarget.ArrayBuffer, Handle);
                GL.GetBufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, (Span<T>)content);
                return content;
            }
        }

        public Buffer(IGL gl, Span<T> data, BufferTarget target, BufferUsage usage) : base(gl, gl.GenBuffer())
        {
            Count = data.Length;
            bufferTarget = target;
            _bufferUsage = usage;

            GL.BindBuffer(bufferTarget, Handle);
            GL.CheckErros();

            GL.BufferData(bufferTarget, data, _bufferUsage);
            GL.CheckErros();
        }

        public void SubData<U>(ReadOnlySpan<U> data, long offset) where U : unmanaged
        {
            Bind();
            GL.BufferSubData(bufferTarget, new IntPtr(offset), data);
            GL.CheckErros();
        }

        public void SubData<U>(U data, long offset) where U : unmanaged
        {
            unsafe
            {
                SubData(new ReadOnlySpan<U>(&data, 1), offset);
            }
        }

        public void Bind()
        {
            GL.BindBuffer(bufferTarget, Handle);
            GL.CheckErros();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                GL.DeleteBuffer(Handle);
                GL.CheckErros();
            }
        }
    }
}
