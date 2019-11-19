using System.Collections.Generic;
using UnityEngine;

namespace Lockstep.Game {
    [System.Serializable]
    public partial class GameViewConfig : UnityEngine.ScriptableObject {
        public const string ResPath = "Config/UnityGameViewConfig";
        public List<RenderInfo> RenderInfos = new List<RenderInfo>();
    }
}