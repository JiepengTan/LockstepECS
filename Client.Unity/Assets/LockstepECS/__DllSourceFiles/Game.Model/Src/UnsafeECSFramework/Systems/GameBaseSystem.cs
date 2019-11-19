using Lockstep.UnsafeECS;

namespace Lockstep.UnsafeECS.Game {
    public abstract unsafe partial class GameBaseSystem : BaseSystem {
        protected TempFields _tempFields => _context.TempFields;
    }

    public abstract unsafe partial class GameJobSystem : BaseJobSystem {
        protected TempFields _tempFields => _context.TempFields;
    }
    
    public abstract partial class GameExecuteSystem :BaseExecuteSystem{
        protected TempFields _tempFields => _context.TempFields;
    }
}