using System;
using System.Collections.Generic;
using System.Reflection;
using Lockstep.Game;
using UnityEngine;

namespace Lockstep.Game {
    public class GameResourceService : GameService, IGameResourceService {
        public string pathPrefix = "Prefabs/";

        private Dictionary<int, GameObject> _id2Prefab = new Dictionary<int, GameObject>();

        public object LoadPrefab(int id){
            return _LoadPrefab(id);
        }

        GameObject _LoadPrefab(int id){
            if (_id2Prefab.TryGetValue(id, out var val)) {
                return val;
            }

            return null;
        }
    }
}