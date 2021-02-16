// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/. --%>
using Silk.NET.Maths;
using System;
using System.Drawing;
using Silk.NET.OpenGL;
using SunEngine.GL;
using SunEngine.Inputs;

namespace SunEngine.SilkNET
{
    internal static class Extensions
    {

        public static Vector2D<int> ToSilk(this Size value) => new Vector2D<int>(value.Width, value.Height);
        public static Vector2D<int> ToSilk(this Point value) => new Vector2D<int>(value.X, value.Y);

        public static Point ToPoint(this Vector2D<int> value) => new Point(value.X, value.Y);
        public static Size ToSize(this Vector2D<int> value) => new Size(value.X, value.Y);

        public static Silk.NET.Windowing.WindowState ToSilk(this Windowing.WindowState windowState)
        {
            switch (windowState)
            {
                case Windowing.WindowState.Fullscreen:
                    return Silk.NET.Windowing.WindowState.Fullscreen;
                case Windowing.WindowState.Maximized:
                    return Silk.NET.Windowing.WindowState.Maximized;
                case Windowing.WindowState.Minimized:
                    return Silk.NET.Windowing.WindowState.Minimized;
                case Windowing.WindowState.Normal:
                    return Silk.NET.Windowing.WindowState.Normal;
                default:
                    throw new InvalidCastException();
            }
        }

        public static Silk.NET.Windowing.WindowBorder ToSilk(this Windowing.WindowBorder windowBorder)
        {
            switch (windowBorder)
            {
                case Windowing.WindowBorder.Fixed:
                    return Silk.NET.Windowing.WindowBorder.Fixed;
                case Windowing.WindowBorder.Hidden:
                    return Silk.NET.Windowing.WindowBorder.Hidden;
                case Windowing.WindowBorder.Resizable:
                    return Silk.NET.Windowing.WindowBorder.Resizable;
                default:
                    throw new InvalidCastException();
            }
        }

        public static GLEnum ToSilk(this BufferTarget target)
        {
            switch (target)
            {
                case BufferTarget.ArrayBuffer:
                    return GLEnum.ArrayBuffer;
                case BufferTarget.AtomicCounterBuffer:
                    return GLEnum.AtomicCounterBuffer;
                case BufferTarget.CopyReadBuffer:
                    return GLEnum.CopyReadBuffer;
                case BufferTarget.CopyWriteBuffer:
                    return GLEnum.CopyWriteBuffer;
                case BufferTarget.DispatchIndirectBuffer:
                    return GLEnum.DispatchIndirectBuffer;
                case BufferTarget.DrawIndirectBuffer:
                    return GLEnum.DrawIndirectBuffer;
                case BufferTarget.ElementArrayBuffer:
                    return GLEnum.ElementArrayBuffer;
                case BufferTarget.PixelPackBuffer:
                    return GLEnum.PixelPackBuffer;
                case BufferTarget.PixelUnpackBuffer:
                    return GLEnum.PixelUnpackBuffer;
                case BufferTarget.QueryBuffer:
                    return GLEnum.QueryBuffer;
                case BufferTarget.ShaderStorageBuffer:
                    return GLEnum.ShaderStorageBuffer;
                case BufferTarget.TextureBuffer:
                    return GLEnum.TextureBuffer;
                case BufferTarget.TransformFeedbackBuffer:
                    return GLEnum.TransformFeedbackBuffer;
                case BufferTarget.UniformBuffer:
                    return GLEnum.UniformBuffer;
                default:
                    throw new InvalidCastException();
            }
        }

        public static GLEnum ToSilk(this GL.TextureTarget target)
        {
            switch (target)
            {
                case GL.TextureTarget.ProxyTexture1D:
                    return GLEnum.ProxyTexture1D;
                case GL.TextureTarget.ProxyTexture1DArray:
                    return GLEnum.ProxyTexture1DArray;
                case GL.TextureTarget.ProxyTexture2D:
                    return GLEnum.ProxyTexture2D;
                case GL.TextureTarget.ProxyTexture2DArray:
                    return GLEnum.ProxyTexture2DArray;
                case GL.TextureTarget.ProxyTexture2DMultisample:
                    return GLEnum.ProxyTexture2DMultisample;
                case GL.TextureTarget.ProxyTexture2DMultisampleArray:
                    return GLEnum.ProxyTexture2DMultisampleArray;
                case GL.TextureTarget.ProxyTexture3D:
                    return GLEnum.ProxyTexture3D;
                case GL.TextureTarget.ProxyTextureCubeMap:
                    return GLEnum.ProxyTextureCubeMap;
                case GL.TextureTarget.ProxyTextureCubeMapArray:
                    return GLEnum.ProxyTextureCubeMapArray;
                case GL.TextureTarget.ProxyTextureRectangle:
                    return GLEnum.ProxyTextureRectangle;
                case GL.TextureTarget.Texture1D:
                    return GLEnum.Texture1D;
                case GL.TextureTarget.Texture1DArray:
                    return GLEnum.Texture1DArray;
                case GL.TextureTarget.Texture2D:
                    return GLEnum.Texture2D;
                case GL.TextureTarget.Texture2DArray:
                    return GLEnum.Texture2DArray;
                case GL.TextureTarget.Texture2DMultisample:
                    return GLEnum.Texture2DMultisample;
                case GL.TextureTarget.Texture2DMultisampleArray:
                    return GLEnum.Texture2DMultisampleArray;
                case GL.TextureTarget.Texture3D:
                    return GLEnum.Texture3D;
                case GL.TextureTarget.TextureBuffer:
                    return GLEnum.TextureBuffer;
                case GL.TextureTarget.TextureCubeMap:
                    return GLEnum.TextureCubeMap;
                case GL.TextureTarget.TextureCubeMapArray:
                    return GLEnum.TextureCubeMapArray;
                case GL.TextureTarget.TextureCubeMapNegativeX:
                    return GLEnum.TextureCubeMapNegativeX;
                case GL.TextureTarget.TextureCubeMapNegativeY:
                    return GLEnum.TextureCubeMapNegativeY;
                case GL.TextureTarget.TextureCubeMapNegativeZ:
                    return GLEnum.TextureCubeMapNegativeZ;
                case GL.TextureTarget.TextureCubeMapPositiveX:
                    return GLEnum.TextureCubeMapPositiveX;
                case GL.TextureTarget.TextureCubeMapPositiveY:
                    return GLEnum.TextureCubeMapPositiveY;
                case GL.TextureTarget.TextureCubeMapPositiveZ:
                    return GLEnum.TextureCubeMapPositiveZ;
                case GL.TextureTarget.TextureRectangle:
                    return GLEnum.TextureRectangle;
                default:
                    throw new InvalidCastException();
            }
        }

