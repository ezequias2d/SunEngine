// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using Assimp;
using SunEngine.GL;
using SunEngine.Data.Meshes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Numerics;
using AssimpMesh = Assimp.Mesh;
using SunMesh = SunEngine.Data.Meshes.Mesh;
using StbImageSharp;

namespace SunEngine.Data.Loader
{
    public static class SunLoader
    {
        #region texture loader
        public static Texture LoadTexture(string filename) => LoadTexture(File.OpenRead(filename));

        public static Texture LoadTexture(Stream stream)
        {
            StbImage.stbi_set_flip_vertically_on_load(1);
            var image = ImageResult.FromStream(stream);
            
            Texture texture = default;
            texture.PixelType = PixelType.UnsignedByte;
            switch(image.Comp)
            {
                case ColorComponents.RedGreenBlue:
                    texture.PixelFormat = PixelFormat.Rgb;
                    break;
                case ColorComponents.RedGreenBlueAlpha:
                    texture.PixelFormat = PixelFormat.Rgba;
                    break;
            }
            texture.Width = image.Width;
            texture.Height = image.Height;

            texture.Data = new byte[GetPixelSize(texture.PixelFormat) * texture.Width * texture.Height];

            Array.Copy(image.Data, texture.Data, image.Data.Length);

            return texture;
        }

        private static int GetPixelSize(GL.PixelFormat pixelFormat)
        {
            switch (pixelFormat)
            {
                case GL.PixelFormat.Rgba:
                    return 4 * sizeof(byte);
                case GL.PixelFormat.Rgb:
                    return 3 * sizeof(byte);
                case GL.PixelFormat.Red:
                    return sizeof(byte);
                default:
                    throw new FormatException("GL.PixelFormat is not supported.");
            }
        }
        #endregion

        #region mesh loader

        public static SunMesh[] LoadMesh(string filename) => LoadMesh(File.OpenRead(filename));

        public static SunMesh[] LoadMesh(Stream stream)
        {
            AssimpContext importer = new AssimpContext();

            PostProcessSteps steps = PostProcessSteps.Triangulate | PostProcessSteps.GenerateSmoothNormals | PostProcessSteps.JoinIdenticalVertices;
            Scene scene = importer.ImportFileFromStream(stream, steps);

            return InitFromScene(scene);
        }

        private static SunMesh[] InitFromScene(Scene assimpScene)
        {
            List<SunMesh> meshes = new List<SunMesh>();
            foreach (AssimpMesh assimpMesh in assimpScene.Meshes)
                meshes.Add(InitMesh(assimpMesh));

            return meshes.ToArray();
        }

        private static SunMesh InitMesh(AssimpMesh assimpMesh)
        {
            Span<Vector3> vertices = new Vector3[assimpMesh.VertexCount];
            Span<Vector3> normals = new Vector3[assimpMesh.VertexCount];
            Span<Vector2> uvs = new Vector2[assimpMesh.VertexCount];

            for (int i = 0; i < assimpMesh.VertexCount; i++)
            {
                Vector3D position = assimpMesh.Vertices[i];
                Vector3D normal = assimpMesh.Normals[i];
                Vector3D uv = assimpMesh.HasTextureCoords(0) ? assimpMesh.TextureCoordinateChannels[0][i] : default;

                vertices[i] = new Vector3(position.X, position.Y, position.Z);
                normals[i] = new Vector3(normal.X, normal.Y, normal.Z);
                uvs[i] = new Vector2(uv.X, uv.Y);
            }

            Span<TriangleFace> faces = stackalloc TriangleFace[assimpMesh.FaceCount];
            for (int i = 0; i < assimpMesh.FaceCount; i++)
            {
                Face face = assimpMesh.Faces[i];
                Debug.Assert(face.IndexCount == 3);
                faces[i] = new TriangleFace((uint)face.Indices[0], (uint)face.Indices[1], (uint)face.Indices[2]);
            }

            return new SunMesh(assimpMesh.Name, vertices, uvs, normals, faces);
        }
        #endregion 
    }
}
