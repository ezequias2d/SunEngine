// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/. --%>
using SunEngine.GL;
using SunEngine.Inputs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SunEngine.Core
{
    public sealed class World
    {
        private readonly IDictionary<Guid, GameObject> _gameObjects;
        private readonly IDictionary<string, IList<GameObject>> _gameObjectsByName;
        private readonly IDictionary<string, IList<GameObject>> _gameObjectsByTag;
        private readonly IList<ISubsystem> _subsystems;
        internal readonly Size _screenSize;

        public World(IGL gl, IInput input, Size screenSize, int samples = 16)
        {
            _screenSize = screenSize;
            _gameObjects = new Dictionary<Guid, GameObject>();
            _gameObjectsByName = new Dictionary<string, IList<GameObject>>();
            _gameObjectsByTag = new Dictionary<string, IList<GameObject>>();
            _subsystems = new List<ISubsystem>();

            DrawSubsystem = new DrawSubsystem(this, gl, screenSize, samples);
            AddSubsystem(DrawSubsystem);

            ComponentSubsystem = new ComponentSubsystem(this);
            AddSubsystem(ComponentSubsystem);

            Input = input;
        }

        public IEnumerable<GameObject> GameObjects => _gameObjects.Values;


        public DrawSubsystem DrawSubsystem { get; }
        public ComponentSubsystem ComponentSubsystem { get; }

        public IInput Input { get; }

        public void AddSubsystem(ISubsystem subsystem)
        {
            _subsystems.Add(subsystem);
        }

        public void Attach(object obj, GameObject gameObject)
        {
            foreach (var subsystem in _subsystems)
                subsystem.Add(obj, gameObject);
        }

        public void Detach(object obj)
        {
            foreach (var subsystem in _subsystems)
                subsystem.Remove(obj);
        }

        public void Detach(GameObject gameObject)
        {
            foreach (var subsystem in _subsystems)
                subsystem.Remove(gameObject);
        }

        public GameObject New(string name, string tag)
        {
            GameObject gameObject = new GameObject(this)
            {
                Name = name,
                Tag = tag
            };


            _gameObjects[gameObject.ID] = gameObject;
            AddBy(_gameObjectsByName, name, gameObject);
            AddBy(_gameObjectsByTag, tag, gameObject);

            gameObject.ChangeName += OnChangeName;
            gameObject.ChangeTag += OnChangeTag;

            return gameObject;
        }

        public void Destroy(GameObject gameObject)
        {
            gameObject.ChangeName -= OnChangeName;
            gameObject.ChangeTag -= OnChangeTag;

            Detach(gameObject);

            _gameObjects.Remove(gameObject.ID);
            RemoveBy(_gameObjectsByName, gameObject.Name, gameObject);
            RemoveBy(_gameObjectsByTag, gameObject.Tag, gameObject);
        }

        public GameObject Get(Guid id) => _gameObjects[id];

        public IEnumerable<GameObject> Get(string name) 
        {
            _gameObjectsByName.TryGetValue(name, out var value);
            return value ?? Array.Empty<GameObject>();
        }

        private void OnChangeName(object sender, (string older, string newer) e)
        {
            GameObject gameObject = sender as GameObject;

            RemoveBy(_gameObjectsByName, e.older, gameObject);
            AddBy(_gameObjectsByName, e.newer, gameObject);
        }

        private void OnChangeTag(object sender, (string older, string newer) e)
        {
            GameObject gameObject = sender as GameObject;

            RemoveBy(_gameObjectsByTag, e.older, gameObject);
            AddBy(_gameObjectsByTag, e.newer, gameObject);
        }

        private static bool RemoveBy(IDictionary<string, IList<GameObject>> dic, string value, GameObject gameObject)
        {
            if (dic.ContainsKey(value))
            {
                bool aux =  dic[value].Remove(gameObject);
                if (dic[value].Count == 0)
                    dic.Remove(value);
                return aux;
            }
            return false;
        }

        private static void AddBy(IDictionary<string, IList<GameObject>> dic, string value, GameObject gameObject)
        {
            if (dic.ContainsKey(value) && dic[value] != null)
                dic[value].Add(gameObject);
            else
            {
                List<GameObject> list = new List<GameObject>();
                dic[value] = list;
                list.Add(gameObject);
            }
        }
    }
}