        public static Silk.NET.OpenGL.BufferUsageARB ToSilk(this GL.BufferUsage usage)
        {
            switch (usage)
            {
                case BufferUsage.DynamicCopy:
                    return BufferUsageARB.DynamicCopy;
                case BufferUsage.DynamicDraw:
                    return BufferUsageARB.DynamicDraw;
                case BufferUsage.DynamicRead:
                    return BufferUsageARB.DynamicRead;
                case BufferUsage.StaticCopy:
                    return BufferUsageARB.StaticCopy;
                case BufferUsage.StaticDraw:
                    return BufferUsageARB.StaticDraw;
                case BufferUsage.StaticRead:
                    return BufferUsageARB.StaticRead;
                case BufferUsage.StreamCopy:
                    return BufferUsageARB.StreamCopy;
                case BufferUsage.StreamDraw:
                    return BufferUsageARB.StreamDraw;
                case BufferUsage.StreamRead:
                    return BufferUsageARB.StreamRead;
                default:
                    throw new InvalidCastException();
            }
        }

        public static Silk.NET.OpenGL.ShaderType ToSilk(this GL.ShaderType type)
        {
            switch (type)
            {
                case GL.ShaderType.ComputeShader:
                    return Silk.NET.OpenGL.ShaderType.ComputeShader;
                case GL.ShaderType.FragmentShader:
                    return Silk.NET.OpenGL.ShaderType.FragmentShader;
                case GL.ShaderType.GeometryShader:
                    return Silk.NET.OpenGL.ShaderType.GeometryShader;

                case GL.ShaderType.TessControlShader:
                    return Silk.NET.OpenGL.ShaderType.TessControlShader;
                case GL.ShaderType.TessEvaluationShader:
                    return Silk.NET.OpenGL.ShaderType.TessEvaluationShader;
                case GL.ShaderType.VertexShader:
                    return Silk.NET.OpenGL.ShaderType.VertexShader;

                default:
                    throw new InvalidCastException();
            }
        }

        public static Silk.NET.OpenGL.PrimitiveType ToSilk(this GL.PrimitiveType type)
        {
            switch (type)
            {
                case GL.PrimitiveType.LineLoop:
                    return Silk.NET.OpenGL.PrimitiveType.LineLoop;
                case GL.PrimitiveType.Lines:
                    return Silk.NET.OpenGL.PrimitiveType.Lines;
                case GL.PrimitiveType.LinesAdjacency:
                    return Silk.NET.OpenGL.PrimitiveType.LinesAdjacency;
                case GL.PrimitiveType.LineStrip:
                    return Silk.NET.OpenGL.PrimitiveType.LineStrip;
                case GL.PrimitiveType.LineStripAdjacency:
                    return Silk.NET.OpenGL.PrimitiveType.LineStripAdjacency;
                case GL.PrimitiveType.Patches:
                    return Silk.NET.OpenGL.PrimitiveType.Patches;
                case GL.PrimitiveType.Points:
                    return Silk.NET.OpenGL.PrimitiveType.Points;
                case GL.PrimitiveType.Quads:
                    return Silk.NET.OpenGL.PrimitiveType.QuadsExt;
                case GL.PrimitiveType.TriangleFan:
                    return Silk.NET.OpenGL.PrimitiveType.TriangleFan;
                case GL.PrimitiveType.Triangles:
                    return Silk.NET.OpenGL.PrimitiveType.Triangles;
                case GL.PrimitiveType.TrianglesAdjacency:
                    return Silk.NET.OpenGL.PrimitiveType.TrianglesAdjacency;
                case GL.PrimitiveType.TriangleStrip:
                    return Silk.NET.OpenGL.PrimitiveType.TriangleStrip;
                case GL.PrimitiveType.TriangleStripAdjacency:
                    return Silk.NET.OpenGL.PrimitiveType.TriangleStripAdjacency;
                default:
                    throw new InvalidCastException();
            }
        }

        public static Silk.NET.OpenGL.DrawElementsType ToSilk(this GL.DrawElementsType type)
        {
            switch (type)
            {
                case GL.DrawElementsType.UnsignedByte:
                    return Silk.NET.OpenGL.DrawElementsType.UnsignedByte;
                case GL.DrawElementsType.UnsignedInt:
                    return Silk.NET.OpenGL.DrawElementsType.UnsignedInt;
                case GL.DrawElementsType.UnsignedShort:
                    return Silk.NET.OpenGL.DrawElementsType.UnsignedShort;
                default:
                    throw new InvalidCastException();
            }
        }

