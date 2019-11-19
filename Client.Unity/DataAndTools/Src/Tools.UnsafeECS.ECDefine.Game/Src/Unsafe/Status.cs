// Copyright 2019 谭杰鹏. All Rights Reserved //https://github.com/JiepengTan 

using System;
using System.Collections.Generic;
using Lockstep.UnsafeECSDefine;
using Lockstep.Serialization;

namespace Lockstep.UnsafeECSDefine {
    public class CollisionConfig { }
    public class Msg_G2C_GameStartInfo { }
    public class ConfigPlayerInfo{}
    public partial class GameStateService : IServiceState,IGameStateService {
        // states
        public bool IsPlaying;
        public bool IsGameOver;
        public byte LocalEntityId; 

        // volatile states
        public int CurScore;
    }

    public partial class GameConfigService : IServiceState,IGameConfigService {
        public string RelPath;
        public string RecorderFilePath;
        public string DumpStrPath;

        public int MaxPlayerCount;

        public CollisionConfig CollisionConfig;
        public Msg_G2C_GameStartInfo ClientModeInfo;
        public List<ConfigPlayerInfo> PlayerInfos;
    }
}