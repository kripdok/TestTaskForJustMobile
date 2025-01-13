using ObservableCollections;
using Project.Scripts.Game.Gameplay.Service.UI;
using Project.Scripts.Game.Gameplay.View.Bricks;
using Zenject;

namespace Project.Scripts.Game.Gameplay.Root.View
{
    public class WorldGameplayRootViewModel
    {
        public readonly IObservableCollection<BrickViewModel> AllBuildongs;

        [Inject]
        public WorldGameplayRootViewModel(BrickService brickService)
        {
            AllBuildongs = brickService.AllBricks;
        }
    }
}