        public static GLEnum ToSilk(this GL.PixelInternalFormat format)
        {
            switch (format)
            {
                case PixelInternalFormat.DepthComponet24:
                    return GLEnum.DepthComponent24;
                case PixelInternalFormat.DepthStencil248:
                    return GLEnum.Depth24Stencil8;
                case PixelInternalFormat.Red8:
                    return GLEnum.R8;
                case PixelInternalFormat.RG8:
                    return GLEnum.RG8;
                case PixelInternalFormat.RGB8:
                    return GLEnum.Rgb8;
                case PixelInternalFormat.RGBA8:
                    return GLEnum.Rgba;
                default:
                    throw new InvalidCastException();
            }
        }
        public static GLEnum ToSilk(this GL.PixelFormat format)
        {
            switch (format)
            {
                case GL.PixelFormat.Alpha:
                    return GLEnum.Alpha;
                case GL.PixelFormat.AlphaInteger:
                    return (GLEnum)36247;
                case GL.PixelFormat.Bgr:
                    return GLEnum.Bgr;
                case GL.PixelFormat.Bgra:
                    return GLEnum.Bgra;
                case GL.PixelFormat.BgraInteger:
                    return GLEnum.BgraInteger;
                case GL.PixelFormat.BgrInteger:
                    return GLEnum.BgrInteger;
                case GL.PixelFormat.Blue:
                    return GLEnum.Blue;
                case GL.PixelFormat.BlueInteger:
                    return GLEnum.BlueInteger;
                case GL.PixelFormat.DepthComponent:
                    return GLEnum.DepthComponent;
                case GL.PixelFormat.DepthStencil:
                    return GLEnum.DepthStencil;
                case GL.PixelFormat.Green:
                    return GLEnum.Green;
                case GL.PixelFormat.GreenInteger:
                    return GLEnum.GreenInteger;
                case GL.PixelFormat.Luminance:
                    return (GLEnum)6409;
                case GL.PixelFormat.LuminanceAlpha:
                    return (GLEnum)6410;
                case GL.PixelFormat.Red:
                    return GLEnum.Red;
                case GL.PixelFormat.RedInteger:
                    return GLEnum.RedInteger;
                case GL.PixelFormat.Rg:
                    return GLEnum.RG;
                case GL.PixelFormat.Rgb:
                    return GLEnum.Rgb;
                case GL.PixelFormat.Rgba:
                    return GLEnum.Rgba;
                case GL.PixelFormat.RgbaInteger:
                    return GLEnum.RgbaInteger;
                case GL.PixelFormat.RgbInteger:
                    return GLEnum.RgbInteger;
                case GL.PixelFormat.RgInteger:
                    return GLEnum.RGInteger;
                case GL.PixelFormat.UnsignedInt:
                    return GLEnum.UnsignedInt;
                case GL.PixelFormat.UnsignedShort:
                    return GLEnum.UnsignedShort;
                default:
                    throw new InvalidCastException();

            }
        }
        public static GLEnum ToSilk(this GL.PixelType type)
        {
            switch (type)
            {
                case GL.PixelType.Byte:
                    return GLEnum.Byte;
                case GL.PixelType.Float:
                    return GLEnum.Float;
                case GL.PixelType.Float32UnsignedInt248Rev:
                    return GLEnum.Float32UnsignedInt248Rev;
                case GL.PixelType.HalfFloat:
                    return GLEnum.HalfFloat;
                case GL.PixelType.Int:
                    return GLEnum.Int;
                case GL.PixelType.Short:
                    return GLEnum.Short;
                case GL.PixelType.UnsignedByte:
                    return GLEnum.UnsignedByte;
                case GL.PixelType.UnsignedByte233Reversed:
                    return GLEnum.UnsignedByte233Rev;
                case GL.PixelType.UnsignedByte332:
                    return GLEnum.UnsignedByte332;
                case GL.PixelType.UnsignedInt:
                    return GLEnum.UnsignedInt;
                case GL.PixelType.UnsignedInt1010102:
                    return GLEnum.UnsignedInt1010102;
                case GL.PixelType.UnsignedInt10F11F11FRev:
                    return GLEnum.UnsignedInt10f11f11fRev;
                case GL.PixelType.UnsignedInt2101010Reversed:
                    return GLEnum.UnsignedInt2101010Rev;
                case GL.PixelType.UnsignedInt248:
                    return GLEnum.UnsignedInt248;
                case GL.PixelType.UnsignedInt5999Rev:
                    return GLEnum.UnsignedInt5999Rev;
                case GL.PixelType.UnsignedInt8888:
                    return GLEnum.UnsignedInt8888;
                case GL.PixelType.UnsignedInt8888Reversed:
                    return GLEnum.UnsignedInt8888Rev;
                case GL.PixelType.UnsignedShort:
                    return GLEnum.UnsignedShort;
                case GL.PixelType.UnsignedShort1555Reversed:
                    return GLEnum.UnsignedShort1555Rev;
                case GL.PixelType.UnsignedShort4444:
                    return GLEnum.UnsignedShort4444;
                case GL.PixelType.UnsignedShort4444Reversed:
                    return GLEnum.UnsignedShort4444Rev;
                case GL.PixelType.UnsignedShort5551:
                    return GLEnum.UnsignedShort5551;
                case GL.PixelType.UnsignedShort565:
                    return GLEnum.UnsignedShort565;
                case GL.PixelType.UnsignedShort565Reversed:
                    return GLEnum.UnsignedShort565Rev;
                default:
                    throw new InvalidCastException();
            }
        }
        public static GLEnum ToSilk(this GL.TextureParameterName pname)
        {
            switch (pname)
            {
                case GL.TextureParameterName.ClampToBorder:
                    return GLEnum.ClampToBorder;
                case GL.TextureParameterName.ClampToEdge:
                    return GLEnum.ClampToEdge;
                case GL.TextureParameterName.DepthStencilTextureMode:
                    return GLEnum.DepthStencilTextureMode;
                case GL.TextureParameterName.DepthTextureMode:
                    return (GLEnum)34891;
                case GL.TextureParameterName.GenerateMipmap:
                    return (GLEnum)33169;
                case GL.TextureParameterName.TextureAlphaSize:
                    return GLEnum.TextureAlphaSize;
                case GL.TextureParameterName.TextureBaseLevel:
                    return GLEnum.TextureBaseLevel;
                case GL.TextureParameterName.TextureBlueSize:
                    return GLEnum.TextureBlueSize;
                case GL.TextureParameterName.TextureBorder:
                    return (GLEnum)4101;
                case GL.TextureParameterName.TextureBorderColor:
                    return GLEnum.TextureBorderColor;
                case GL.TextureParameterName.TextureCompareFailValue:
                    return (GLEnum)32959;
                case GL.TextureParameterName.TextureCompareFunc:
                    return GLEnum.TextureCompareFunc;
                case GL.TextureParameterName.TextureCompareMode:
                    return GLEnum.TextureCompareMode;
                case GL.TextureParameterName.TextureComponents:
                    return (GLEnum)4099;
                case GL.TextureParameterName.TextureDepth:
                    return GLEnum.TextureDepth;
                case GL.TextureParameterName.TextureGreenSize:
                    return GLEnum.TextureGreenSize;
                case GL.TextureParameterName.TextureHeight:
                    return GLEnum.TextureHeight;
                case GL.TextureParameterName.TextureIntensitySize:
                    return (GLEnum)32865;
                case GL.TextureParameterName.TextureInternalFormat:
                    return GLEnum.TextureInternalFormat;
                case GL.TextureParameterName.TextureLodBias:
                    return GLEnum.TextureLodBias;
                case GL.TextureParameterName.TextureLuminanceSize:
                    return (GLEnum)32864;
                case GL.TextureParameterName.TextureMagFilter:
                    return GLEnum.TextureMagFilter;
                case GL.TextureParameterName.TextureMaxLevel:
                    return GLEnum.TextureMaxLevel;
                case GL.TextureParameterName.TextureMaxLod:
                    return GLEnum.TextureMaxLod;
                case GL.TextureParameterName.TextureMinFilter:
                    return GLEnum.TextureMinFilter;
                case GL.TextureParameterName.TextureMinLod:
                    return GLEnum.TextureMinLod;
                case GL.TextureParameterName.TexturePriority:
                    return (GLEnum)32870;
                case GL.TextureParameterName.TextureRedSize:
                    return GLEnum.TextureRedSize;
                case GL.TextureParameterName.TextureResident:
                    return (GLEnum)32871;
                case GL.TextureParameterName.TextureSwizzleA:
                    return GLEnum.TextureSwizzleA;
                case GL.TextureParameterName.TextureSwizzleB:
                    return GLEnum.TextureSwizzleB;
                case GL.TextureParameterName.TextureSwizzleG:
                    return GLEnum.TextureSwizzleG;
                case GL.TextureParameterName.TextureSwizzleR:
                    return GLEnum.TextureSwizzleR;
                case GL.TextureParameterName.TextureSwizzleRgba:
                    return GLEnum.TextureSwizzleRgba;
                case GL.TextureParameterName.TextureWidth:
                    return GLEnum.TextureWidth;
                case GL.TextureParameterName.TextureWrapR:
                    return GLEnum.TextureWrapR;
                case GL.TextureParameterName.TextureWrapS:
                    return GLEnum.TextureWrapS;
                case GL.TextureParameterName.TextureWrapT:
                    return GLEnum.TextureWrapT;
                default:
                    throw new InvalidCastException();
            }
        }
        public static Silk.NET.OpenGL.TextureMinFilter ToSilk(this GL.TextureMinFilter minFilter)
        {
            switch (minFilter)
            {
                case GL.TextureMinFilter.Linear:
                    return Silk.NET.OpenGL.TextureMinFilter.Linear;
                case GL.TextureMinFilter.LinearMipmapLinear:
                    return Silk.NET.OpenGL.TextureMinFilter.LinearMipmapLinear;
                case GL.TextureMinFilter.LinearMipmapNearest:
                    return Silk.NET.OpenGL.TextureMinFilter.LinearMipmapNearest;
                case GL.TextureMinFilter.Nearest:
                    return Silk.NET.OpenGL.TextureMinFilter.Nearest;
                case GL.TextureMinFilter.NearestMipmapLinear:
                    return Silk.NET.OpenGL.TextureMinFilter.NearestMipmapLinear;
                case GL.TextureMinFilter.NearestMipmapNearest:
                    return Silk.NET.OpenGL.TextureMinFilter.NearestMipmapNearest;
                default:
                    throw new InvalidCastException();
            }
        }

