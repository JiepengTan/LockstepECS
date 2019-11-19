using Lockstep.Game;
using Lockstep.Math;
using UnityEngine;

namespace Lockstep.UnsafeECS.Game {
    public unsafe class BaseEntityView : MonoBehaviour {
        protected const float _LerpVal = 0.5f;
    	public void Update(){ DoUpdate(Time.deltaTime); }
        public virtual void BindEntity(Entity* entityPtr){ }
        public virtual void OnBindEntity(){ }

        public virtual void OnUnbindEntity(){ }
        public virtual void UnbindEntity(){gameObject.DestroyExt();}
        public virtual void RebindEntity(Entity* newEntityPtr){ }
        public virtual void DoUpdate(float deltaTime){}
        protected virtual void OnBind(){ }

        public virtual void OnSkillFire(LFloat range){}
        public virtual void OnSkillDone(LFloat range){}

        protected void UpdatePosRot(ref Transform3D  transform3D){
            var targetPos = transform3D.Position.ToVector3();
            var pos = Vector3.Lerp(transform.position, targetPos, _LerpVal);
            transform.position = pos;
            if (transform3D.Forward != LVector3.zero) {
                var targetRot = Quaternion.LookRotation(transform3D.Forward.ToVector3(), Vector3.up);
                var rot = Quaternion.Lerp(transform.rotation, targetRot, _LerpVal);
                transform.rotation = rot;
            }
            var scale = transform3D.Scale.ToFloat();
            transform.localScale = Vector3.one * scale; 
        }
    }
}