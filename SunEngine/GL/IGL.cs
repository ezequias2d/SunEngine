// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Drawing;
using System.Numerics;

namespace SunEngine.GL
{
    public interface IGL
    {
        #region buffers
        /// <summary>
        /// Create a single buffer object.
        /// </summary>
        /// <returns>Handle of buffer object.</returns>
        int GenBuffer();

        /// <summary>
        /// Delete a single buffer object.
        /// </summary>
        /// <param name="buffer">Handle of buffer object.</param>
        void DeleteBuffer(int buffer);

        /// <summary>
        /// Create buffer objects to fill the <paramref name="buffers"/>
        /// </summary>
        /// <param name="buffers">Specifies an array in which the generated buffer object names are stored.</param>
        void GenBuffers(Span<int> buffers);

        /// <summary>
        /// Deletes buffer objects in <paramref name="buffers"/>.
        /// </summary>
        /// <param name="buffers">An array with buffer objects to delete.</param>
        void DeleteBuffers(Span<int> buffers);

        /// <summary>
        /// Binds a buffer object to the specified buffer binding point.
        /// </summary>
        /// <param name="target">Specifies the target to which the buffer object is bound.</param>
        /// <param name="buffer">Specifies the name of a buffer object.</param>
        void BindBuffer(BufferTarget target, int buffer);

        /// <summary>
        /// Binds the buffer object buffer to the binding point at index index of the array of targets specified by target. 
        /// </summary>
        /// <param name="target">Specify the target of the bind operation. 
        /// Target must be one of <see cref="BufferTarget.AtomicCounterBuffer"/>, 
        ///                       <see cref="BufferTarget.TransformFeedbackBuffer"/>, 
        ///                       <see cref="BufferTarget.UniformBuffer"/>
        ///                    or <see cref="BufferTarget.ShaderStorageBuffer"/>.
        ///                    </param>
        /// <param name="index">Specify the index of the binding point within the array specified by target.</param>
        /// <param name="buffer">The name of a buffer object to bind to the specified binding point.</param>
        void BindBufferBase(BufferTarget target, int index, int buffer);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target">Specify the target of the bind operation. 
        /// Target must be one of <see cref="BufferTarget.AtomicCounterBuffer"/>, 
        ///                       <see cref="BufferTarget.TransformFeedbackBuffer"/>, 
        ///                       <see cref="BufferTarget.UniformBuffer"/>
        ///                    or <see cref="BufferTarget.ShaderStorageBuffer"/>.
        ///                    </param>
        /// <param name="index">Specify the index of the binding point within the array specified by target.</param>
        /// <param name="buffer">The name of a buffer object to bind to the specified binding point.</param>
        /// <param name="offset">The starting offset in basic machine units into the buffer object buffer.</param>
        /// <param name="size">The amount of data in machine units that can be read from the buffer object while used as an indexed target.</param>
        void BindBufferRange(BufferTarget target, int index, int buffer, IntPtr offset, UIntPtr size);

        /// <summary>
        /// Return some or all of the data contents of the data storage of the specified buffer object.
        /// 
        /// Data starting at byte offset <paramref name="offset"/> and extending for size of <paramref name="data"/> is copied from the buffer object's data store to the <paramref name="data"/>.
        /// 
        /// An error is thrown 
        /// </summary>
        /// <typeparam name="T">Data type.</typeparam>
        /// <param name="target">Specifies the target to which the buffer object is bound.</param>
        /// <param name="offset">Specifies the offset into the buffer object's data store from which data will be returned, measured in bytes.</param>
        /// <param name="data">Specifies the location where buffer object data is returned.</param>
        /// <exception cref="GLException">
        /// If the buffer object is currently mapped, or if offset and size together define a 
        /// range beyond the bounds of the buffer object's data store.
        /// </exception>
        void GetBufferSubData<T>(BufferTarget target, IntPtr offset, Span<T> data) where T : unmanaged;

