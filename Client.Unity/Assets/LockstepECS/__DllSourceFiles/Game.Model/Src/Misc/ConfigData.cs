using System.Collections.Generic;
using System.Runtime.InteropServices;
using Lockstep.Math;

namespace Lockstep.UnsafeECS.Game {
   
    [System.Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct ConfigPlayerInfo {
        public LVector3 Position;
        public LVector3 Forward;
        public LFloat Scale;
        public LFloat Deg;
        public int AssetId;
        public MoveData MoveData;
    }
    
}