        public static Silk.NET.OpenGL.TextureMagFilter ToSilk(this GL.TextureMagFilter minFilter)
        {
            switch (minFilter)
            {
                case GL.TextureMagFilter.Linear:
                    return Silk.NET.OpenGL.TextureMagFilter.Linear;
                case GL.TextureMagFilter.Nearest:
                    return Silk.NET.OpenGL.TextureMagFilter.Nearest;
                default:
                    throw new InvalidCastException();
            }
        }
        public static Silk.NET.OpenGL.VertexAttribPointerType ToSilk(this GL.VertexAttribPointerType type)
        {
            switch (type)
            {
                case GL.VertexAttribPointerType.Byte:
                    return Silk.NET.OpenGL.VertexAttribPointerType.Byte;
                case GL.VertexAttribPointerType.Double:
                    return Silk.NET.OpenGL.VertexAttribPointerType.Double;
                case GL.VertexAttribPointerType.Fixed:
                    return Silk.NET.OpenGL.VertexAttribPointerType.Fixed;
                case GL.VertexAttribPointerType.Float:
                    return Silk.NET.OpenGL.VertexAttribPointerType.Float;
                case GL.VertexAttribPointerType.HalfFloat:
                    return Silk.NET.OpenGL.VertexAttribPointerType.HalfFloat;
                case GL.VertexAttribPointerType.Int:
                    return Silk.NET.OpenGL.VertexAttribPointerType.Int;
                case GL.VertexAttribPointerType.Short:
                    return Silk.NET.OpenGL.VertexAttribPointerType.Short;
                case GL.VertexAttribPointerType.UnsignedByte:
                    return Silk.NET.OpenGL.VertexAttribPointerType.UnsignedByte;
                case GL.VertexAttribPointerType.UnsignedInt:
                    return Silk.NET.OpenGL.VertexAttribPointerType.UnsignedInt;
                case GL.VertexAttribPointerType.UnsignedShort:
                    return Silk.NET.OpenGL.VertexAttribPointerType.UnsignedShort;
                default:
                    throw new InvalidCastException();
            }
        }

