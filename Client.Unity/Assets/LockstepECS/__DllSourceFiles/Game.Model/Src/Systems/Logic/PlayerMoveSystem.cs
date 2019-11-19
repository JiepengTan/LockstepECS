using Lockstep.Math;
using UnityEngine;
using Debug = Lockstep.Logging.Debug;

namespace Lockstep.UnsafeECS.Game {
    public unsafe partial class PlayerMoveSystem : GameExecuteSystem {
        private LFloat _deltaTime;

        protected override bool BeforeSchedule(){
            //assign jobData info
            _deltaTime = _globalStateService.DeltaTime;
            return true;
        }

        public void Execute(ref Transform3D transform3D,
            ref MoveData moveData){
            var deg = transform3D.Deg + (moveData.DeltaDeg * _deltaTime) * moveData.AngularSpd;
            LFloat s, c;
            var ccwDeg = (-deg + 90);
            LMath.SinCos(out s, out c, LMath.Deg2Rad * ccwDeg);
            var ford = new LVector3(c, 0, s);
            transform3D.Forward = ford;
            var scale = 1 + transform3D.Scale * new LFloat(null, 200);
            transform3D.Position += transform3D.Forward *
                                    (_deltaTime * ((moveData.MoveSpd) * scale));
        }
    }
}