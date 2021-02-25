// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using SunEngine.Core;
using SunEngine.Core.Components;
using SunEngine.Graphics;
using SunEngine.Inputs;
using SunEngine.Windowing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace SunEngine.SolarSystem
{
    public sealed class CameraObject : IComponent
    {
        private float _zoom;
        private readonly ISunWindow _window;
        private readonly IDictionary<SolarObject, PlanetObject> _solarObjects;
        public CameraObject(World world, ISunWindow window, IDictionary<SolarObject, PlanetObject> solarObjects)
        {
            _solarObjects = solarObjects;
            _window = window;

            World = world;
            Input = world.Input;

            GameObject gameObject = world.New("CameraObject", "Camera");
            gameObject.Position = new Vector3(0, 0, 31);
            Camera = new CameraComponent(_window.Dimension.Width / (float)_window.Dimension.Height);
            Camera.Camera.FieldOfView = SunMath.Deg2RadF * 45f;
            Camera.IsPerspective = true;
            Camera.Camera.FarDistance = 50;
            
            world.Attach(Camera, gameObject);
            world.Attach(this, gameObject);

            world.DrawSubsystem.MainCamera = Camera;

            Focus = SolarObject.None;
            _zoom = 1;
        }
        public SolarObject Focus { get; set; }
        internal CameraComponent Camera { get; }
        private World World { get; }
        private IInput Input { get; }

        private void Increment()
        {
            if (Focus == SolarObject.Last)
                Focus = SolarObject.First;
            else
                Focus++;

            lerpValue = 0;
            UpdateWindowTitle();
        }
        private void Decrement()
        {
            if (Focus == SolarObject.First)
                Focus = SolarObject.Last;
            else
                Focus--;

            lerpValue = 0;
            UpdateWindowTitle();
        }
        public void LateUpdate(GameObject sender, ElapsedTimeEventArgs e)
        {
            var keyboard = Input.GetKeyboardState();

            if (keyboard[Key.PageUp] == KeyEvent.Down)
                Increment();
            else if (keyboard[Key.PageDown] == KeyEvent.Down)
                Decrement();

            if (keyboard[Key.End] == KeyEvent.Down)
            {
                Focus = SolarObject.None;
                UpdateWindowTitle();
            }

            if(Focus == SolarObject.None)
                FreeCamera(sender, e);
            else
                PlanetFocus(Focus.ToString(), sender, e);
        }

        private void UpdateWindowTitle()
        {
            const string mainTitle = "Solar System";
            if (Focus == SolarObject.None)
                _window.Title = $"{mainTitle} - Free camera";
            else
            {
                var name = Focus.ToString();
                var planet = _solarObjects[Focus];
                var complement = string.Empty;
                
                if(planet is MoonObject moon)
                {
                    var solarObjectName = moon.Father.SolarObject.ToString();
                    var article = "A";
                    if (IsVowel(solarObjectName[0]))
                        article = "An";
                    complement = $"({article} {solarObjectName} moon)";
                }
                
                _window.Title = $"{mainTitle} - Focused in {name}{complement}";
            }
        }

        private bool IsVowel(char c)
        {
            return "aeiouAEIOU".IndexOf(c) >= 0;
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

            sender.Rotation = Quaternion.Inverse(SunMath.ToQuaternion(euler));
        }

        Vector3 euler = Vector3.Zero;
        float lerpValue = 0;
        private void PlanetFocus(string objectName, GameObject sender, ElapsedTimeEventArgs e)
        {
            lerpValue = Math.Min((float)(lerpValue + e.Time), 1f);

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

                euler += rotatedirection * (float)e.Time * 90f;


                float min = planet.Scale.X * 4f;

                if (keyboard[Key.W] == KeyEvent.Repeat)
                    _zoom -= (float)e.Time * _zoom;

                if(keyboard[Key.S] == KeyEvent.Repeat)
                    _zoom += (float)e.Time * _zoom;

                if (keyboard[Key.A] == KeyEvent.Down)
                    _zoom = min;

                if (keyboard[Key.D] == KeyEvent.Down)
                    _zoom = 1f;

                _zoom = Math.Max(_zoom, min);
                
                Vector3 offset = Vector3.Transform(Vector3.UnitZ, SunMath.ToQuaternion(euler));
                Camera.Position = Vector3.Lerp(Camera.Position, planet.Position + offset * _zoom, lerpValue);
                Camera.LookAt(planet.Position);
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

            direction = Vector3.Transform(direction, Quaternion.Inverse(sender.Rotation));
            sender.Position += direction * (float)e.Time;
        }

        public void Update(GameObject sender, ElapsedTimeEventArgs e)
        {
        }
    }
}
