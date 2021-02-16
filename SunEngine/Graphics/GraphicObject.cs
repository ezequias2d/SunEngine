// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/. --%>
using SunEngine.GL;
using System;

namespace SunEngine.Graphics
{
    public abstract class GraphicObject : ResourceObject, IEquatable<GraphicObject>
    {
        public readonly int Handle;
        public GraphicObject(IGL gl, int handle) : base(gl)
        {
            Handle = handle;
            GL.CheckErros();
        }

        public override int GetHashCode()
        {
            return Handle;
        }

        public bool Equals(GraphicObject other)
        {
            return other.Handle == Handle;
        }

        public override bool Equals(object obj)
        {
            if (obj is GraphicObject)
                return Equals(obj as GraphicObject);
            return false;
        }
    }
}
