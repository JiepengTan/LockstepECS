using Lockstep.Game;
using Lockstep.UnsafeECS;

namespace Lockstep.UnsafeECS.Game {
    public class GameLogicSystems : Systems {
        public GameLogicSystems(Context contexts, IServiceContainer services){
            //Init 
            {
                Add(new InitSystem().Init(contexts, services));
            }
            //Input 
            {
                Add(new InputSystem().Init(contexts, services));
            }
            //Update
            {
                //
                Add(new PlayerMoveSystem().Init(contexts, services));
            }
            //Clean
            {
                //Add(new DestroySystem().Init(contexts, services));
            }
        }
    }
}