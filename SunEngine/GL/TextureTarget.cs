// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/. --%>

namespace SunEngine.GL
{
    public enum TextureTarget
    {
        Texture1D,
        Texture2D,
        ProxyTexture1D,
        ProxyTexture2D,        
        Texture3D,
        ProxyTexture3D,
        TextureRectangle,
        ProxyTextureRectangle,
        TextureCubeMap,
        TextureCubeMapPositiveX,
        TextureCubeMapNegativeX,
        TextureCubeMapPositiveY,
        TextureCubeMapNegativeY,
        TextureCubeMapPositiveZ,
        TextureCubeMapNegativeZ,
        ProxyTextureCubeMap,
        Texture1DArray,
        ProxyTexture1DArray,
        Texture2DArray,
        ProxyTexture2DArray,
        TextureBuffer,
        TextureCubeMapArray,
        ProxyTextureCubeMapArray,
        Texture2DMultisample,
        ProxyTexture2DMultisample,
        Texture2DMultisampleArray,
        ProxyTexture2DMultisampleArray
    }
}
