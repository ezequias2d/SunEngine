using SunEngine.Core;
using SunEngine.Core.Components;
using SunEngine.Graphics;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SunEngine.SolarSystem
{
    public sealed class PlanetRing : IComponent
    {
        public PlanetRing(World world, PlanetObject planetObject, string objectName, Model ring, Shader shader, string projectionViewBindingName, string modelBindingName, params GpuTexture[] textures)
        {
            PlanetObject = planetObject;
            
            var gameObject = world.New(objectName, "Ring");
            world.Attach(this, gameObject);

            ModelComponent modelComponent = new ModelComponent(ring, shader, projectionViewBindingName, modelBindingName)
            {
                Textures = textures
            };

            world.Attach(modelComponent, gameObject);
        }

        public PlanetObject PlanetObject { get; }

        public void LateUpdate(GameObject sender, ElapsedTimeEventArgs e)
        {
            
        }

        public void Update(GameObject sender, ElapsedTimeEventArgs e)
        {
            sender.Position = new Vector3((float)PlanetObject.Position.X, (float)PlanetObject.Position.Y, (float)PlanetObject.Position.Z);
            sender.Rotation = PlanetObject.Rotation;
            sender.Scale = PlanetObject.Scale * PlanetObject.PlanetSize * Vector3.One;
        }
    }
}