        public static ClearBufferMask ToSilk(this ClearMask mask)
        {
            ClearBufferMask silkMask = 0;

            if (mask.HasFlag(ClearMask.AccumBufferBit))
                silkMask |= (ClearBufferMask)512;

            if (mask.HasFlag(ClearMask.ColorBufferBit))
                silkMask |= ClearBufferMask.ColorBufferBit;

            if (mask.HasFlag(ClearMask.DepthBufferBit))
                silkMask |= ClearBufferMask.DepthBufferBit;

            if (mask.HasFlag(ClearMask.StencilBufferBit))
                silkMask |= ClearBufferMask.StencilBufferBit;

            return silkMask;
        }

        public static Silk.NET.OpenGL.EnableCap ToSilk(this GL.EnableCap cap)
        {
            switch (cap)
            {
                case GL.EnableCap.Blend:
                    return Silk.NET.OpenGL.EnableCap.Blend;
                case GL.EnableCap.ClipDistance0:
                    return Silk.NET.OpenGL.EnableCap.ClipDistance0;
                case GL.EnableCap.ClipDistance1:
                    return Silk.NET.OpenGL.EnableCap.ClipDistance1;
                case GL.EnableCap.ClipDistance2:
                    return Silk.NET.OpenGL.EnableCap.ClipDistance2;
                case GL.EnableCap.ClipDistance3:
                    return Silk.NET.OpenGL.EnableCap.ClipDistance3;
                case GL.EnableCap.ClipDistance4:
                    return Silk.NET.OpenGL.EnableCap.ClipDistance4;
                case GL.EnableCap.ClipDistance5:
                    return Silk.NET.OpenGL.EnableCap.ClipDistance5;
                case GL.EnableCap.ClipDistance6:
                    return Silk.NET.OpenGL.EnableCap.ClipDistance6;
                case GL.EnableCap.ClipDistance7:
                    return Silk.NET.OpenGL.EnableCap.ClipDistance7;
                case GL.EnableCap.ColorLogicOp:
                    return Silk.NET.OpenGL.EnableCap.ColorLogicOp;
                case GL.EnableCap.ColorTable:
                    return Silk.NET.OpenGL.EnableCap.ColorTable;
                case GL.EnableCap.CullFace:
                    return Silk.NET.OpenGL.EnableCap.CullFace;
                case GL.EnableCap.DebugOutput:
                    return Silk.NET.OpenGL.EnableCap.DebugOutput;
                case GL.EnableCap.DebugOutputSynchronous:
                    return Silk.NET.OpenGL.EnableCap.DebugOutputSynchronous;
                case GL.EnableCap.DepthClamp:
                    return Silk.NET.OpenGL.EnableCap.DepthClamp;
                case GL.EnableCap.DepthTest:
                    return Silk.NET.OpenGL.EnableCap.DepthTest;
                case GL.EnableCap.Dither:
                    return Silk.NET.OpenGL.EnableCap.Dither;
                case GL.EnableCap.FramebufferSrgb:
                    return Silk.NET.OpenGL.EnableCap.FramebufferSrgb;
                case GL.EnableCap.LineSmooth:
                    return Silk.NET.OpenGL.EnableCap.LineSmooth;
                case GL.EnableCap.Multisample:
                    return Silk.NET.OpenGL.EnableCap.Multisample;
                case GL.EnableCap.PolygonOffsetFill:
                    return Silk.NET.OpenGL.EnableCap.PolygonOffsetFill;
                case GL.EnableCap.PolygonOffsetLine:
                    return Silk.NET.OpenGL.EnableCap.PolygonOffsetLine;
                case GL.EnableCap.PolygonOffsetPoint:
                    return Silk.NET.OpenGL.EnableCap.PolygonOffsetPoint;
                case GL.EnableCap.PolygonSmooth:
                    return Silk.NET.OpenGL.EnableCap.PolygonSmooth;
                case GL.EnableCap.PostColorMatrixColorTable:
                    return Silk.NET.OpenGL.EnableCap.PostColorMatrixColorTable;
                case GL.EnableCap.PostConvolutionColorTable:
                    return Silk.NET.OpenGL.EnableCap.PostConvolutionColorTable;
                case GL.EnableCap.PrimitiveRestart:
                    return Silk.NET.OpenGL.EnableCap.PrimitiveRestart;
                case GL.EnableCap.PrimitiveRestartFixedIndex:
                    return Silk.NET.OpenGL.EnableCap.PrimitiveRestartFixedIndex;
                case GL.EnableCap.ProgramPointSize:
                    return Silk.NET.OpenGL.EnableCap.ProgramPointSize;
                case GL.EnableCap.RasterizerDiscard:
                    return Silk.NET.OpenGL.EnableCap.RasterizerDiscard;
                case GL.EnableCap.SampleAlphaToCoverage:
                    return Silk.NET.OpenGL.EnableCap.SampleAlphaToCoverage;
                case GL.EnableCap.SampleAlphaToOne:
                    return Silk.NET.OpenGL.EnableCap.SampleAlphaToOne;
                case GL.EnableCap.SampleCoverage:
                    return Silk.NET.OpenGL.EnableCap.SampleCoverage;
                case GL.EnableCap.SampleMask:
                    return Silk.NET.OpenGL.EnableCap.SampleMask;
                case GL.EnableCap.SampleShading:
                    return Silk.NET.OpenGL.EnableCap.SampleShading;
                case GL.EnableCap.ScissorTest:
                    return Silk.NET.OpenGL.EnableCap.ScissorTest;
                case GL.EnableCap.StencilTest:
                    return Silk.NET.OpenGL.EnableCap.StencilTest;
                case GL.EnableCap.Texture1D:
                    return Silk.NET.OpenGL.EnableCap.Texture1D;
                case GL.EnableCap.Texture2D:
                    return Silk.NET.OpenGL.EnableCap.Texture2D;
                case GL.EnableCap.TextureCubeMapSeamless:
                    return Silk.NET.OpenGL.EnableCap.TextureCubeMapSeamless;
                default:
                    throw new InvalidCastException();
            }
        }

