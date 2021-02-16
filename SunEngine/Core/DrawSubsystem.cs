// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/. --%>
using SunEngine.Core.Components;
using SunEngine.Data;
using SunEngine.GL;
using SunEngine.Graphics;
using System;
using System.Drawing;
using System.Numerics;

namespace SunEngine.Core
{
    public sealed class DrawSubsystem : BaseSubsystem<IDrawable>, IDrawable, IDisposable
    {
        private readonly int sampleTexture;
        internal DrawSubsystem(World world, IGL gl, Size size, int samples) : base(world)
        {
            GL = gl;

            Span<WorldViewMatrices> data = stackalloc WorldViewMatrices[1];
            MatricesBuffer = new Buffer<WorldViewMatrices>(
                GL,
                data,
                BufferTarget.UniformBuffer,
                BufferUsage.DynamicDraw);

            Framebuffer = new Framebuffer(GL);
            Framebuffer.Bind(FramebufferTarget.Framebuffer);

            FramebufferColor0 = new GpuTexture(GL, new Texture(PixelFormat.Rgb, PixelType.UnsignedByte, size.Width, size.Height, ReadOnlySpan<byte>.Empty), PixelInternalFormat.RGB8);
            Framebuffer.Attach(FramebufferColor0, FramebufferAttachment.ColorAttachment0);

            DepthBuffer = new Renderbuffer(GL, PixelInternalFormat.DepthStencil248, size);
            Framebuffer.Attach(DepthBuffer, FramebufferAttachment.DepthStencilAttachment);

            Framebuffer.Unbind(FramebufferTarget.Framebuffer);


            //gl.Enable(EnableCap.Multisample);
            //sampleTexture = GL.GenTexture();
            //GL.TexImage2DMultisample(TextureTarget.Texture2DMultisample, samples, PixelInternalFormat.RGB8, world._screenSize.Width, world._screenSize.Height, true);


        }
        public IGL GL { get; }
        public CameraComponent MainCamera { get; set; }
        public WorldViewMatrices Matrices;
        public Buffer<WorldViewMatrices> MatricesBuffer { get; }
        public Framebuffer Framebuffer { get; }
        public GpuTexture FramebufferColor0 { get; }
        public Renderbuffer DepthBuffer { get; }



        public void Draw(GameObject _, ElapsedTimeEventArgs e)
        {
            Matrices.View = MainCamera.Camera.CreateView();
            
            if(MainCamera.IsPerspective)
                Matrices.Projection = MainCamera.Camera.CreatePerspective();
            else
                Matrices.Projection = MainCamera.Camera.CreateOthograph();

            {
                Span<WorldViewMatrices> data = stackalloc WorldViewMatrices[1];
                data[0] = Matrices;
                MatricesBuffer.SubData<WorldViewMatrices>(data, 0);
            }

            foreach (var pair in this)
                pair.Key.Draw(World.Get(pair.Value), e);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public struct WorldViewMatrices
        {
            public Matrix4x4 Projection;
            public Matrix4x4 View;
        }
    }
}
