// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
namespace SunEngine.Data.Meshes
{
    public struct TriangleFace
    {
        public uint Vertex1;
        public uint Vertex2;
        public uint Vertex3;

        public TriangleFace(uint vertex1, uint vertex2, uint vertex3)
        {
            Vertex1 = vertex1;
            Vertex2 = vertex2;
            Vertex3 = vertex3;
        }
    }
}
