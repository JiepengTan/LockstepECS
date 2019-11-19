using Lockstep.Game;
using Lockstep.UnsafeECS.Game;


public class UnityServiceContainer : BaseGameServicesContainer {
    public UnityServiceContainer() : base(){
        
        //basic service 
        RegisterService(new RandomService());
        RegisterService(new IdService());
        RegisterService(new GlobalStateService());
        RegisterService(new SimulatorService());
        RegisterService(new NetworkService());
        //RegisterService(new ResService());
        RegisterService(new UnsafeEcsFactoryService());
        //Code Gen service
        RegisterService(new GameStateService());
        RegisterService(new GameConfigService());
        
        // game services 
        RegisterService(new GameResourceService());
        RegisterService(new GameInputService());
        
        // unity service 
        RegisterService(new UnityEntityService());
        RegisterService(new UnityEffectService());
        RegisterService(new UnityAudioService());
        RegisterService(new UnityGameEffectService());
        RegisterService(new UnityGameAudioService());
        RegisterService(new UnityMapService());
    }
}