// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/. --%>
using SunEngine.GL;
using System;

namespace SunEngine.Graphics
{
    public abstract class ResourceObject : IDisposable
    {
        public IGL GL { get; }
        public bool IsDisposed { get; private set; }

        public ResourceObject(IGL gl)
        {
            GL = gl;
            IsDisposed = false;
        }

        protected abstract void Dispose(bool disposing);

        public void Dispose()
        {
            if (IsDisposed) return;
            Dispose(true);
            IsDisposed = true;
            GC.SuppressFinalize(this);
        }

        ~ResourceObject()
        {
            Dispose(false);
        }
    }
}
