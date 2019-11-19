using Lockstep.Math;
using Lockstep.Game;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonoProxyMainScript))]
public class EditorMainScript : Editor {
    private MonoProxyMainScript owner;
    public int rollbackTickCount = 60;

    public override void OnInspectorGUI(){
        base.OnInspectorGUI();
        owner = target as MonoProxyMainScript;
        if (!owner.HasInit ) return;
        var world = ((owner.GetService<ISimulatorService>()) as SimulatorService)?.World;
        EditorGUILayout.LabelField("CurTick " + world.Tick);
        rollbackTickCount = EditorGUILayout.IntField("RollbackTickCount", rollbackTickCount);
        if (GUILayout.Button("Rollback")) {
            ((owner.GetService<ISimulatorService>()) as SimulatorService).__debugRockbackToTick =
                world.Tick - rollbackTickCount;
        }

        if (GUILayout.Button("Resume")) {
            owner.GetService<IGlobalStateService>().IsPause = false;
        }
    }
}