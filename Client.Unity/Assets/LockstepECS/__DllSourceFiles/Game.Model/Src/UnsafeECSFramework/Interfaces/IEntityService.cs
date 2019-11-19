using Lockstep.UnsafeECS;
namespace Lockstep.UnsafeECS.Game {
    public unsafe partial interface IEntityService : Lockstep.Game.IService {
        void OnEntityCreated(Context context, Entity* entity);
        void OnEntityDestroy(Context context, Entity* pEntity);
    }
}