namespace Lockstep.Game {
    
    public partial class GameService :BaseGameService{
        protected IGameStateService _gameStateService;
        protected IGameConfigService _gameConfigService;
        protected IGameResourceService _gameResourceService;
        
        protected override void OnInitReference(IServiceContainer serviceContainer, IManagerContainer mgrContainer){
          
            _gameStateService = serviceContainer.GetService<IGameStateService>();
            _gameConfigService = serviceContainer.GetService<IGameConfigService>();
            _gameResourceService = serviceContainer.GetService<IGameResourceService>();
        }
    }
}