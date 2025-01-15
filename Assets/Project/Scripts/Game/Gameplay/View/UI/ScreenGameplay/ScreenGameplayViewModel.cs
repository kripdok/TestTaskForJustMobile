using ObservableCollections;
using Project.Scripts.Game.Gameplay.Inputs;
using Project.Scripts.Game.Gameplay.Service.UI;
using Project.Scripts.Game.Gameplay.View.UI.Brick;
using Project.Scripts.MVVM.UI;
using R3;

namespace Project.Scripts.Game.Gameplay.View.UI.ScreenGameplay
{
    public class ScreenGameplayViewModel : WindowViewModel
    {
        public readonly IObservableCollection<UIBrickBinder> AllUIBricks;

        private readonly GameplayUIManager _uiManager;

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
