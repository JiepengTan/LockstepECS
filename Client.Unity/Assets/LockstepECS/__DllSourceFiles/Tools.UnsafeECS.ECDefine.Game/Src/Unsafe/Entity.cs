// Copyright 2019 谭杰鹏. All Rights Reserved //https://github.com/JiepengTan 

using System.Collections.Generic;
using Lockstep.UnsafeECSDefine;

namespace Lockstep.UnsafeECSDefine {
    

    [EntityCount(2)]
    public partial class PlayerCube :IEntity,IBindViewEntity,IUpdateViewEntity{
        public Transform3D Transform;
        public Prefab Prefab;
        public MoveData Move;
        public PlayerData Player;
        public PlayerCubeTag Tag;
    }
}