        public static Silk.NET.Input.Key ToSilk(this Key key)
        {
            switch (key)
            {
                case Key.A:
                    return Silk.NET.Input.Key.A;
                case Key.Apostrophe:
                    return Silk.NET.Input.Key.Apostrophe;
                case Key.B:
                    return Silk.NET.Input.Key.B;
                case Key.Backslash:
                    return Silk.NET.Input.Key.BackSlash;
                case Key.Backspace:
                    return Silk.NET.Input.Key.Backspace;
                case Key.C:
                    return Silk.NET.Input.Key.C;
                case Key.CapsLock:
                    return Silk.NET.Input.Key.CapsLock;
                case Key.Comma:
                    return Silk.NET.Input.Key.Comma;
                case Key.D:
                    return Silk.NET.Input.Key.D;
                case Key.D0:
                    return Silk.NET.Input.Key.Number0;
                case Key.D1:
                    return Silk.NET.Input.Key.Number1;
                case Key.D2:
                    return Silk.NET.Input.Key.Number2;
                case Key.D3:
                    return Silk.NET.Input.Key.Number3;
                case Key.D4:
                    return Silk.NET.Input.Key.Number4;
                case Key.D5:
                    return Silk.NET.Input.Key.Number5;
                case Key.D6:
                    return Silk.NET.Input.Key.Number6;
                case Key.D7:
                    return Silk.NET.Input.Key.Number7;
                case Key.D8:
                    return Silk.NET.Input.Key.Number8;
                case Key.D9:
                    return Silk.NET.Input.Key.Number9;
                case Key.Delete:
                    return Silk.NET.Input.Key.Delete;
                case Key.Down:
                    return Silk.NET.Input.Key.Down;
                case Key.E:
                    return Silk.NET.Input.Key.E;
                case Key.End:
                    return Silk.NET.Input.Key.End;
                case Key.Enter:
                    return Silk.NET.Input.Key.Enter;
                case Key.Equal:
                    return Silk.NET.Input.Key.Equal;
                case Key.Escape:
                    return Silk.NET.Input.Key.Escape;
                case Key.F:
                    return Silk.NET.Input.Key.F;
                case Key.F1:
                    return Silk.NET.Input.Key.F1;
                case Key.F10:
                    return Silk.NET.Input.Key.F10;
                case Key.F11:
                    return Silk.NET.Input.Key.F11;
                case Key.F12:
                    return Silk.NET.Input.Key.F12;
                case Key.F13:
                    return Silk.NET.Input.Key.F13;
                case Key.F14:
                    return Silk.NET.Input.Key.F14;
                case Key.F15:
                    return Silk.NET.Input.Key.F15;
                case Key.F16:
                    return Silk.NET.Input.Key.F16;
                case Key.F17:
                    return Silk.NET.Input.Key.F17;
                case Key.F18:
                    return Silk.NET.Input.Key.F18;
                case Key.F19:
                    return Silk.NET.Input.Key.F19;
                case Key.F2:
                    return Silk.NET.Input.Key.F2;
                case Key.F20:
                    return Silk.NET.Input.Key.F20;
                case Key.F21:
                    return Silk.NET.Input.Key.F21;
                case Key.F22:
                    return Silk.NET.Input.Key.F22;
                case Key.F23:
                    return Silk.NET.Input.Key.F23;
                case Key.F24:
                    return Silk.NET.Input.Key.F24;
                case Key.F25:
                    return Silk.NET.Input.Key.F25;
                case Key.F3:
                    return Silk.NET.Input.Key.F3;
                case Key.F4:
                    return Silk.NET.Input.Key.F4;
                case Key.F5:
                    return Silk.NET.Input.Key.F5;
                case Key.F6:
                    return Silk.NET.Input.Key.F6;
                case Key.F7:
                    return Silk.NET.Input.Key.F7;
                case Key.F8:
                    return Silk.NET.Input.Key.F8;
                case Key.F9:
                    return Silk.NET.Input.Key.F9;
                case Key.G:
                    return Silk.NET.Input.Key.G;
                case Key.GraveAccent:
                    return Silk.NET.Input.Key.GraveAccent;
                case Key.H:
                    return Silk.NET.Input.Key.H;
                case Key.Home:
                    return Silk.NET.Input.Key.Home;
                case Key.I:
                    return Silk.NET.Input.Key.I;
                case Key.Insert:
                    return Silk.NET.Input.Key.Insert;
                case Key.J:
                    return Silk.NET.Input.Key.J;
                case Key.K:
                    return Silk.NET.Input.Key.K;
                case Key.KeyPad0:
                    return Silk.NET.Input.Key.Keypad0;
                case Key.KeyPad1:
                    return Silk.NET.Input.Key.Keypad1;
                case Key.KeyPad2:
                    return Silk.NET.Input.Key.Keypad2;
                case Key.KeyPad3:
                    return Silk.NET.Input.Key.Keypad3;
                case Key.KeyPad4:
                    return Silk.NET.Input.Key.Keypad4;
                case Key.KeyPad5:
                    return Silk.NET.Input.Key.Keypad5;
                case Key.KeyPad6:
                    return Silk.NET.Input.Key.Keypad6;
                case Key.KeyPad7:
                    return Silk.NET.Input.Key.Keypad7;
                case Key.KeyPad8:
                    return Silk.NET.Input.Key.Keypad8;
                case Key.KeyPad9:
                    return Silk.NET.Input.Key.Keypad9;
                case Key.KeyPadAdd:
                    return Silk.NET.Input.Key.KeypadAdd;
                case Key.KeyPadDecimal:
                    return Silk.NET.Input.Key.KeypadDecimal;
                case Key.KeyPadDivide:
                    return Silk.NET.Input.Key.KeypadDivide;
                case Key.KeyPadEnter:
                    return Silk.NET.Input.Key.KeypadEnter;
                case Key.KeyPadEqual:
                    return Silk.NET.Input.Key.KeypadEqual;
                case Key.KeyPadMultiply:
                    return Silk.NET.Input.Key.KeypadMultiply;
                case Key.KeyPadSubtract:
                    return Silk.NET.Input.Key.KeypadSubtract;
                case Key.L:
                    return Silk.NET.Input.Key.L;
                case Key.Left:
                    return Silk.NET.Input.Key.Left;
                case Key.LeftAlt:
                    return Silk.NET.Input.Key.AltLeft;
                case Key.LeftBracket:
                    return Silk.NET.Input.Key.LeftBracket;
                case Key.LeftControl:
                    return Silk.NET.Input.Key.ControlLeft;
                case Key.LeftShift:
                    return Silk.NET.Input.Key.ShiftLeft;
                case Key.LeftSuper:
                    return Silk.NET.Input.Key.SuperLeft;
                case Key.M:
                    return Silk.NET.Input.Key.M;
                case Key.Menu:
                    return Silk.NET.Input.Key.Menu;
                case Key.Minus:
                    return Silk.NET.Input.Key.Minus;
                case Key.N:
                    return Silk.NET.Input.Key.N;
                case Key.NumLock:
                    return Silk.NET.Input.Key.NumLock;
                case Key.O:
                    return Silk.NET.Input.Key.O;
                case Key.P:
                    return Silk.NET.Input.Key.P;
                case Key.PageDown:
                    return Silk.NET.Input.Key.PageDown;
                case Key.PageUp:
                    return Silk.NET.Input.Key.PageUp;
                case Key.Pause:
                    return Silk.NET.Input.Key.Pause;
                case Key.Period:
                    return Silk.NET.Input.Key.Period;
                case Key.PrintScreen:
                    return Silk.NET.Input.Key.PrintScreen;
                case Key.Q:
                    return Silk.NET.Input.Key.Q;
                case Key.R:
                    return Silk.NET.Input.Key.R;
                case Key.Right:
                    return Silk.NET.Input.Key.Right;
                case Key.RightBracket:
                    return Silk.NET.Input.Key.RightBracket;
                case Key.RightAlt:
                    return Silk.NET.Input.Key.AltRight;
                case Key.RightControl:
                    return Silk.NET.Input.Key.ControlRight;
                case Key.RightShift:
                    return Silk.NET.Input.Key.ShiftRight;
                case Key.RightSuper:
                    return Silk.NET.Input.Key.SuperRight;
                case Key.S:
                    return Silk.NET.Input.Key.S;
                case Key.ScrollLock:
                    return Silk.NET.Input.Key.ScrollLock;
                case Key.Semicolon:
                    return Silk.NET.Input.Key.Semicolon;
                case Key.Slash:
                    return Silk.NET.Input.Key.Slash;
                case Key.Space:
                    return Silk.NET.Input.Key.Space;
                case Key.T:
                    return Silk.NET.Input.Key.T;
                case Key.Tab:
                    return Silk.NET.Input.Key.Tab;
                case Key.U:
                    return Silk.NET.Input.Key.U;
                case Key.Unknown:
                    return Silk.NET.Input.Key.Unknown;
                case Key.Up:
                    return Silk.NET.Input.Key.Up;
                case Key.V:
                    return Silk.NET.Input.Key.V;
                case Key.W:
                    return Silk.NET.Input.Key.W;
                case Key.X:
                    return Silk.NET.Input.Key.X;
                case Key.Y:
                    return Silk.NET.Input.Key.Y;
                case Key.Z:
                    return Silk.NET.Input.Key.Z;
                default:
                    throw new InvalidCastException();
                
            }
        }

