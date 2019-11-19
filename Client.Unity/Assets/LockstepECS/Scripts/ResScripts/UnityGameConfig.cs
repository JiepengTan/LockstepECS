using System.Collections.Generic;
using System.IO;
using Lockstep.UnsafeECS.Game;
using Lockstep.Util;
using UnityEngine;

namespace Lockstep.Game {
    [CreateAssetMenu(menuName = "GameConfig")]
    [System.Serializable]
    public partial class UnityGameConfig : UnityEngine.ScriptableObject {
        public GameConfig pureConfig = new GameConfig();

        public static void SaveToJson(GameConfig config){
            var json = JsonUtil.ToJson(config);
            File.WriteAllText(Application.dataPath + "/LockstepECS/Resources/Config/GameConfig.json", json);
        }
    }
}