// Copyright 2019 谭杰鹏. All Rights Reserved //https://github.com/JiepengTan 

using Lockstep.UnsafeECSDefine;
using Lockstep.Math;

namespace Lockstep.UnsafeECSDefine {
    
    public partial class PlayerCubeTag : IGameComponent {
        public int Pad;
    }

    public partial class AssetData : IGameComponent {
        public int AssetId;
    }
    
    public partial class PlayerData : IGameComponent {
        public int Score;
        public int LocalId;
    }
    public partial class MoveData : IGameComponent {
        public float MoveSpd;
        public float AcceleratedSpd;
        public float CurSpd;
        public float AngularSpd;
        public float DeltaDeg;
    }
}