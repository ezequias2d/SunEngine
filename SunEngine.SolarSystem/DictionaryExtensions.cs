// Copyright (c) 2021 Ezequias Silva.
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Collections.Generic;
using System.Text;

namespace SunEngine.SolarSystem
{
    public static class DictionaryExtensions
    {
        
        public static void Add<T>(this IDictionary<SolarObject, T> dicionary, T planetObject) where T : PlanetObject
        {
            dicionary.Add(planetObject.SolarObject, planetObject);
        }
    }
}
