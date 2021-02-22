// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using SunEngine.Core;
using SunEngine.Core.Components;
using SunEngine.Graphics;
using SunEngine.Inputs;
using System;
using System.Linq;
using System.Numerics;

namespace SunEngine.SolarSystem
{
    public sealed class CameraObject : IComponent
    {
        private float _zoom;
        public CameraObject(World world, float aspectRatio)
        {
            World = world;
            Input = world.Input;

            GameObject gameObject = world.New("CameraObject", "Camera");
            gameObject.Position = new Vector3(0, 0, 31);
            Camera = new CameraComponent(aspectRatio);
            Camera.Camera.FieldOfView = SunMath.ToRadians(45f);
            Camera.IsPerspective = true;
            Camera.Camera.FarDistance = 50;
            
            world.Attach(Camera, gameObject);
            world.Attach(this, gameObject);

            world.DrawSubsystem.MainCamera = Camera;

            Focus = SolarObject.None;
            _zoom = 1;
        }
        public SolarObject Focus { get; set; }
        private CameraComponent Camera { get; }
        private World World { get; }
        private IInput Input { get; }

        private void Increment()
        {
            if (Focus == SolarObject.Last)
                Focus = SolarObject.First;
            else
                Focus++;
        }
        private void Decrement()
        {
            if (Focus == SolarObject.First)
                Focus = SolarObject.Last;
            else
                Focus--;
        }
        public void Update(GameObject sender, ElapsedTimeEventArgs e)
        {
            var keyboard = Input.GetKeyboardState();

            if (keyboard[Key.PageUp] == KeyEvent.Down)
                Increment();
            else if (keyboard[Key.PageDown] == KeyEvent.Down)
                Decrement();

            if (keyboard[Key.End] == KeyEvent.Down)
                Focus = SolarObject.None;

            if(Focus == SolarObject.None)
                FreeCamera(sender, e);
            else
                PlanetFocus(Focus.ToString(), sender, e);
            
        }

        private void FreeRotate(GameObject sender, ElapsedTimeEventArgs e)
        {
            var keyboard = Input.GetKeyboardState();

            Vector3 direction = Vector3.Zero;
            if (keyboard[Key.Down] == KeyEvent.Repeat)
                direction -= Vector3.UnitX;

            if (keyboard[Key.Up] == KeyEvent.Repeat)
                direction += Vector3.UnitX;

            if (keyboard[Key.Left] == KeyEvent.Repeat)
                direction += Vector3.UnitY;

            if (keyboard[Key.Right] == KeyEvent.Repeat)
                direction -= Vector3.UnitY;

            euler += direction * (float)e.Time * 45f;

            sender.Rotation = SunMath.ToQuaternion(euler);
        }

        Vector3 euler = Vector3.Zero;
        private void PlanetFocus(string objectName, GameObject sender, ElapsedTimeEventArgs e)
        {
            var keyboard = Input.GetKeyboardState();
            var planet = World.Get(objectName).FirstOrDefault();
            if (planet != null)
            {
                Vector3 rotatedirection = Vector3.Zero;
                if (keyboard[Key.Down] == KeyEvent.Repeat)
                    rotatedirection -= Vector3.UnitX;

                if (keyboard[Key.Up] == KeyEvent.Repeat)
                    rotatedirection += Vector3.UnitX;

                if (keyboard[Key.Left] == KeyEvent.Repeat)
                    rotatedirection += Vector3.UnitY;

                if (keyboard[Key.Right] == KeyEvent.Repeat)
                    rotatedirection -= Vector3.UnitY;

                Camera.Camera.NearDistance = planet.Scale.X * 0.9f;

                {
                    euler += rotatedirection * (float)e.Time * 90f;
                    Camera.Rotation = Quaternion.Lerp(SunMath.ToQuaternion(euler), Camera.Rotation, 0.5f);
                }

                float min = planet.Scale.X * 4f;

                if (keyboard[Key.W] == KeyEvent.Repeat)
                    _zoom -= (float)e.Time;

                if(keyboard[Key.S] == KeyEvent.Repeat)
                    _zoom += (float)e.Time;

                if (keyboard[Key.A] == KeyEvent.Down)
                    _zoom = min;

                if (keyboard[Key.D] == KeyEvent.Down)
                    _zoom = 1f;

                _zoom = Math.Max(_zoom, min);

                Vector3 offset = Vector3.Transform(Vector3.UnitZ, Camera.Rotation);
                Camera.Position = (planet.Position + offset * _zoom + Camera.Position) / 2f;


            }
            else
                Increment();

        }

        private void FreeCamera(GameObject sender, ElapsedTimeEventArgs e)
        {
            Camera.Camera.NearDistance = 0.01f;
            FreeRotate(sender, e);

            var keyboard = Input.GetKeyboardState();

            Vector3 direction = Vector3.Zero;

            if (keyboard[Key.S] == KeyEvent.Repeat)
                direction += Vector3.UnitZ;

            if (keyboard[Key.W] == KeyEvent.Repeat)
                direction -= Vector3.UnitZ;

            if (keyboard[Key.A] == KeyEvent.Repeat)
                direction -= Vector3.UnitX;

            if (keyboard[Key.D] == KeyEvent.Repeat)
                direction += Vector3.UnitX;


            if (keyboard[Key.Q] == KeyEvent.Repeat)
                direction += Vector3.UnitY;

            if (keyboard[Key.E] == KeyEvent.Repeat)
                direction -= Vector3.UnitY;

            direction = Vector3.Transform(direction, sender.Rotation);
            sender.Position += direction * (float)e.Time;
        }
    }
}