        public static Silk.NET.OpenGL.FramebufferTarget ToSilk(this GL.FramebufferTarget target)
        {
            switch (target)
            {
                case GL.FramebufferTarget.DrawFramebuffer:
                    return Silk.NET.OpenGL.FramebufferTarget.DrawFramebuffer;
                case GL.FramebufferTarget.Framebuffer:
                    return Silk.NET.OpenGL.FramebufferTarget.Framebuffer;
                case GL.FramebufferTarget.ReadFramebuffer:
                    return Silk.NET.OpenGL.FramebufferTarget.ReadFramebuffer;
                default:
                    throw new InvalidCastException();
            }
        }

        public static Silk.NET.OpenGL.RenderbufferTarget ToSilk(this GL.RenderbufferTarget target)
        {
            switch (target)
            {
                case GL.RenderbufferTarget.Renderbuffer:
                    return Silk.NET.OpenGL.RenderbufferTarget.Renderbuffer;
                default:
                    throw new InvalidCastException();
            }
        }

        public static Silk.NET.OpenGL.FramebufferAttachment ToSilk(this GL.FramebufferAttachment attachment)
        {
            switch (attachment)
            {
                case GL.FramebufferAttachment.ColorAttachment0:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment0;
                case GL.FramebufferAttachment.ColorAttachment1:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment1;
                case GL.FramebufferAttachment.ColorAttachment10:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment10;
                case GL.FramebufferAttachment.ColorAttachment11:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment11;
                case GL.FramebufferAttachment.ColorAttachment12:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment12;
                case GL.FramebufferAttachment.ColorAttachment13:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment13;
                case GL.FramebufferAttachment.ColorAttachment14:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment14;
                case GL.FramebufferAttachment.ColorAttachment15:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment15;
                case GL.FramebufferAttachment.ColorAttachment16:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment16;
                case GL.FramebufferAttachment.ColorAttachment17:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment17;
                case GL.FramebufferAttachment.ColorAttachment18:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment18;
                case GL.FramebufferAttachment.ColorAttachment19:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment19;
                case GL.FramebufferAttachment.ColorAttachment2:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment2;
                case GL.FramebufferAttachment.ColorAttachment20:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment20;
                case GL.FramebufferAttachment.ColorAttachment21:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment21;
                case GL.FramebufferAttachment.ColorAttachment22:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment22;
                case GL.FramebufferAttachment.ColorAttachment23:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment23;
                case GL.FramebufferAttachment.ColorAttachment24:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment24;
                case GL.FramebufferAttachment.ColorAttachment25:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment25;
                case GL.FramebufferAttachment.ColorAttachment26:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment26;
                case GL.FramebufferAttachment.ColorAttachment27:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment27;
                case GL.FramebufferAttachment.ColorAttachment28:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment28;
                case GL.FramebufferAttachment.ColorAttachment29:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment29;
                case GL.FramebufferAttachment.ColorAttachment3:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment3;
                case GL.FramebufferAttachment.ColorAttachment30:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment30;
                case GL.FramebufferAttachment.ColorAttachment31:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment31;
                case GL.FramebufferAttachment.ColorAttachment4:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment4;
                case GL.FramebufferAttachment.ColorAttachment5:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment5;
                case GL.FramebufferAttachment.ColorAttachment6:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment6;
                case GL.FramebufferAttachment.ColorAttachment7:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment7;
                case GL.FramebufferAttachment.ColorAttachment8:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment8;
                case GL.FramebufferAttachment.ColorAttachment9:
                    return Silk.NET.OpenGL.FramebufferAttachment.ColorAttachment9;
                case GL.FramebufferAttachment.DepthAttachment:
                    return Silk.NET.OpenGL.FramebufferAttachment.DepthAttachment;
                case GL.FramebufferAttachment.StencilAttachment:
                    return Silk.NET.OpenGL.FramebufferAttachment.StencilAttachment;
                case GL.FramebufferAttachment.DepthStencilAttachment:
                    return (Silk.NET.OpenGL.FramebufferAttachment)GLEnum.DepthStencilAttachment;
                default:
                    throw new InvalidCastException();

            }
        }