        /// <summary>
        /// Create a new data store for a buffer object.
        /// </summary>
        /// <typeparam name="T">Data type.</typeparam>
        /// <param name="target">Specifies the target to which the buffer object is bound.</param>
        /// <param name="data">Specifies the data that will be copied into the data store for initialization, or <see cref="Span{T}.Empty"/> if no data is to be copied.</param>
        /// <param name="usage">Specifies the expected usage pattern of the data store.</param>
        void BufferData<T>(BufferTarget target, Span<T> data, BufferUsage usage) where T : unmanaged;

        /// <summary>
        /// Updates a subset of a buffer object's data store.
        /// </summary>
        /// <typeparam name="T">Data type.</typeparam>
        /// <param name="target">Specifies the target to which the buffer object is bound.</param>
        /// <param name="offset">Specifies the offset into the buffer object's data store where data replacement will begin, measured in bytes.</param>
        /// <param name="data">Specifies the new data that will be copied into the data store.</param>
        void BufferSubData<T>(BufferTarget target, IntPtr offset, ReadOnlySpan<T> data) where T : unmanaged;
        #endregion

        #region shaders and program

        /// <summary>
        /// Creates a single program object.
        /// </summary>
        /// <returns>Handle of program object.</returns>
        int CreateProgram();

        /// <summary>
        /// Deletes a single program object.
        /// </summary>
        /// <param name="program"></param>
        void DeleteProgram(int program);

        /// <summary>
        /// Attach a shader to a program object.
        /// </summary>
        /// <param name="program">Specifies the program object to which a shader object will be attached.</param>
        /// <param name="shader">Specifies the shader object that is to be attached.</param>
        void AttachShader(int program, int shader);

        /// <summary>
        /// Detach a shader of a program object.
        /// </summary>
        /// <param name="program">Specifies the program object from which to detach the shader object.</param>
        /// <param name="shader">Specifies the shader object to be detached.</param>
        void DetachShader(int program, int shader);

        /// <summary>
        /// Links the program object specified by <paramref name="program"/>.
        /// </summary>
        /// <param name="program">Specifies the handle of the program object to be linked.</param>
        void LinkProgram(int program);

        /// <summary>
        /// Installs the program object specified by program as part of current rendering state.
        /// </summary>
        /// <param name="program">Specifies the handle of the program object whose executables are to be used as part of current rendering state.</param>
        void UseProgram(int program);

        /// <summary>
        /// Creates a shader object.
        /// </summary>
        /// <param name="shaderType">Specifies the type of shader to be created.</param>
        /// <returns>Handle of shader object.</returns>
        int CreateShader(ShaderType shaderType);

        /// <summary>
        /// Delete the memory and invalidate the <paramref name="shader"/>.
        /// 
        /// If a shader object to be deleted is attached to a program object,
        /// it will be flagged for deletion, but it will not be deleted until 
        /// it is no longer attached to any program object, for any rendering 
        /// context (i.e., it must be detached from wherever it was attached 
        /// before it will be deleted). A value of 0 for shader will be silently
        /// ignored.
        /// 
        /// </summary>
        /// <param name="shader">Specifies the shader object to be deleted.</param>
        void DeleteShader(int shader);

        /// <summary>
        /// Sets the source code in <paramref name="shader"/>.
        /// </summary>
        /// <param name="shader">Specifies the handle of the shader object whose source code is to be replaced.</param>
        /// <param name="sourceCode">The shader code to loaded into the shader.</param>
        void ShaderSource(int shader, string sourceCode);

        /// <summary>
        /// Compiles the source code strings that have been stored in the shader object specified by shader.
        /// </summary>
        /// <param name="shader">Specifies the shader object to be compiled.</param>
        void CompileShader(int shader);

        void ValidateProgram(int program);
        string GetProgramInfoLog(int program);
        #endregion

        #region debug
        /// <summary>
        /// Gets the value of the error flag. 
        /// Each detectable error is assigned a numeric code and symbolic name. 
        /// When an error occurs, the error flag is set to the appropriate error 
        /// code value. No other errors are recorded until <see cref="GL.GLError"/> is called, 
        /// the error code is returned, and the flag is reset to <see cref="GLError.NoError"/>. 
        ///
        /// If a call to glGetError returns <see cref="GLError.NoError"/>, there has been no 
        /// detectable error since the last call to <see cref="GL.GLError"/>, or since the GL was initialized.
        /// </summary>
        /// <returns>The current error flag.</returns>
        GLError GetError();
        #endregion

