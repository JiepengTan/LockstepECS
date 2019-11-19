// Copyright 2019 谭杰鹏. All Rights Reserved //https://github.com/JiepengTan 

using System;

namespace Lockstep.UnsafeECSDefine {
    
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
}