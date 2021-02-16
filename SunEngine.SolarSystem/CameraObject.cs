// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/. --%>
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
        private CameraComponent Camera { get; }
        private World World { get; }
        private IInput Input { get; }
        
        private Vector3 EulerPlanet;


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

            Focus = CameraFocus.Free;
            EulerPlanet = Vector3.Zero;
        }
        public CameraFocus Focus { get; set; }

        private void Increment()
        {
            if (Focus == CameraFocus.Last)
                Focus = CameraFocus.First;
            else
                Focus++;
        }
        private void Decrement()
        {
            if (Focus == CameraFocus.First)
                Focus = CameraFocus.Last;
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

            if(Focus == CameraFocus.Free)
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

            sender.Rotation = SunMath.ToQuaternion(SunMath.ToEulerAngles(sender.Rotation) + direction * (float)e.Time * 45f);
        }

        private void PlanetRotate(GameObject _, ElapsedTimeEventArgs e)
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

            EulerPlanet += direction * (float)e.Time * 90f;

            EulerPlanet = new Vector3(
                (EulerPlanet.X > 0) ? (EulerPlanet.X % 360) : (360 - (EulerPlanet.X * (-1) % 360)),
                (EulerPlanet.Y > 0) ? (EulerPlanet.Y % 360) : (360 - (EulerPlanet.Y * (-1) % 360)),
                (EulerPlanet.Z > 0) ? (EulerPlanet.Z % 360) : (360 - (EulerPlanet.Z * (-1) % 360)));

            Console.WriteLine("EulerPlanet: " + EulerPlanet);
        }

        private void PlanetFocus(string objectName, GameObject sender, ElapsedTimeEventArgs e)
        {
            PlanetRotate(sender, e);

            var planet = World.Get(objectName).FirstOrDefault();
            if (planet != null)
            {
                Camera.Camera.NearDistance = planet.Scale.X * 0.95f;

                Vector3 offset = new Vector3(0, 0, 1f);
                offset = Vector3.Transform(offset, SunMath.ToQuaternion(EulerPlanet));
                Console.WriteLine("Offset:" + offset);

                Camera.Position = (planet.Position + offset * planet.Scale.X * 3f + Camera.Position) / 2f;
                Camera.Rotation = SunMath.ToQuaternion(EulerPlanet);
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
