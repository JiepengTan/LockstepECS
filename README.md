# LockstepECS 
## **简介**
LockstepECS  一个基于c# 指针和结构体 的帧同步框架，使用于超大型场景的帧同步游戏
优点：
- 运行速度快，使用指针,和结构体，基本无gc , PureMode 都比Entitas 快两倍，Burst Mode，快四倍以上
- 内存紧凑，预测回滚是否帧状态拷贝快 7000 只鱼的状态拷贝只消耗0.3ms
- API 和 UNITY ECS 非常相似，可以使用同一种编程范式来编写 logic 层 和 view 层
- 无缝兼容UnityECS ，使用条件宏可以切换两种模式，
    - PureMode:纯代码形式，可以直接在服务器中运行逻辑，不依赖Unity 
    - Burst Mode: 模式，直接生成适配Unity ECS Burst+job框架代码的代码，进一步提升运行速度

- github 上的是免费版

## **Reference**

- [帧同步基础库 https://github.com/JiepengTan/LockstepEngine][1]
- [高性能帧同步ECS框架 https://github.com/JiepengTan/LockstepECS][8]
- [代码生成DSL https://github.com/JiepengTan/ME][2]
- [demo: 帧同步版联机版 模拟鲨鱼围捕 2000 条小鱼 https://github.com/JiepengTan/LcokstepECS_Demo_Boid][3]
- [帧同步教程 https://github.com/JiepengTan/Lockstep-Tutorial][4]
- **LockstepECS 建议或bug 群**
928084598，功能仅限提bug和建议

- **Unity DOTS 技术交流 微信群**

<p align="center"> <img src="https://github.com/JiepengTan/JiepengTan.github.io/blob/master/assets/img/blog/MyVX.png?raw=true" width="256"/></p>

```
为了过滤伸手党，不浪费群里人时间，有ECS使用经验后你再加（能独立用ECS写个小游戏）
添加微信请备注 LECS: + 你的github网址
eg:我个人GitHub 地址为 https://github.com/JiepengTan
备注应该填写为：LECS:JiepengTan

```

## **视频链接**
- [环境搭建（Win & Mac）][9]
- [Boid Demo 运行 ][10]

## **TODO**
- 预测回滚 
- 碰撞检测库
- 寻路库
- 序列化库向前兼容

## **1.安装运行**
### **1.ClientMode**
 - 1.打开场景
![Screen Shot 2019-11-13 at 4.48.20 PM.png](https://upload-images.jianshu.io/upload_images/11593954-fb2066c0652e5cd0.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

- 2. 确保钩上了 ClientMode  选项
![Screen Shot 2019-11-13 at 4.49.50 PM.png](https://upload-images.jianshu.io/upload_images/11593954-5dafa8d3640934b2.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

- 3. 运行 游戏
  A  D  控制方向
  Space  释放技能 (Boid demo 才有技能)

### **2.联网模式**
- 1.打开Game.sln 
![Screen Shot 2019-11-13 at 4.59.36 PM.png](https://upload-images.jianshu.io/upload_images/11593954-b4990f7daa51223e.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

- 2.编译并运行 Game.sln
![[图片上传中...(Screen Shot 2019-11-13 at 4.50.45 PM.png-75d531-1573782602414-0)]
](https://upload-images.jianshu.io/upload_images/11593954-33601348642f30af.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)


- 3.确保关闭了单机模式  ClientMode
![Screen Shot 2019-11-13 at 4.50.45 PM.png](https://upload-images.jianshu.io/upload_images/11593954-0dbb32d16f646c97.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

- 4.Build 
![Screen Shot 2019-11-13 at 5.03.04 PM.png](https://upload-images.jianshu.io/upload_images/11593954-c97d439635944ffc.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

- 5.Run Client
![Screen Shot 2019-11-13 at 5.07.33 PM.png](https://upload-images.jianshu.io/upload_images/11593954-7f73394ad639eadf.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

- 6. 现在是简单帧同步模式 

![Screen Shot 2019-11-13 at 5.09.51 PM.png](https://upload-images.jianshu.io/upload_images/11593954-dbc45db61ba001d9.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)


（Build 版本代码中为了方便测试，做了限制自动绕圈圈，你可以修改他）
![Screen Shot 2019-11-15 at 10.04.02 AM.png](https://upload-images.jianshu.io/upload_images/11593954-30209b83a9618d66.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

## **2. 开发**
### 1. 目录安排
![Screen Shot 2019-11-15 at 10.14.28 AM.png](https://upload-images.jianshu.io/upload_images/11593954-512d44c85f835292.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)
- 1. Game.Model
   这里放置的是MVC 中Model and Control 代码（Logic 层）
- 2. Game.View
   这里放置的是MVC 中View 代码（View 层），依赖于Model  层
- 3. Tools.UnsafeECS.ECSDefine.Game 
   这里放置的是你对游戏中 Entity  Component  System 以及全局状态 State 的定义,必须保证该子dll  是能编译的（不能依赖于Model 或 View 层）

### 2. 环境要求
- 1.需要安装NetCore2.2以上
- 2.需要安装Mono
- 3.GitBash


### 3. Tools.UnsafeECS.ECSDefine 定义详解
#### 0. 代码生成

1. Windows 用户
    - 使用gitBash 执行 Client.Unity/DataAndTools/Tools/UpdateAndCodeGen_Win.sh
    
2. Mac 用户
    - 使用命令行执行 Client.Unity/DataAndTools/Tools/UpdateAndCodeGen.sh
    - 或直接双击运行 Client.Unity/DataAndTools/Tools/UpdateAndCodeGen  (注意无后缀)

#### 1. 接口定义
```cpp
   // Copyright 2019 谭杰鹏. All Rights Reserved //https://github.com/JiepengTan 
    /// should declare a Method like:
    /// public void Execute(.....)
    public interface ISystem { }
    /// should declare a Method like:
    /// public void Execute(int index, .....)
    public interface ISystemWithIdx { }
    /// should declare a Method like:
    /// public void Execute(Entity* ptr, .....)
    public interface ISystemWithEntity { }

    /// this system would always be scheduled in main thread 
    /// should declare a Method like:
    /// public void Execute(.....)
    public interface IPureSystem : ISystem { }

    /// this system would always be scheduled in main thread 
    /// should declare a Method like:
    /// public void Execute(Entity* ptr, .....)
    public interface IPureSystemWithEntity : IPureSystem, ISystemWithEntity { }
    
    /// If macro UNING_UNITY_BURST_JOB was defined
    /// this system will be complied by Unity's Burst compiler and scheduled by Unity's JobSystem,Otherwise it will be scheduled call in MainThread
    /// should declare a Method like:
    ///  public void Execute(.....)
    public interface IJobSystem : ISystem { }

    /// If macro UNING_UNITY_BURST_JOB was defined
    /// this system will be complied by Unity's Burst compiler and scheduled by Unity's JobSystem,Otherwise it will be scheduled call in MainThread
    /// should declare a Method like:
    /// public void Execute(Entity* ptr, .....)
    public interface IJobSystemWithEntity : IJobSystem, ISystemWithEntity { }

    /// If macro UNING_UNITY_BURST_JOB was defined
    /// this system will be complied by Unity's Burst compiler and scheduled by Unity's JobSystem,Otherwise it will be scheduled call in MainThread
    /// should declare a Method like:
    /// public void Execute(int index, .....)
    public interface IJobForEachSystem : IJobSystem { }
    /// If macro UNING_UNITY_BURST_JOB was defined
    /// this system will be complied by Unity's Burst compiler and scheduled by Unity's JobSystem,Otherwise it will be scheduled call in MainThread
    /// should declare a Method like:
    /// public void Execute(Entity* ptr,int index, .....)
    public interface IJobForEachSystemWithEntity : IJobForEachSystem, ISystemWithEntity { }

    /// If macro UNING_UNITY_BURST_JOB was defined
    /// this system will be complied by Unity's Burst compiler and scheduled by Unity's JobSystem,Otherwise it will be scheduled call in MainThread
    /// should declare as if it was inherited from : Unity.Collections.IJobNativeMultiHashMapMergedSharedKeyIndices
    public interface IJobHashMapSystem : ISystem { }

    
    /// Component in game
    public interface IGameComponent : IComponent { }
    /// Service using in game
    public interface IGameService{}
    
    /// Game status which need read from config
    public interface IGameConfigService{}
    /// Game status which can not be modified in game
    public interface IGameConstStateService{}
    /// Game status can changed during game 
    public interface IGameStateService{}
    
    
    /// Component
    public interface IComponent { }
    /// Entity
    public interface IEntity { }
    /// Game status
    public interface IServiceState { }
    /// Game Events
    public interface IEvent{}
    /// CollisionEvent define (TODO)
    public interface ICollisionEvent { }

    /// Create a game object to bind Entity
    /// to view the Entity's status or Attach some effect to the gameObject
    /// reference to :CodeGen_EntityView.cs
    public interface IBindViewEntity{}
    /// synchronize Unsafe Entity's Position and Rotation to Unity Entity
    /// reference to :CodeGen_UpdateViewStateSystem.cs
    public interface IUpdateViewEntity{}
````

#### 2. 属性集合 
- 1. EntityCountAttribute
   定义默认初始化的保留的Entity 数量，用于优化  类似List<T> 中的 capacity 中的概念
``` 
    [EntityCount(1000)]
    public partial class BoidCell : IEntity{
        public CellData Cell;
    }
``` 

- 1. NoGenCodeAttribute
提示代码生成器在代码生成的过程中忽略掉本类型
``` 
    [NoGenCode] public class ConfigTargetInfo{}
``` 

#### 3. Componet 定义
1. Component 中可以使用的类型
```   
   bool
   float  //注意不支持double 
   byte
   sbyte
   short
   ushort
   int
   uint
   long
   ulong
   Vector2 
   Vector3 
   Quaternion 
   Vector2Int 
   Vctor3Int
   EntityRef
   Entity 
```

2. 注意事项，应该使用Public 定义属性，同时需要最终继承自IGameComponent
```
    public partial class BoidState : IGameComponent {
        public float SinkTimer;
        public bool IsDied;
        public bool IsScored;
        public int KillerIdx;
    }
```
3. 可以使用继承
-  Component define 
```
     public partial class TestCompBase1 : IGameComponent {
        public int Count;
    }
    public partial class TestCompChild2 : TestCompBase1 {
        public float Radius;
    }
```
- Generated codes
```
   [StructLayoutAttribute(LayoutKind.Sequential)]
    [System.Serializable]
    public unsafe partial struct TestCompBase1 {
        public int Count; 
        public override Int32 GetHashCode() {
            unchecked {
                var hash = 7;
                hash = hash * 37 +Count.GetHashCode();  
                return hash;
            }
        }
    }
    [StructLayoutAttribute(LayoutKind.Sequential)]
    [System.Serializable]
    public unsafe partial struct TestCompChild2 {
        public LFloat Radius;
        public int Count; 
        public override Int32 GetHashCode() {
            unchecked {
                var hash = 7;
                hash = hash * 37 +Radius.GetHashCode();
                hash = hash * 37 +Count.GetHashCode();  
                return hash;
            }
        }
    }
```
#### 4. Entity 定义
1. 注意事项，应该使用Public 定义属性，同时需要最终继承自IEntity
2. 可以使用继承
- Define
```
    [EntityCount(20)]
    public partial class TestEntity1 : IEntity{
        public CellData Cell;
    }

    [EntityCount(100)]
    public partial class TestEntity2 : TestEntity1{
        public SpawnData Spawn;
    }
```
- Generated Codes
```
    [StructLayoutAttribute(LayoutKind.Sequential)]
    [System.Serializable]
    public unsafe partial struct TestEntity1 :IEntity {
        public const Int32 INIT_COUNT = 20;
        internal Entity _entity;

        // Fields
        public CellData Cell; 
        // BuildIn properties
        public EntityRef EntityRef =>_entity._ref;
        public int EntityIndex =>_entity._ref._index;
        public EEntityType EntityType => (EEntityType)(_entity._ref._type);
        public bool IsActive =>_entity._active;
    }
    [StructLayoutAttribute(LayoutKind.Sequential)]
    [System.Serializable]
    public unsafe partial struct TestEntity2 :IEntity {
        public const Int32 INIT_COUNT = 100;
        internal Entity _entity;

        // Fields
        public SpawnData Spawn;
        public CellData Cell; 
        // BuildIn properties
        public EntityRef EntityRef =>_entity._ref;
        public int EntityIndex =>_entity._ref._index;
        public EEntityType EntityType => (EEntityType)(_entity._ref._type);
        public bool IsActive =>_entity._active;
    }
```
3. IUpdateViewEntity
如果需要将UnsafeECS 中的Entity 的位置于旋转属性同步到Unity 中的Entity ，需要继承自IUpdateViewEntity接口，参考代码 CodeGen_UpdateViewStateSystem.cs

4. IBindViewEntity
如果希望能够以GameObject 的形式 绑定相关的Entity,这样方便挂接特效等一些View 层的脚本，可以继承自IBindViewEntity 接口，参考代码 CodeGen_EntityView.cs
![Screen Shot 2019-11-15 at 1.01.13 PM.png](https://upload-images.jianshu.io/upload_images/11593954-d604b2d2a84b8c03.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

#### 5. System
1. **Overview**

- System Define 
    - IJobSystem
    - IPureSystem
- System Implement
    - GameJobSystem
    - GameExecuteSystem

- **其中以Job开头的定义，在UNING_UNITY_BURST_JOB  定义了的情况下，会使用Unity 的Burst 进行编译，且会交由Unity 的JobSystem 进行调度，否则会在主线程中调度，所以这类System,不能修改全局的状态，只能访问相关的属性**
- **而 IPureSystem 一定会被主线程调用，适合处理涉及全局状态的改变的任务**

```
 public abstract unsafe class BaseExecuteSystem : BaseSystem, IExecuteSystem {
        public void Execute(IContext context){
            if (BeforeSchedule()) {  DoSchedule(context);  }
            AfterSchedule(isSucc);
        }
        protected virtual bool BeforeSchedule(){ return true;}
        protected virtual void DoSchedule(IContext context){  context.Schedule(this);  }
        protected virtual void AfterSchedule(bool isSucc){ }
    }
 public partial class GameExecuteSystem :BaseExecuteSystem{   }
```
- 你可以Override BeforeSchedule  在正式调度前去设置一下状态，方便在Execute 中访问
- 你可以Override AfterSchedule  在调度完成后去清理一下状态

2. **PureSystem**
这类System 继承自IPureSystem 如：
- IPureSystem
- IPureSystemWithEntity

这类System 中的Execute 方法必定是在主线程中调用，一旦涉及到全局状态的修改，就必须使用这种类型的System , 比如 Entity 的创建以及销毁，全局状态的设置，如demo中的SpawnSystem
- System Define 
```
    public class SpawnSystem : IPureSystemWithEntity {
        public SpawnData SpawnData;
        public AssetData AssetData;
    }
```
- System Implement
``` 
 public unsafe partial class SpawnSystem : GameExecuteSystem {
        public void Execute(Entity* entity, ref SpawnData spawnData, ref AssetData assetData){
            var count = spawnData.Count;
            var center = spawnData.Position;
            var radius = spawnData.Radius;
            var spawnPositions = new NativeArray<LVector3>(count, Allocator.Temp,
                NativeArrayOptions.UninitializedMemory);
            GeneratePoints.RandomPointsInUnitCube(spawnPositions);
            var pointPtr = spawnPositions.GetPointer(0);
            var context = _context;
            for (int i = 0; i < count; ++i, ++pointPtr) {
                var boidPtr = context.PostCmdCreateBoid();
                boidPtr->Transform.Position = center + (*pointPtr * radius);
                boidPtr->Transform.Forward = *pointPtr;
                boidPtr->Transform.Scale = 1;
                boidPtr->Prefab.AssetId = assetData.AssetId;
            }

            spawnPositions.Dispose();
            context.PostCmdDestroyEntity(entity);
        }
    }
```

3. **JobSystem**
这类System 继承自IJobSystem 如：
- IJobSystem
- IJobSystemWithEntity
- IJobForEachSystem
- IJobForEachSystemWithEntity
- IJobHashMapSystem
If macro UNING_UNITY_BURST_JOB was defined
    /// this system will be complied by Unity's Burst compiler and scheduled by Unity's JobSystem,Otherwise it will be scheduled call in MainThread
  
- System Define
```    
    public class SinkSystem : IJobSystem {
        public Transform3D Transform;
        public BoidState BoidState;
    }
```
- System Implement
```
 public unsafe partial class SinkSystem : GameJobSystem {
        public unsafe partial struct JobDefine {
            [ReadOnly] public LVector3 SinkOffset;
            [ReadOnly] public LFloat DeltaTime;

            public void Execute(ref Transform3D transform3D, ref BoidState boidState){
                if (!boidState.IsDied) return;
                boidState.SinkTimer -= DeltaTime;
                transform3D.Position += SinkOffset;
            }
        }

        protected override bool BeforeSchedule(){
            //assign jobData info
            JobData.DeltaTime = _globalStateService.DeltaTime;
            JobData.SinkOffset = new LVector3(0,_gameConfigService.BoidSettting.SinkSpd * JobData.DeltaTime,0) ;
            return true;
        }
    }
```
#### 6. Status & Config
- 你需要在 GameStateService 定义游戏中会使用到的可以变化的变量
框架会帮你自动进行状态的备份以及还原
```
    public partial class GameStateService : IServiceState,IGameStateService {
        // states
        public bool IsPlaying;
        public bool IsGameOver;
        public byte LocalEntityId; 

        // volatile states
        public int CurEnemyCount;
        public int CurScore;
        public float CurScale;
        
    }
```

- 如果你的变量是配置，在游戏加载后就不再变动，可以放置在GameConfigService 中，框架会自动帮你生成相关的ScriptObject  方便进行配置（其中使用到的类型不需要在 ECDefine  中定义，只需要声明有这样的一个类型，并且可以编译通过即可，可以参考 Tools.UnsafeECS.ECDefine.Game/Src/Unsafe/Status.cs）
```
    public partial class GameConfigService : IServiceState,IGameConfigService {
        public string RelPath;
        public string RecorderFilePath;
        public string DumpStrPath;

        public int InitScale;
        public int MaxPlayerCount;

        public CollisionConfig CollisionConfig;
        public Msg_G2C_GameStartInfo ClientModeInfo;
        public List<ConfigTargetInfo> TargetInfos;
        public List<ConfigSpawnInfo> SpawnInfos;
        public List<ConfigObstacleInfo> ObstacleInfos;
        public ConfigBoidSharedData BoidSettting;
    }
```
#### 7. Events

```
    public class OnSkillFire: IEvent{
        public SkillData SkillData;
    }
    public class OnSkillDone: IEvent{
        public SkillData SkillData;
    }
```
框架会自动生成相关的Service  
自需要在相关的Service 中实现相应的事件的响应，或者二次派发亦可，
 
```
    public unsafe partial interface IGameEventService : IService{
        void OnSkillFire(Entity* ptr
                ,ref SkillData SkillData             
            );
        void OnSkillDone(Entity* ptr
                ,ref SkillData SkillData             
            );    
    } 
```
可以在相应的GameExecuteSystem 中调用 RaiseEventXxxx 事件
如：
```
    public unsafe partial class SkillSystem : GameExecuteSystem {
        private LFloat _deltaTime;
        public void Execute(Entity* ptr,ref SkillData skillData){
            //skillData.IsFiring = skillMonoData.IsFiring;
            if (skillData.IsNeedFire) {
                if (skillData.CdTimer <= 0 && !skillData.IsFiring) {
                    skillData.CdTimer = skillData.CD;
                    skillData.IsFiring = true;
                    skillData.DurationTimer = skillData.Duration;
                    RaiseEventOnSkillFire(ptr,ref skillData);
                }
            }

            skillData.CdTimer -= _deltaTime;
            if (skillData.IsFiring) {
                skillData.DurationTimer -= _deltaTime;
                if (skillData.DurationTimer <= 0) {
                    skillData.IsFiring = false;
                    RaiseEventOnSkillDone(ptr,ref skillData);
                }
            }

            skillData.IsNeedFire = false;
        }
      ...
```

By convention, You should not call any method which start with **_**，eg: _DoDestroyEntity

 [1]: https://github.com/JiepengTan/LockstepEngine
 [2]: https://github.com/JiepengTan/ME
 [3]: https://github.com/JiepengTan/LcokstepECS_Demo_Boid
 [4]: https://github.com/JiepengTan/Lockstep-Tutorial
 [5]: https://github.com/JiepengTan/FishManShaderTutorial
 [6]: https://www.bilibili.com/video/av67829097
 [7]: https://www.bilibili.com/video/av68850334
 [8]: https://github.com/JiepengTan/LockstepECS
 [9]: https://www.bilibili.com/video/av76298196
 [10]: https://www.bilibili.com/video/av76311418

