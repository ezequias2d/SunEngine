// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/. --%>
using SunEngine.GL;

namespace SunEngine.Graphics
{
    public class VertexArray : GraphicObject
    {
        public VertexArray(IGL gl) : base(gl, gl.GenVertexArray())
        {
            
        }

        public void Bind()
        {
            GL.BindVertexArray(Handle);
        }

        public void DrawTriangles(int first, int count)
        {
            Bind();
            GL.DrawArrays(PrimitiveType.Triangles, first, count);
        }

        public void DrawElementsTriangles(int first, int count)
        {
            Bind();
            GL.DrawElements(PrimitiveType.Triangles, count, DrawElementsType.UnsignedInt, first);
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
                GL.DeleteVertexArray(Handle);
        }
    }
}
