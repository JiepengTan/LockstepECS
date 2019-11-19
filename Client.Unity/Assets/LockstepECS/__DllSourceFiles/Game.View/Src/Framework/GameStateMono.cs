using Lockstep.UnsafeECS.Game;
using UnityEngine;

namespace Lockstep.Game {
    [ExecuteInEditMode]
    public class GameStateMono : UnityEngine.MonoBehaviour {
        GUIStyle myStyle = new GUIStyle();
        GUIStyle otherStyle = new GUIStyle();
        private void Start(){
            myStyle.fontSize = 40;
            myStyle.normal.textColor = new Color(46f/256f, 163f/256f, 256f/256f, 256f/256f);
            otherStyle.fontSize = 30;
            otherStyle.normal.textColor = new Color(46f/256f, 12f/256f, 256f/256f, 256f/256f);
        }

        private void OnGUI(){
            if(!Application.isPlaying) return;
            int offset = 50;
            var context = Context.Instance;
            var svc = context.GetService<IGlobalStateService>();
            if(svc == null ) return;
            
            for (int i = 0; i < svc.ActorCount; i++) {
                GUI.Label(new Rect(0, offset, 100, 50), $"Score: {Context.Instance.GetPlayerInfo(i).Player.Score}",svc.LocalActorId == i ? myStyle:otherStyle );
                offset += 50;
            }
            
            GUI.Label(new Rect(0, offset, 100, 50), $"Count: {GameStateService.Instance.CurScore}");
        }
    }
}