        #region uniform buffer

        /// <summary>
        /// Retrieve the index of a named uniform block
        /// </summary>
        /// <param name="program">Specifies the name of a program containing the uniform block.</param>
        /// <param name="uniformBlockName">Specifies the name of the uniform block whose index to retrieve.</param>
        /// <returns>Uniform block index in the program.</returns>
        int GetUniformBlockIndex(int program, string uniformBlockName);

        /// <summary>
        /// Binding points for active uniform blocks are assigned using.
        /// 
        /// When a program object is linked or re-linked, the uniform buffer object binding point assigned to each of its active uniform blocks is reset to zero.
        /// 
        /// </summary>
        /// <param name="program">The name of a program object containing the active uniform block whose binding to assign.</param>
        /// <param name="uniformBlockIndex">The index of the active uniform block within program whose binding to assign.</param>
        /// <param name="uniformBlockBinding">Specifies the binding point to which to bind the uniform block with index uniformBlockIndex within program.</param>
        void UniformBlockBinding(int program, int uniformBlockIndex, int uniformBlockBinding);
        #endregion

        #region framebuffer
        int GenFramebuffer();
        void DeleteFramebuffer(int framebuffer);
        void BindFramebuffer(FramebufferTarget target, int framebuffer);
        FramebufferStatus CheckFramebufferStatus(FramebufferTarget target);
        void FramebufferTexture2D(FramebufferTarget target, FramebufferAttachment attachment, TextureTarget textureTarget, int texture, int level);
        void FramebufferRenderbuffer(FramebufferTarget target, FramebufferAttachment attachment, RenderbufferTarget renderbufferTarget, int renderbuffer);

        void BlitFramebuffer(Rectangle src, Rectangle dst, ClearMask mask, BlitFramebufferFilter filter);
        #endregion

        #region renderbuffer

        int GenRenderbuffer();
        void DeleteRenderbuffer(int renderbuffer);
        void BindRenderbuffer(RenderbufferTarget target, int renderbuffer);
        void RenderbufferStorage(RenderbufferTarget target, PixelInternalFormat internalFormat, int width, int height);
        void RenderbufferStorageMultisample(RenderbufferTarget target, int samples, PixelInternalFormat internalFormat, int width, int height);
        #endregion

        #region texture

        /// <summary>
        /// Generate a texture object.
        /// </summary>
        /// <returns>Handle of texture object.</returns>
        int GenTexture();

        /// <summary>
        /// Delete a texture object.
        /// </summary>
        /// <param name="texture"></param>
        void DeleteTexture(int texture);

        /// <summary>
        /// Define a texture image.
        ///  The arguments describe the parameters of the texture image, 
        ///  such as height, width, width of the border, level-of-detail number(<see cref="IGL.TexParameter"/>), 
        ///  and number of color components provided. The last three 
        ///  arguments describe how the image is represented in memory. 
        /// </summary>
        /// <typeparam name="T">Pixel type.</typeparam>
        /// <param name="target">Specifies the target texture.</param>
        /// <param name="level">Specifies the level-of-detail number.
        /// 
        /// Level 0 is the base image level. Level n is the nth mipmap reduction image. 
        /// 
        /// If target is <see cref="TextureTarget.TextureRectangle"/> or 
        /// <see cref="TextureTarget.ProxyTextureRectangle"/>, level must be 0.
        /// </param>
        /// <param name="internalFormat">Specifies the internal format of texture.</param>
        /// <param name="width">Specifies the width of the texture image.
        /// All implementations support texture images that are at least 1024 texels wide.</param>
        /// <param name="height">Specifies the height of the texture image, or the number of layers in a texture array,
        /// in the case of the <see cref="TextureTarget.Texture1DArray"/> and <see cref="TextureTarget.ProxyTexture1DArray"/>
        /// targets. All implementations support 2D texture images that are at least 1024 texels high, and texture arrays
        /// that are at least 256 layers deep.
        /// </param>
        /// <param name="border">This value must be 0.</param>
        /// <param name="pixelFormat">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel data.</param>
        /// <param name="pixels">Specifies the image data in memory.</param>
        void TexImage2D<T>(TextureTarget target, int level, PixelInternalFormat internalFormat, int width, int height, int border, PixelFormat pixelFormat, PixelType type, ReadOnlySpan<T> pixels) where T : unmanaged;

