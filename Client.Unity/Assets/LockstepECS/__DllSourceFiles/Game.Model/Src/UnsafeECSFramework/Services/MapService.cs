namespace Lockstep.Game {
    public interface IMapService : IService { }

    [System.Serializable]
    public partial class MapService : BaseService, IMapService { }
}