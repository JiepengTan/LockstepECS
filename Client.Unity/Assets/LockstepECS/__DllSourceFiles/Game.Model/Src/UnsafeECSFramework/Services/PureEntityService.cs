using Lockstep.UnsafeECS;

namespace Lockstep.UnsafeECS.Game {
    public unsafe partial class PureEntityService : IEntityService {
        public virtual void OnEntityCreated(Context context, Entity* entity){ }
        public virtual void OnEntityDestroy(Context context, Entity* pEntity){ }
    }
}