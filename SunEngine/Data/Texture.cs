// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/. --%>
using SunEngine.GL;
using System;

namespace SunEngine.Data
{
    public struct Texture
    {
        public Texture(PixelFormat pixelFormat, PixelType pixelType, int width, int height, ReadOnlySpan<byte> data)
        {
            PixelFormat = pixelFormat;
            PixelType = pixelType;
            Width = width;
            Height = height;
            Data = data.ToArray();
        }

        /// <summary>
        /// The pixel format.
        /// </summary>
        public PixelFormat PixelFormat { get; set; }
        public PixelType PixelType { get; set; }
        /// <summary>
        /// Width of the texture, in pixels.
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Height of the texture, in pixels.
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Data of texture.
        /// </summary>
        public byte[] Data { get; set; }
    }
}
