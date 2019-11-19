namespace Lockstep.Game {
    [System.Serializable]
    public partial class UnityMapService : MapService {

        public override void DoStart(){
        }

        public  void OnEvent_SimulationAwake(object param){
            LoadLevel(1);
        }

        protected void LoadLevel(int level){
            EventHelper.Trigger(EEvent.LevelLoadProgress, 0.5f);
            EventHelper.Trigger(EEvent.LevelLoadProgress, 1f);
            EventHelper.Trigger(EEvent.LevelLoadDone, level);
        }
    }
}