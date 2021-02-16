// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/. --%>
using SunEngine.GL;
using SunEngine.Graphics;
using System.Numerics;

namespace SunEngine.Core.Components
{
    public class ModelComponent : IDrawable, IComponent
    {
        private Matrix4x4 modelMatrix;
        
        public ModelComponent(Model model, Shader shader, int projectionViewBindingIndex, int modelBindingIndex)
        {
            Model = model;
            Shader = shader;

            ProjectionViewBinding = new Binding(this);
            ModelBinding = new Binding(this);

            ProjectionViewBinding.Index = projectionViewBindingIndex;
            ModelBinding.Index = modelBindingIndex;

            modelMatrix = Matrix4x4.Identity;
            ModelUniformBuffer = new Buffer<Matrix4x4>(model.GL, new Matrix4x4[1], BufferTarget.UniformBuffer, BufferUsage.DynamicDraw);
        }

        public ModelComponent(Model model, Shader shader, string projectionViewBindingName, string modelBindingName)
        {
            Model = model;
            Shader = shader;
            
            ProjectionViewBinding = new Binding(this);
            ModelBinding = new Binding(this);

            ProjectionViewBinding.Name = projectionViewBindingName;
            ModelBinding.Name = modelBindingName;

            modelMatrix = Matrix4x4.Identity;
            ModelUniformBuffer = new Buffer<Matrix4x4>(model.GL, new Matrix4x4[1], BufferTarget.UniformBuffer, BufferUsage.DynamicDraw);
        }

        public Model Model { get; set; }
        public Shader Shader { get; set; }
        public GpuTexture[] Textures { get; set; }
        public Buffer<Matrix4x4> ModelUniformBuffer { get; }

        public Binding ModelBinding { get; set; }
        public Binding ProjectionViewBinding { get; set; }


        public void Draw(GameObject gameObject, ElapsedTimeEventArgs e)
        {
            ModelUniformBuffer.SubData(modelMatrix, 0);

            var drawSubsystem = gameObject.World.DrawSubsystem;

            Shader.Use();
            Shader.ResetBindingPointCount();
            Shader.SetUniformBuffer(ModelUniformBuffer, ModelBinding.Index);
            Shader.SetUniformBuffer(drawSubsystem.MatricesBuffer, ProjectionViewBinding.Index);

            if(Textures != null)
            {
                int n = 0;
                foreach(var texture in Textures)
                    texture.Bind(n++);
            }

            Model.Draw();
        }

        public void Update(GameObject sender, ElapsedTimeEventArgs e)
        {
            modelMatrix = Matrix4x4.CreateScale(sender.Scale) * Matrix4x4.CreateFromQuaternion(sender.Rotation) * Matrix4x4.CreateTranslation(sender.Position);
        }

        public sealed class Binding
        {
            private readonly ModelComponent _component;
            private string _name;
            private int _index;
            public string Name
            {
                get => _name;
                set
                {
                    if (_name != value)
                    {
                        _name = value;
                        _component.Shader.Use();
                        _index = _component.Shader.GetUniformBufferIndex(value);
                    }
                }
            }
            public int Index
            {
                get => _index;
                set
                {
                    if (_index != value)
                    {
                        _name = null;
                        _index = value;
                    }
                }
            }

            internal Binding(ModelComponent component)
            {
                _component = component;
            }
        }
    }
}
