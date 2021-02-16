// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/. --%>
using System;
using System.Numerics;

namespace SunEngine.Data.Meshes
{
    /// <summary>
    /// A mesh represents a geometry or model with a single material.
    /// </summary>
    public struct Mesh
    {
        /// <summary>
        /// Name of mesh.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Positions of each vertex.
        /// </summary>
        public Vector3[] Vertices { get; set; }

        /// <summary>
        /// Texture coordinates of each vertex.
        /// </summary>
        public Vector2[] UVs { get; set; }

        /// <summary>
        /// Faces of each vertex.
        /// </summary>
        public TriangleFace[] Faces { get; set; }

        /// <summary>
        /// Normals of each vertex.
        /// </summary>
        public Vector3[] Normals { get; set; }

        /// <summary>
        /// Vertex tangents.
        /// 
        /// The tangent of a vertex points in the direction of the positive X texture axis. 
        /// The array contains normalized vectors, null if not present.
        /// The array, when it exists, is the same size as the vertices.
        /// </summary>
        public Vector3[] Tangents { get; set; }

        /// <summary>
        /// Vertex bitangents.
        /// </summary>
        public Vector3[] Bitangents { get; set; }

        public Mesh(string name, ReadOnlySpan<Vector3> vertices, ReadOnlySpan<Vector2> uvs, ReadOnlySpan<Vector3> normals, ReadOnlySpan<TriangleFace> faces) 
            : this(name, vertices, uvs, normals, faces, ReadOnlySpan<Vector3>.Empty, ReadOnlySpan<Vector3>.Empty)
        {

        }

        /// <summary>
        /// Initialize a new instance of <see cref="Mesh"/> with parameters.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="vertices"></param>
        /// <param name="uvs"></param>
        /// <param name="normals"></param>
        /// <param name="faces"></param>        
        /// <param name="tangents"></param>
        /// <param name="bitangents"></param>
        public Mesh(string name, ReadOnlySpan<Vector3> vertices, 
                                ReadOnlySpan<Vector2> uvs, 
                                ReadOnlySpan<Vector3> normals, 
                                ReadOnlySpan<TriangleFace> faces, 
                                ReadOnlySpan<Vector3> tangents, 
                                ReadOnlySpan<Vector3> bitangents)
        {
            Name = name;

            Vertices = vertices.ToArray();
            UVs = uvs.ToArray();
            Normals = normals.ToArray();
            Faces = faces.ToArray();
            Tangents = tangents.ToArray();
            Bitangents = bitangents.ToArray();
        }

    }
}
