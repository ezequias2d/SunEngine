// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using SunEngine.Data.Meshes;
using SunEngine.GL;
using System;
using System.Numerics;

namespace SunEngine.Graphics
{
    public class Model : ResourceObject
    {
        private readonly Buffer<float> vertexBuffer;
        private readonly Buffer<TriangleFace> elementBuffer;
        private readonly VertexArray vertexArray;

        private readonly int _indiceCount;
        private readonly int _VerticesCount;

        public Model(IGL gl, Mesh mesh) : base(gl)
        {
            _indiceCount = (mesh.Faces?.Length ?? 0) * 3;
            _VerticesCount = (mesh.Vertices?.Length ?? 0);

            vertexArray = new VertexArray(GL);
            vertexArray.Bind();


            int size = 0;
            // calculate the size of data.
            {
                size += GetSize(mesh.Vertices);
                size += GetSize(mesh.UVs);
                size += GetSize(mesh.Normals);
                size += GetSize(mesh.Tangents);
                size += GetSize(mesh.Bitangents);
            }
            Span<float> data = new float[size / sizeof(float)];

            // copies the data from mesh to the data span.
            {
                int offset = 0;
                if (mesh.Vertices != null && mesh.Vertices.Length != 0)
                    offset += Copy<Vector3, float>(data.Slice(offset), mesh.Vertices) / sizeof(float);

                if (mesh.UVs != null && mesh.Vertices.Length != 0)
                    offset += Copy<Vector2, float>(data.Slice(offset), mesh.UVs) / sizeof(float);

                if (mesh.Normals != null && mesh.Vertices.Length != 0)
                    offset += Copy<Vector3, float>(data.Slice(offset), mesh.Normals) / sizeof(float);

                if (mesh.Tangents != null && mesh.Vertices.Length != 0)
                    offset += Copy<Vector3, float>(data.Slice(offset), mesh.Tangents) / sizeof(float);

                if (mesh.Bitangents != null && mesh.Vertices.Length != 0)
                    Copy<Vector3, float>(data.Slice(offset), mesh.Bitangents);
            }

            vertexBuffer = new Buffer<float>(GL, data, BufferTarget.ArrayBuffer, BufferUsage.StaticDraw);
            vertexBuffer.Bind();

            // configure vertex array.
            {
                int offset = 0;
                uint index = 0;
                if (mesh.Vertices != null && mesh.Vertices.Length != 0)
                    VertexAttribPointer<Vector3>(ref index, ref offset, mesh.Vertices);

                if (mesh.UVs != null && mesh.Vertices.Length != 0)
                    VertexAttribPointer<Vector2>(ref index, ref offset, mesh.UVs);

                if (mesh.Normals != null && mesh.Vertices.Length != 0)
                    VertexAttribPointer<Vector3>(ref index, ref offset, mesh.Normals);

                if (mesh.Tangents != null && mesh.Vertices.Length != 0)
                    VertexAttribPointer<Vector3>(ref index, ref offset, mesh.Tangents);

                if (mesh.Bitangents != null && mesh.Vertices.Length != 0)
                    VertexAttribPointer<Vector3>(ref index, ref offset, mesh.Bitangents);
            }

            if (mesh.Faces != null)
                elementBuffer = new Buffer<TriangleFace>(GL, mesh.Faces, BufferTarget.ElementArrayBuffer, BufferUsage.StaticDraw);
            else
                elementBuffer = null;
        }

        public void Draw()
        {
            vertexArray.Bind();

            if (_indiceCount > 0)
                vertexArray.DrawElementsTriangles(0, _indiceCount);
            else
                vertexArray.DrawTriangles(0, _VerticesCount);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                vertexBuffer.Dispose();
                elementBuffer.Dispose();
            }
        }

        private unsafe void VertexAttribPointer<T>(ref uint index, ref int offset, ReadOnlySpan<T> data) where T : unmanaged
        {
            if(data.Length != 0)
            {
                GL.VertexAttribPointer(index, sizeof(T) / sizeof(float), VertexAttribPointerType.Float, false, sizeof(T), offset);
                GL.CheckErros();

                GL.EnableVertexAttribArray(index);
                GL.CheckErros();
                offset += sizeof(T) * data.Length;
            }
            index++;
        }

        private static unsafe int Copy<S, D>(Span<D> dst, Span<S> src) where S : unmanaged where D : unmanaged
        {
            fixed(void* dstPtr = dst, srcPtr = src)
            {
                int srcSize = src.Length * sizeof(S);
                int dstSize = dst.Length * sizeof(D);
                Buffer.MemoryCopy(srcPtr, dstPtr, dstSize, srcSize);
                return srcSize;
            }
        }

        public unsafe static int GetSize<T>(ReadOnlySpan<T> span) where T : unmanaged =>
            span.Length * sizeof(T);

        public unsafe static int GetSize<T>(T[] data) where T : unmanaged =>
            (data?.Length ?? 0) * sizeof(T);
    }
}

