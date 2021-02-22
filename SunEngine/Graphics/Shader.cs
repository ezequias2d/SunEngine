// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using SunEngine.GL;
using System;
using System.Collections.Generic;
using System.Text;

namespace SunEngine.Graphics
{
    public class Shader : GraphicObject
    {
        private int bindingPointCount;
        public Shader(IGL gl, params ShaderProgram[] shaders) : this(gl, (IEnumerable<ShaderProgram>)shaders)
        {

        }

        internal Shader(IGL gl, IEnumerable<ShaderProgram> shaders) : base(gl, gl.CreateProgram())
        {
            bindingPointCount = 0;

            List<int> shaderHandles = new List<int>();
            foreach (ShaderProgram program in shaders)
            {
                int shaderHandle = CompileShaderProgram(program);
                GL.AttachShader(Handle, shaderHandle);
                Console.WriteLine("Attach Shader: " + GL.GetError());
                shaderHandles.Add(shaderHandle);
            }

            GL.LinkProgram(Handle);
            GL.CheckErros();

            GL.ValidateProgram(Handle);
            GL.CheckErros();

            Console.WriteLine(GL.GetProgramInfoLog(Handle));
    
            foreach (int shaderHandle in shaderHandles)
            {
                GL.DetachShader(Handle, shaderHandle);
                GL.CheckErros();

                GL.DeleteShader(shaderHandle);
                GL.CheckErros();
            }
        }

        private int CompileShaderProgram(ShaderProgram program)
        {
            int shaderObject = GL.CreateShader(program.ShaderType);
            GL.CheckErros();
            if (program.IsASCII)
            {
                string text = Encoding.ASCII.GetString(program.Data);
                GL.ShaderSource(shaderObject, text);
                GL.CheckErros();

                GL.CompileShader(shaderObject);
                GL.CheckErros();
                return shaderObject;
            }
            else
            {
                GL.DeleteShader(shaderObject);
                GL.CheckErros();
                throw new NotImplementedException("Non-UTF8 shaders have not been implemented.");
            }
        }

        public void Use()
        {
            GL.UseProgram(Handle);
            GL.CheckErros();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                GL.DeleteProgram(Handle);
                GL.CheckErros();
            }
        }

        public void ResetBindingPointCount()
        {
            bindingPointCount = 0;
        }

        public void SetUniformBuffer<T>(Buffer<T> buffer, int index) where T : unmanaged
        {
            GL.UniformBlockBinding(Handle, index, bindingPointCount);
            GL.CheckErros();

            GL.BindBufferBase(BufferTarget.UniformBuffer, bindingPointCount, buffer.Handle);
            GL.CheckErros();
            bindingPointCount++;
        }

        public void SetUniformBuffer<T>(Buffer<T> buffer, string uniformBlockName) where T : unmanaged
        {
            int index = GetUniformBufferIndex(uniformBlockName);
            SetUniformBuffer(buffer, index);
        }

        public int GetUniformBufferIndex(string uniformBlockName)
        {
            return GL.GetUniformBlockIndex(Handle, uniformBlockName); ;
        }
    }
}