        void TexImage2DMultisample(TextureTarget target, int samples, PixelInternalFormat internalFormat, int width, int height, bool fixedSampleLocations);

        /// <summary>
        /// Assign the value in <paramref name="param"/> to the texture parameter specified as <paramref name="pname"/>.
        /// </summary>
        /// <param name="target">Specifies the target to which the texture is bound.</param>
        /// <param name="pname">Specifies the symbolic name of a single-valued texture parameter.</param>
        /// <param name="param">Specifies the value to storage.</param>
        void TexParameter(TextureTarget target, TextureParameterName pname, float param);

        /// <summary>
        /// Assign the values in <paramref name="params"/> to the texture parameter specified as <paramref name="pname"/>.
        /// </summary>
        /// <param name="target">Specifies the target to which the texture is bound.</param>
        /// <param name="pname">Specifies the symbolic name of a single-valued texture parameter.</param>
        /// <param name="params">Specifies an array where the value or values of pname are stored.</param>
        void TexParameter(TextureTarget target, TextureParameterName pname, ReadOnlySpan<float> @params);

        /// <summary>
        /// Assign the value in <paramref name="param"/> to the texture parameter specified as <paramref name="pname"/>.
        /// </summary>
        /// <param name="target">Specifies the target to which the texture is bound.</param>
        /// <param name="pname">Specifies the symbolic name of a single-valued texture parameter.</param>
        /// <param name="param">Specifies the value to storage.</param>
        void TexParameter(TextureTarget target, TextureParameterName pname, int param);

        /// <summary>
        /// Assign the values in <paramref name="params"/> to the texture parameter specified as <paramref name="pname"/>.
        /// </summary>
        /// <param name="target">Specifies the target to which the texture is bound.</param>
        /// <param name="pname">Specifies the symbolic name of a single-valued texture parameter.</param>
        /// <param name="params">Specifies an array where the value or values of pname are stored.</param>
        void TexParameter(TextureTarget target, TextureParameterName pname, ReadOnlySpan<int> @params);

        /// <summary>
        /// Assign the value in <paramref name="param"/> to the texture parameter specified as <paramref name="pname"/>.
        /// </summary>
        /// <param name="target">Specifies the target to which the texture is bound.</param>
        /// <param name="pname">Specifies the symbolic name of a single-valued texture parameter.</param>
        /// <param name="param">Specifies the value to storage.</param>
        void TexParameter(TextureTarget target, TextureParameterName pname, TextureMinFilter param);

        /// <summary>
        /// Assign the value in <paramref name="param"/> to the texture parameter specified as <paramref name="pname"/>.
        /// </summary>
        /// <param name="target">Specifies the target to which the texture is bound.</param>
        /// <param name="pname">Specifies the symbolic name of a single-valued texture parameter.</param>
        /// <param name="param">Specifies the value to storage.</param>
        void TexParameter(TextureTarget target, TextureParameterName pname, TextureMagFilter param);

        /// <summary>
        /// Generates mipmaps for the specified texture object.
        /// </summary>
        /// <param name="textureTarget">Specifies the target to which the texture object is bound for glGenerateMipmap. 
        /// Must be one of <see cref="TextureTarget.Texture1D"/>, 
        ///                <see cref="TextureTarget.Texture2D"/>, 
        ///                <see cref="TextureTarget.Texture3D"/>, 
        ///                <see cref="TextureTarget.Texture1DArray"/>, 
        ///                <see cref="TextureTarget.Texture2DArray"/>,
        ///                <see cref="TextureTarget.TextureCubeMap"/>, or
        ///                <see cref="TextureTarget.TextureCubeMapArray"/>.
        ///</param>
        void GenerateMipmap(TextureTarget target);

