using Lockstep.UnsafeECSDefine;

namespace Lockstep.UnsafeECSDefine {
   
    //Input
    public class InputSystem : IPureSystem {
        public PlayerData PlayerData;
        public MoveData MoveData;
        public Transform3D Transform;
    }
    
    public class PlayerMoveSystem : IPureSystem {
        public Transform3D Transform3D;
        public MoveData MoveData;
    }

}