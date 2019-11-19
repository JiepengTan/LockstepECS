 namespace Lockstep.InternalUnsafeECS {
     
 }

 namespace Lockstep.UnsafeECS {
    public static class EntityExt {
        public static Game.EEntityType EnumType(this EntityRef _this){
            return (Game.EEntityType) _this._type;
        }
        public static Game.EEntityType EnumType(this Entity _this){
            return (Game.EEntityType) _this._ref._type;
        }
    }
                                                                                    
}                                                                                               