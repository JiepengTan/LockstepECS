using Lockstep.Game;
using Lockstep.UnsafeECS;

namespace Lockstep.UnsafeECS.Game {
    public partial class WorldSystems : Systems {
        public WorldSystems(Context contexts, IServiceContainer services, Systems logicFeature){
            if (logicFeature != null) {
                Add(logicFeature);    
            }
        }
    }
}