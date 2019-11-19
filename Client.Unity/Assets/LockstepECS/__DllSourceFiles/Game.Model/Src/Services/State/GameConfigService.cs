using System;
using System.Collections.Generic;
using System.IO;
using Lockstep.ECS;
using Lockstep.Math;
using Lockstep.Serialization;
using Lockstep.Util;
using NetMsg.Common;
using UnityEngine;
using Lockstep.Game;

namespace Lockstep.Game {                                                                      
	[System.Serializable]  
    public partial class GameConfigService : BaseService {
        [NonSerialized] private GameConfig _curState;
        public LFloat DeltaTime => new LFloat(null,NetworkDefine.UPDATE_DELTATIME);
        public override void DoAwake(IServiceContainer services){
            var text = UnityEngine.Resources.Load<TextAsset>("Config/GameConfig").text;
            _curState = JsonUtil.ToObject<GameConfig>(text);
            _globalStateService.GameStartInfo = _curState.ClientModeInfo;
        }
    }
}