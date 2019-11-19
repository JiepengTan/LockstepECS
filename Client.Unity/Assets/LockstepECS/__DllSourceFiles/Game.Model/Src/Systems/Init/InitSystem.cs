using System.Collections.Generic;
using Lockstep.Math;
using UnityEngine;
using Debug = Lockstep.Logging.Debug;

namespace Lockstep.UnsafeECS.Game {
    public unsafe class InitSystem : GameBaseSystem, IInitializeSystem {
        public void Initialize(IContext context){
            _context.HasInit = true;
            var config = _gameConfigService;
            int playerId = 0;
            var count = _globalStateService.ActorCount;
            for (int i = 0; i < count; i++) {
                var obstacleInfo = config.PlayerInfos[i % config.PlayerInfos.Count];
                var entity = _context.PostCmdCreatePlayerCube();
                entity->Transform.Position = obstacleInfo.Position;
                entity->Transform.Forward = obstacleInfo.Forward;
                entity->Transform.Scale = obstacleInfo.Scale;
                entity->Move = obstacleInfo.MoveData;
                entity->Prefab.AssetId = obstacleInfo.AssetId;
                entity->Player.LocalId = playerId++; //
                Debug.Log("Create ObstacleInfos " + entity->Player.LocalId);
            }
        }
    }
}