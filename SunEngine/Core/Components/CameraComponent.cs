// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using SunEngine.Data;
using SunEngine.Graphics;
using System.Numerics;

namespace SunEngine.Core.Components
{
    public class CameraComponent : IDrawable
    {
        public CameraComponent(float aspectRatio)
        {
            Camera = new Camera
            {
                AspectRatio = aspectRatio,
                NearDistance =  0.1f,
                FarDistance = 100.0f,
                FieldOfView = SunMath.ToRadians(60f),
                Name = "MainCamera",
                OrthographicSize = 500f,
                Position = Vector3.Zero,
                Rotation = Quaternion.Identity,
                Up = Vector3.UnitY
            };
            cameraToGameObject = false;
        }
        public Camera Camera;
        public bool cameraToGameObject;
        public bool IsPerspective;

        public void BeforeDraw(GameObject sender, ElapsedTimeEventArgs e)
        {
            if (cameraToGameObject)
            {
                sender.Rotation = Camera.Rotation;
                sender.Position = Camera.Position;
                cameraToGameObject = false;
            }
            else
            {
                Camera.Rotation = sender.Rotation;
                Camera.Position = sender.Position;
            }
        }

        public void LookAt(Vector3 target)
        {
            Camera.LookAt(target);
            cameraToGameObject = true;
        }

        public void Draw(GameObject gameObject, ElapsedTimeEventArgs e)
        {
            
        }

        public Vector3 Position 
        {
            get
            {
                return Camera.Position;
            }
            set
            {
                if(Camera.Position != value)
                {
                    Camera.Position = value;
                    cameraToGameObject = true;
                }
            }
        }

        public Quaternion Rotation
        {
            get
            {
                return Camera.Rotation;
            }
            set
            {
                if (Camera.Rotation != value)
                {
                    Camera.Rotation = value;
                    cameraToGameObject = true;
                }
            }
        }
    }
}