        public static Silk.NET.OpenGL.BlitFramebufferFilter ToSilk(this GL.BlitFramebufferFilter filter)
        {
            switch (filter)
            {
                case GL.BlitFramebufferFilter.Linear:
                    return Silk.NET.OpenGL.BlitFramebufferFilter.Linear;
                case GL.BlitFramebufferFilter.Nearest:
                    return Silk.NET.OpenGL.BlitFramebufferFilter.Nearest;
                default:
                    throw new InvalidCastException();
            }
        }
        public static GL.GLError ToSunGLError(this Silk.NET.OpenGL.GLEnum @enum)
        {
            switch (@enum)
            {
                case GLEnum.NoError:
                    return GLError.NoError;
                case GLEnum.InvalidEnum:
                    return GLError.InvalidEnum;
                case GLEnum.InvalidValue:
                    return GLError.InvalidValue;
                case GLEnum.InvalidOperation:
                    return GLError.InvalidOperation;
                case GLEnum.InvalidFramebufferOperation:
                    return GLError.InvalidFramebufferOperation;
                case GLEnum.OutOfMemory:
                    return GLError.OutOfMemory;
                default:
                    throw new InvalidCastException();
            }
        }
        public static GL.FramebufferStatus ToSunFramebufferStatus(this GLEnum @enum)
        {
            switch (@enum)
            {
                case 0:
                    return GL.FramebufferStatus.GenericError;
                case GLEnum.FramebufferComplete:
                    return GL.FramebufferStatus.Complete;
                case GLEnum.FramebufferUndefined:
                    return GL.FramebufferStatus.Undefined;
                case GLEnum.FramebufferIncompleteAttachment:
                    return GL.FramebufferStatus.IncompleteAttachment;
                case GLEnum.FramebufferIncompleteMissingAttachment:
                    return GL.FramebufferStatus.IncompleteMissingAttachment;
                case GLEnum.FramebufferIncompleteDrawBuffer:
                    return GL.FramebufferStatus.IncompleteDrawBuffer;
                case GLEnum.FramebufferIncompleteReadBuffer:
                    return GL.FramebufferStatus.IncompleteReadBuffer;
                case GLEnum.FramebufferUnsupported:
                    return GL.FramebufferStatus.Unsupported;
                case GLEnum.FramebufferIncompleteMultisample:
                    return GL.FramebufferStatus.IncompleteMultisample;
                case GLEnum.FramebufferIncompleteLayerTargets:
                    return GL.FramebufferStatus.IncompleteLayerTargets;
                default:
                    throw new InvalidCastException();
            }
        }

    }
}
