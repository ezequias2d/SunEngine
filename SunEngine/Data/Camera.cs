// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System.Numerics;

namespace SunEngine.Data
{
    /// <summary>
    /// A virtual camera.
    /// </summary>
    public struct Camera
    {
        /// <summary>
        /// The name of the camera.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The up vector of camera.
        /// </summary>
        public Vector3 Up { get; set; }

        /// <summary>
        /// Position of camera in world space.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Rotation of camera in world space.
        /// </summary>
        public Quaternion Rotation { get; set; }

        /// <summary>
        /// The field of view in the y direction, in radians.
        /// </summary>
        public float FieldOfView { get; set; }

        /// <summary>
        /// Camera's half-size when in orthographic mode.
        /// </summary>
        public float OrthographicSize { get; set; }

        /// <summary>
        /// The distance to the near view plane.
        /// </summary>
        public float NearDistance { get; set; }

        /// <summary>
        /// The distance to the far view plane.
        /// </summary>
        public float FarDistance { get; set; }

        /// <summary>
        /// The aspect ratio, defined as view space width divided by height.
        /// </summary>
        public float AspectRatio { get; set; }

        /// <summary>
        /// Look the camera to the target.
        /// </summary>
        /// <param name="target">Target</param>
        public void LookAt(Vector3 target)
        {
            Matrix4x4 lookAtMatrix = Matrix4x4.CreateLookAt(Position, target, Up);
            Matrix4x4.Decompose(lookAtMatrix, out _, out Quaternion rot, out _);
            Rotation = rot;
        }

        public Matrix4x4 CreateOthograph() =>
            Matrix4x4.CreateOrthographic(OrthographicSize, OrthographicSize * AspectRatio, NearDistance, FarDistance);

        public Matrix4x4 CreateView()
        {
            //Vector3 cameraFront = Vector3.Transform(new Vector3(0.0f, 0.0f, -1.0f), Rotation);

            //return Matrix4x4.CreateLookAt(Position, Position + cameraFront, Up);
            return Matrix4x4.CreateTranslation(-Position) *
                Matrix4x4.CreateFromQuaternion(Rotation);
        }

        public Matrix4x4 CreatePerspective() => Matrix4x4.CreatePerspectiveFieldOfView(FieldOfView, AspectRatio, NearDistance, FarDistance);
    }
}
