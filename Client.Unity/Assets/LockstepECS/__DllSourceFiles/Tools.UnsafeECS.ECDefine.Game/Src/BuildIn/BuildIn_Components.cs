// Copyright 2019 谭杰鹏. All Rights Reserved //https://github.com/JiepengTan 

namespace Lockstep.UnsafeECSDefine {

    /// BuildIn Service 
    public interface IBuildInService{}
    /// 需要进行Service 属性展开的类型
    public interface INeedServiceProperty{}
    public interface ICanRaiseEvent{}
    public interface IBuildInComponent : IComponent { }
    
    
    
    public class CollisionShape { }


    public class Animator : IBuildInComponent {
        public int Pad;
    }

    public class CollisionAgent : IBuildInComponent {
        public CollisionShape Collider;
        public bool IsTrigger;
        public int Layer;
        public bool IsEnable;
        [NoExcel] public bool IsSleep;
        public float Mass;
        public float AngularSpeed;
        public Vector3 Speed;
    }

    public class NavMeshAgent : IBuildInComponent { 
        public int Pad;
    }

    public class Prefab : IBuildInComponent { 
        public int AssetId;
    }

    public class Transform2D : IBuildInComponent { 
        public Vector2 Position;
        public float Deg;
        public float Scale;
        
    }

    public class Transform3D : IBuildInComponent {
        public Vector3 Position;
        public Vector3 Forward;
        public float Scale;
    }
}