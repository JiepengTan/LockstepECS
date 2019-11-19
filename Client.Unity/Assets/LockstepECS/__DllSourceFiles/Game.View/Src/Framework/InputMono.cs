using Lockstep.Collision2D;
using Lockstep.Game;
using Lockstep.Math;
using UnityEngine;
using Debug = Lockstep.Logging.Debug;

namespace Lockstep.Game {
    public class InputMono : UnityEngine.MonoBehaviour {
        private static bool IsReplay => Launcher.Instance?.IsVideoMode ?? false;
        public ushort skillId;
        public ushort deg;
        public void Update(){
            if (World.Instance != null && !IsReplay) {
                var dir = 0;
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
                    dir = 1;
                }
                else if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.LeftArrow)) {
                    dir = -1;
                }
                skillId = (ushort)(Input.GetKey(KeyCode.Space) ? 1 :0);
                
                GameInputService.CurGameInput = new PlayerInput() {
                    SkillId = skillId,
                    Deg = (ushort)(short)dir
                };
            }
        }
    }
}