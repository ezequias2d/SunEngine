// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/. --%>
using System;
using System.Collections;
using System.Collections.Generic;

namespace SunEngine.Core
{
    public class BaseSubsystem<T> : ISubsystem, IEnumerable<KeyValuePair<T, Guid>>
    {
        private readonly IDictionary<Guid, IList<T>> _ts;
        private readonly IDictionary<T, Guid> _tsInv;

        public BaseSubsystem(World world)
        {
            World = world;
            _ts = new Dictionary<Guid, IList<T>>();
            _tsInv = new Dictionary<T, Guid>();
        }

        public World World { get; }
        public GameEventHandle<T> BeforeAdd { get; set; }
        public GameEventHandle<T> BeforeRemove { get; set; }

        public void Add(object obj, GameObject gameObject)
        {
            if (obj is T t)
            {
                if (!_ts.ContainsKey(gameObject.ID))
                    _ts[gameObject.ID] = new List<T>();

                _ts[gameObject.ID].Add(t);
                _tsInv[t] = gameObject.ID;

                BeforeAdd?.Invoke(gameObject, t);
            }
        }
        public IEnumerable Get(GameObject gameObject) 
        {
            return _ts[gameObject.ID];
        }
        public void Remove(object obj)
        {
            if (obj is T t)
            {
                var guid = _tsInv[t];
                _ts[guid].Remove(t);
                _tsInv.Remove(t);

                BeforeRemove?.Invoke(World.Get(guid), t);
            }
        }

        public void Remove(GameObject gameObject)
        {
            var id = gameObject.ID;
            IEnumerable<T> ts = _ts[id];
            _ts.Remove(id);
            foreach (var t in ts)
            {
                _tsInv.Remove(t);
                BeforeRemove?.Invoke(gameObject, t);
            }
        }

        public IEnumerator<KeyValuePair<T, Guid>> GetEnumerator() => _tsInv.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
