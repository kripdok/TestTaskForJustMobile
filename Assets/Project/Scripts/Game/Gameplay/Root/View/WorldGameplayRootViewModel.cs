using ObservableCollections;
using Project.Scripts.Game.Gameplay.ObjectPools;
using Project.Scripts.Game.Gameplay.Service;
using Project.Scripts.Game.Gameplay.View.Bricks;
using Zenject;

namespace Project.Scripts.Game.Gameplay.Root.View
{
    public class WorldGameplayRootViewModel
    {
        public readonly IObservableCollection<BrickViewModel> AllBuildongs;
        public readonly BrickBinderObjectPool ObjectPool;

        [Inject]
        public WorldGameplayRootViewModel(BrickService brickService, BrickBinderObjectPool objectPool)
        {
            AllBuildongs = brickService.AllBricks;
            ObjectPool = objectPool;
        }
    }
}
