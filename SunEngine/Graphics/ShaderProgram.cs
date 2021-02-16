// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/. --%>
using SunEngine.GL;
using System.Text;

namespace SunEngine.Graphics
{
    public class ShaderProgram
    {
        public ShaderType ShaderType { get; }
        public bool IsASCII { get; }
        public byte[] Data { get; }
        public string ASCIIContent => IsASCII ? Encoding.ASCII.GetString(Data): string.Empty;

        public ShaderProgram(ShaderType type, bool isASCII, byte[] data)
        {
            ShaderType = type;
            IsASCII = isASCII;
            Data = data;
        }
    }
}
