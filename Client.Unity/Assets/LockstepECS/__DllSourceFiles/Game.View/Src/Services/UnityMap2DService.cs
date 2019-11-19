using System.Collections.Generic;
using Lockstep.Math;
using UnityEngine;

namespace Lockstep.Game {
    [System.Serializable]
    public partial class UnityMap2DService : BaseMap2DService {
        [SerializeField] public Grid grid { get; private set; }
        public List<LVector2> enemyBornPoints { get; private set; }
        public List<LVector2> playerBornPoss { get; private set; }
        public LVector2 campPos { get; private set; }

        public override void DoStart(){
            base.DoStart();
            if (grid == null) {
                grid = GameObject.FindObjectOfType<Grid>();
            }
        }

        void OnEvent_SimulationAwake(object param){
            LoadLevel(1);
        }

        protected override void OnLoadLevel(int level, GridInfo gridInfo){
            UnityMap2DUtil.CheckLoadTileIDMap();
            
            EventHelper.Trigger(EEvent.LevelLoadProgress, 0.5f);
            UnityMap2DUtil.BindMapView(grid, gridInfo);
            EventHelper.Trigger(EEvent.LevelLoadProgress, 1f);
            EventHelper.Trigger(EEvent.LevelLoadDone, level);
        }
    }
}