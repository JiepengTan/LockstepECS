using Lockstep.UnsafeECS.Game;
using Unity.Transforms;
using Entity = Lockstep.UnsafeECS.Entity;

namespace Lockstep.Game {
    public unsafe partial class UnityEntityService : BaseUnityEntityService {
       public override void OnPlayerCubeCreated(Context context, PlayerCube* entity){}
       public override void OnPlayerCubeDestroy(Context context, PlayerCube* entity){} 
    }
}