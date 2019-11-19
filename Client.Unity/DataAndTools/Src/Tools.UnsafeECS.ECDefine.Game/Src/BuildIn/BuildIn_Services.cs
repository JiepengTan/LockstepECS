// Copyright 2019 谭杰鹏. All Rights Reserved //https://github.com/JiepengTan 
// Warning!!! you should not modify this file! These files are maintained by author
using System;
using System.Collections.Generic;
using Lockstep.UnsafeECSDefine;

namespace Lockstep.UnsafeECSDefine {


	// Game status
	public partial class GameStateService : IGameService{}
	public partial class GameConfigService : IGameService{}
	public partial class GameEventService : IGameService{}
	
	// BuildIn Services
	public partial class RandomService : IBuildInService{}
	public partial class TimeMachineService : IBuildInService{}
	public partial class GlobalStateService : IBuildInService{}
	public partial class ViewService : IBuildInService{}
	public partial class AudioService : IBuildInService{}
	public partial class InputService : IBuildInService{}
	public partial class Map2DService : IBuildInService{}
	public partial class ResService : IBuildInService{}
	public partial class EffectService : IBuildInService{}
	public partial class EventRegisterService : IBuildInService{}
	public partial class IdService : IBuildInService{}
	public partial class DebugService : IBuildInService{}


	//BuildIn Systems
	public partial class Context:INeedServiceProperty{}
	public partial class GameBaseSystem:INeedServiceProperty{}
	public partial class GameJobSystem:INeedServiceProperty{}
	public partial class GameExecuteSystem:INeedServiceProperty{}

	public partial class GameBaseSystem:ICanRaiseEvent{}
	public partial class GameExecuteSystem:ICanRaiseEvent{}
}