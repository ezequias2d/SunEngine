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
using System.Drawing.Imaging;

namespace SunEngine.Data.Loader
{
    public static class SunLoader
    {
        #region texture loader
        public static Texture LoadTexture(string filename) => LoadTexture(File.OpenRead(filename));

        public static Texture LoadTexture(Stream stream)
        {
            Bitmap bitmap = new Bitmap(stream);
            Texture texture = default;
            texture.PixelType = PixelType.UnsignedByte;
            
            switch(bitmap.PixelFormat)
            {
                case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                case System.Drawing.Imaging.PixelFormat.Format8bppIndexed:
                    texture.PixelFormat = GL.PixelFormat.Rgba;
                    break;
                case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
                case System.Drawing.Imaging.PixelFormat.Format32bppRgb:
                    texture.PixelFormat = GL.PixelFormat.Rgb;
                    break;
                case System.Drawing.Imaging.PixelFormat.Format16bppGrayScale:
                    texture.PixelFormat = GL.PixelFormat.Red;
                    break;
                default:
                    throw new InvalidDataException();
            }

            texture.Width = bitmap.Width;
            texture.Height = bitmap.Height;

            texture.Data = new byte[GetPixelSize(texture.PixelFormat) * texture.Width * texture.Height];

            ReadPixels(bitmap, texture.PixelFormat, texture.Data);

            return texture;
        }

        private static unsafe void ReadPixels(Bitmap bitmap, GL.PixelFormat pixelFormat, Span<byte> dstSpan)
        {
            int pixelSizeDst = GetPixelSize(pixelFormat);

            int pixelSizeSrc;
            switch (bitmap.PixelFormat)
            {
                case System.Drawing.Imaging.PixelFormat.Format32bppRgb:
                case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                    pixelSizeSrc = 4;
                    break;
                case System.Drawing.Imaging.PixelFormat.Format16bppGrayScale:
                    pixelSizeSrc = 2;
                    break;
                case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
                    pixelSizeSrc = 3;
                    break;
                case System.Drawing.Imaging.PixelFormat.Format8bppIndexed:
                    pixelSizeSrc = 1;
                    break;
                default:
                    throw new FormatException("PixelFormat is not supported.");
            }
            
            BitmapData bmpData = default;
            try
            {
                bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                                ImageLockMode.ReadOnly,
                                                bitmap.PixelFormat);
                IntPtr ptr = bmpData.Scan0;

                int pixels = bitmap.Width * bitmap.Height;

                int width = bitmap.Width;
                int height = bitmap.Height;
                byte* src = (byte*)ptr.ToPointer();
                fixed (byte* dst = dstSpan)
                {
                    int p;
                    byte aux;
                    Color[] pallet = null;
                    if (bitmap.PixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
                        pallet = bitmap.Palette.Entries;

                    for (int i = 0; i < pixels; i++)
                    {
                        p = (i % width + (height - (i / width) - 1) * width) * pixelSizeSrc;
                        byte* dstPost = dst + i * pixelSizeDst;
                        switch (bitmap.PixelFormat)
                        {
                            case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                                WritePixel(pixelFormat, *(int*)(src + p), dstPost);
                                break;
                            case System.Drawing.Imaging.PixelFormat.Format16bppGrayScale:
                                aux = (byte)(((src[p] * 256) + src[p + 1]) / 257);                                
                                WritePixel(pixelFormat, aux, dstPost);
                                break;
                            case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
                            case System.Drawing.Imaging.PixelFormat.Format32bppRgb:
                                if(p == 0)
                                    WritePixel(pixelFormat, 255 << 24 | ((*(int*)(src + p) >> 8) & 0xFFFFFF), dstPost);
                                else
                                    WritePixel(pixelFormat, 255 << 24 | (*(int*)(src + p - 1) & 0xFFFFFF), dstPost);
                                break;
                            case System.Drawing.Imaging.PixelFormat.Format8bppIndexed:
                                aux = src[p];
                                WritePixel(pixelFormat, pallet[aux].ToArgb(), dstPost);
                                break;

                        }
                    }
                }
            }
            finally
            {
                if(bitmap != null)
                    bitmap.UnlockBits(bmpData);
            }
        }

        private static unsafe void WritePixel(GL.PixelFormat pixelFormat, in int color, byte* data)
        {
            if (BitConverter.IsLittleEndian)
            {
                switch (pixelFormat)
                {
                    case GL.PixelFormat.Rgba:
                        *(int*)data = (color & (0xFF << 24)) | ((color << 8) & 0xFF0000) | ((color >> 8) & 0xFF00) | (color & 0xFF);
                        break;
                    case GL.PixelFormat.Rgb:
                        *(int*)data |= ((color << 8) & 0xFF0000) | ((color >> 8) & 0xFF00) | (color & 0xFF);
                        break;
                    case GL.PixelFormat.Red:
                        *data = (byte)(color & 265);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(pixelFormat));
                }
            }
            else
                throw new NotImplementedException();
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
