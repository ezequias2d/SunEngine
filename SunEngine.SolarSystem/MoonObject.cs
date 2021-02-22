// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using SunEngine.Core;
using SunEngine.Graphics;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SunEngine.SolarSystem
{
    public class MoonObject : PlanetObject
    {
        private readonly PlanetObject _planet;

        public MoonObject(PlanetObject planet, SolarObject solarObject, World world, Model sphere, Shader shader, string projectionViewBindingName, string modelBindingName, params GpuTexture[] textures) : base(solarObject, world, sphere, shader, projectionViewBindingName, modelBindingName, textures)
        {
            _planet = planet;
        }

        public override (double X, double Y, double Z) Center { get => _planet.Position; set => _planet.Position = value; }

        public override void Update(GameObject sender, ElapsedTimeEventArgs e)
        {
            Time += e.Time * TimeRate;

            var position = ComputeCoordinates();
            Position = (position.x * _planet.Scale + Center.X, position.y * _planet.Scale + Center.Y, position.z * _planet.Scale + Center.Z);

            sender.Position = (new Vector3((float)Position.X, (float)Position.Y, (float)Position.Z) + sender.Position) / 2f;
            sender.Scale = Vector3.One * Scale * PlanetSize * _planet.Scale;

            (Vector3 axis, Quaternion rotation) = GetAxialTiltVector(AxialTilt);
            double daytime = (Time / (RotationHours * 3600) + 0.125) % 1;
            sender.Rotation = Quaternion.Concatenate(rotation, Quaternion.CreateFromAxisAngle(axis, (float)(daytime * 360.0 * SunMath.Deg2RadD)));
        }
    }
}
