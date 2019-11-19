using Lockstep.UnsafeECS.Game;
using Unity.Mathematics;
using UnityEngine;


namespace Lockstep.Game{
    public class CameraMono : MonoBehaviour {
        public Transform targetTrans;
        public Vector3 scaleOffset;
        public Vector3 offset;
        public float fogStartDist = 20;
        public float fogDistScale = 1.5f;

        public float lerpSpd = 1;
        public float xRote = 11;
        private void Update(){
            if (!Context.Instance.HasInit) {
                return;
            }

            if (targetTrans == null) {
                var id =GlobalStateService.Instance.LocalActorId;
                var view = EntityViewPlayerCube.GetView(id);
                targetTrans = view?.transform;
            }
            if(targetTrans == null) return;
            var offsetScale = targetTrans.localScale.x;
            var pos = targetTrans.TransformPoint(offset + offsetScale * scaleOffset);
            transform.position = pos;
            transform.rotation =transform.rotation;
            if (targetTrans != null) {
                if (transform.parent != targetTrans) {
                    transform.SetParent(targetTrans,true);
                }
            }
        }
    }
}