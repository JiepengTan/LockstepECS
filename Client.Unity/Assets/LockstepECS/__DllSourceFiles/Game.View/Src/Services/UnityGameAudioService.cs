using UnityEngine;

namespace Lockstep.Game {

    
    [System.Serializable]
    public class UnityGameAudioService : UnityGameService,IGameAudioService {
        private UnityAudioService _unityAudioSvc;
        private static string _audioConfigPath = "AudioConfig";
        private AudioConfig _config = new AudioConfig();
        
        void OnEvent_OnAllPlayerFinishedLoad(object param){
            PlayMusicStart();
        }
        
        public void PlayMusicBG(){ _audioService.PlayClip(_config.BgMusic); }
        public void PlayMusicStart(){ _audioService.PlayClip(_config.StartMusic); }
        
    }
}