        /// <summary>
        /// Selects which texture unit subsequent texture state calls will affect. The number of texture units an implementation supports is implementation dependent, but must be at least 80.
        /// </summary>
        /// <param name="textureUnit">Specifies which texture unit to make active. The number of texture units is implementation dependent, but must be at least 80.</param>
        void ActiveTexture(int textureUnit);

        /// <summary>
        /// Bind a <paramref name="texture"/> to the <paramref name="target"/>.;
        /// </summary>
        /// <param name="target">Specifies the target to which the texture is bound.</param>
        /// <param name="texture">Specifies the name of a texture.</param>
        void BindTexture(TextureTarget target, int texture);
        #endregion

        #region vertex array

        /// <summary>
        /// Create a vertex array object.
        /// </summary>
        /// <returns>Handle of vertex array object.</returns>
        int GenVertexArray();

        /// <summary>
        /// Delete a vertex array object.
        /// </summary>
        /// <param name="array">The vertex array object to delete.</param>
        void DeleteVertexArray(int array);

        /// <summary>
        /// Binds the vertex array object.
        /// </summary>
        /// <param name="array">Specifies the name of the vertex array to bind.</param>
        void BindVertexArray(int array);

        /// <summary>
        /// Specify the location and data format of the array of generic vertex attributes at index index to use when rendering.
        /// </summary>
        /// <param name="index">Specifies the index of the generic vertex attribute to be modified.</param>
        /// <param name="size">Specifies the number of components per generic vertex attribute. Must be 1, 2, 3, 4.</param>
        /// <param name="type">Specifies the data type of each component in the array.</param>
        /// <param name="normalized">Specifies whether fixed-point data values should be normalized.</param>
        /// <param name="stride">Specifies the byte offset between consecutive generic vertex attributes. 
        /// If stride is 0, the generic vertex attributes are understood to be tightly packed in the array. 
        /// The initial value is 0.
        /// </param>
        /// <param name="offset">Specifies a offset of the first component of the first generic vertex 
        /// attribute in the array in the data store of the buffer currently bound to the <see cref="BufferTarget.ArrayBuffer"/>
        /// target. The initial value is 0.</param>
        void VertexAttribPointer(uint index, int size, VertexAttribPointerType type, bool normalized, int stride, int offset);

        /// <summary>
        /// Enable or disable a generic vertex attribute array.
        /// </summary>
        /// <param name="index">Specifies the index of the generic vertex attribute to be enabled or disabled.</param>
        void EnableVertexAttribArray(uint index);
        #endregion

        #region draw calls

        /// <summary>
        /// Specifies multiple geometric primitives with very few subroutine calls. 
        /// Instead of calling a GL procedure to pass each individual vertex, normal,
        /// texture coordinate, edge flag, or color, you can prespecify separate arrays 
        /// of vertices, normals, and colors and use them to construct a sequence of
        /// primitives with a single call.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render.</param>
        /// <param name="first">Specifies the starting index in the enabled arrays.</param>
        /// <param name="count">Specifies the number of indices to be rendered.</param>
        void DrawArrays(PrimitiveType mode, int first, int count);

        /// <summary>
        /// Specifies multiple geometric primitives with very few subroutine calls. 
        /// Instead of calling a GL function to pass each individual vertex, normal,
        /// texture coordinate, edge flag, or color, you can prespecify separate arrays
        /// of vertices, normals, and so on, and use them to construct a sequence of 
        /// primitives with a single call.
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in indices.</param>
        /// <param name="offset">Specifies offset of indices.</param>
        void DrawElements(PrimitiveType mode, int count, DrawElementsType type, int offset);

        void ClearColor(Color color);
        void ClearColor(Vector4 color);
        void ClearColor(float red, float green, float blue, float alpha);

        void Clear(ClearMask mask);

        void Enable(EnableCap cap);
        #endregion
    }
}
