using UnityEngine;

namespace Lockstep.Game {
    [CreateAssetMenu(menuName = "UnityGameViewConfig")]
    [System.Serializable]
    public partial class UnityGameViewConfig : GameViewConfig {
        public int Pad;
    }
}