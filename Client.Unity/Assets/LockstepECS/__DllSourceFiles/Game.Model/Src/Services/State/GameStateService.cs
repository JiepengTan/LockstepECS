using Lockstep.Game;
using Lockstep.Math;

namespace Lockstep.Game {
    public partial class GameStateService : BaseService {
        public override void DoStart(){
            base.DoStart();
            IsGameOver = false;
        }

        public void OnEvent_LevelLoadDone(object param){
            var level = (int) param;
            IsGameOver = false;
            _globalStateService.CurLevel = level;
        }

        public void OnEvent_SimulationStart(object param){
            IsPlaying = true;
        }

        private void GameFalied(){
            IsGameOver = true;
            ShowMessage("Game Over!!");
        }

        private void GameWin(){
            IsGameOver = true;
        }

        void ShowMessage(string msg){ }
    }
}