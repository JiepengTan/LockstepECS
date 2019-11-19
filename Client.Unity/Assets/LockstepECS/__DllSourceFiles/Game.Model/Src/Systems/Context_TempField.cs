using System.Collections.Generic;
using System.Runtime.InteropServices;
using Lockstep.Game;
using Lockstep.Logging;
using Lockstep.Math;
using NetMsg.Common;
using Unity.Collections;

namespace Lockstep.UnsafeECS.Game {
    public unsafe partial class TempFields {
        public void OnDestroy(){
            Clean();
        }
        public void FramePrepare(){}

        public void FrameClearUp(){
            Clean();
        }
        private void Clean(){}
    }
}