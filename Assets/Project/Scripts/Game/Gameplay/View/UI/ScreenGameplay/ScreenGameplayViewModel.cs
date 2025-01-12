using ObservableCollections;
using Project.Scripts.Game.Gameplay.Service.UI;
using Project.Scripts.Game.Gameplay.View.UI.Brick;
using Project.Scripts.MVVM.UI;
using R3;

namespace Project.Scripts.Game.Gameplay.View.UI.ScreenGameplay
{
    public class ScreenGameplayViewModel : WindowViewModel
    {
        private readonly GameplayUIManager _uiManager;
        public readonly IObservableCollection<UIBrickBinder> AllUIBricks;

        public override string Id => "ScreenGameplay";

        public ScreenGameplayViewModel(GameplayUIManager uiManager, UIBricksService uIBricksService)
        {
            AllUIBricks = uIBricksService.AllUIBricks;
            _uiManager = uiManager;
        }

        public void RequestOpenPopupBackMenu()
        {
            _uiManager.OpenPupupBackMenu();
        }

    }
}
