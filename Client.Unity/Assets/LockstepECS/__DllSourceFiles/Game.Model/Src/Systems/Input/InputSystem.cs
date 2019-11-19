using Lockstep.Collision2D;
using Lockstep.Game;
using Lockstep.Math;
using Lockstep.Serialization;

namespace Lockstep.UnsafeECS.Game {
    public unsafe partial class InputSystem : GameExecuteSystem {
        public void Execute(
            ref PlayerData playerData,
            ref MoveData moveData,
            ref Transform3D transform3D
            ){
            if (_tempFields.InputCmds.TryGetValue(playerData.LocalId, out var cmd)) {
                var input = new Deserializer(cmd.content).Parse<PlayerInput>();
                moveData.DeltaDeg = (int)(short)input.Deg;
            }
            else {
                moveData.DeltaDeg = 0;
            }
        }

        private LFloat _deltaTime;
        protected override bool BeforeSchedule(){
            _deltaTime = _globalStateService.DeltaTime;
            return true;
        }
        protected override void AfterSchedule(bool isSucc){
            _tempFields.InputCmds.Clear();
        }
    }

}