using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using Lockstep.Logging;

namespace Lockstep.UnsafeECS.Game {
    [StructLayout(LayoutKind.Sequential, Pack = Define.PackSize)]
    public unsafe partial struct EntityRef {
        internal Int32 _index;
        internal Int32 _version;
        internal Int32 _type;

        public static readonly EntityRef None = default(EntityRef);

        public Boolean Equals(EntityRef other){
            return other._index == this._index && other._version == this._version && other._type == this._type;
        }

        public override Int32 GetHashCode(){
            unchecked {
                var hash = 17;
                hash = hash * 31 + _index;
                hash = hash * 31 + _version;
                hash = hash * 31 + (Int32) _type;
                return hash;
            }
        }

        public override Boolean Equals(Object obj){
            if (obj is EntityRef) {
                return this.Equals((EntityRef) obj);
            }

            return false;
        }

        public override String ToString(){
            return $"[EntityRef Type:{_type} Index:{_index}]";
        }

        public static Boolean operator ==(EntityRef a, EntityRef b){
            return a._type == b._type && a._index == b._index && a._version == b._version;
        }

        public static Boolean operator !=(EntityRef a, EntityRef b){
            return a._type != b._type || a._index != b._index || a._version != b._version;
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = Define.PackSize)]
    public unsafe partial struct Entity {
        internal EntityRef _ref;
        internal Boolean _active;
        public Int32 LocalId;
        public EntityRef EntityRef => _ref;
    }

    [StructLayout(LayoutKind.Sequential, Pack = Define.PackSize)]
    public unsafe partial struct EntityFilter {
        public Entity* Entity;
    }

    public unsafe partial interface IEntityService :Lockstep.Game.IService{
        void OnEntityCreated(Frame f, Entity* entity);
        void OnEntityDestroy(Frame f, Entity* entity);
    }
    public unsafe partial class PureEntityService :IEntityService{
        public virtual void OnEntityCreated(Frame f, Entity* entity){ }
        public virtual void OnEntityDestroy(Frame f, Entity* entity){ }
    }

    //public unsafe struct NativeArray {
    //    
    //}

    public unsafe partial class Frame {
        public __global* _global;
        public __entities _entities = new __entities();
        public Queue<EntityRef> _destroy = new Queue<EntityRef>();
        public IEntityService _entityService;
        public void DoInit(){
            AllocGen();
            InitGen();
        }

        public void PreStepPrepare(){
            _destroy.Clear();
        }

        public void PostStepCleanup(){
            while (_destroy.Count > 0) {
                DestroyEntityInternal(GetEntity(_destroy.Dequeue()));
            }
        }

        void EntityCreate(Entity* entity){
            Debug.Assert(entity->_active == false);
            entity->_ref._version += 1;
            entity->_active = true;
            entity->LocalId = _global->CurLocalId++;
        }

        void EntityDestroy(Entity* entity){
            Debug.Assert(entity->_active);
            entity->_ref._version += 1;
            entity->_active = false;
            entity->LocalId = 0;
        }
    }
}