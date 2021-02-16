// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/. --%>
using System;
using System.Numerics;

namespace SunEngine.Core
{
    public class GameObject
    {
        private string _name;
        private string _tag;

        internal GameObject(World world)
        {
            World = world;
            ID = Guid.NewGuid();
            Rotation = Quaternion.Identity;
            Scale = Vector3.One;
        }

        public World World { get; }
        public Guid ID { get; }
        public Vector3 Position 
        { 
            get; 
            set; 
        }
        public Quaternion Rotation { get; set; }
        public Vector3 Scale { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    string aux = _name;
                    _name = value;
                    ChangeName?.Invoke(this, (aux, value));
                }
            }
        }
        public string Tag 
        { 
            get => _tag;
            set 
            {
                if(_tag != value)
                {
                    string aux = _tag;
                    _tag = value;
                    ChangeTag?.Invoke(this, (aux, value));
                }
            }
        }

        internal EventHandler<(string older, string newer)> ChangeName { get; set; }
        internal EventHandler<(string older, string newer)> ChangeTag { get; set; }
    }
}
