using Lockstep.Game;
using Lockstep.UnsafeECS.Game;
using Lockstep.UnsafeECS;

namespace Lockstep.UnsafeECS.Game {
    public class UnsafeEcsFactoryService : IECSFactoryService {
        private static Context _lastInstance;
        public object CreateSystems(object contexts, IServiceContainer services){
            return new GameLogicSystems(contexts as Context,services) ;
        }
        
        public object CreateContexts(){
            var  ctx = _lastInstance == null ? Context.Instance : new Context();
            _lastInstance = ctx;
            return ctx;
        }
    }
}