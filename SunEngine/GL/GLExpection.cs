// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Runtime.Serialization;

namespace SunEngine.GL
{
    public class GLExpection : Exception
    {
        public GLExpection()
        {
        }

        public GLExpection(string message) : base(message)
        {
        }

        public GLExpection(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GLExpection(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
