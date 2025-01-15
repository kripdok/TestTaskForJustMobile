using Project.Scripts.MVVM.UI;
using R3;

namespace Project.Scripts.Game.MainMenu.View.UI.ScreenMainMenu
{
    public class ScreenMainMenuViewModel : WindowViewModel
    {
        private readonly Subject<Unit> _exitSceneRequest;
        private readonly MainMenuUIManager _uiManager;

        public override string Id => "ScreenMainMenu";

        public ScreenMainMenuViewModel(MainMenuUIManager uiManager, Subject<Unit> exitSceneRequest)
        {
            _exitSceneRequest  = exitSceneRequest;
            _uiManager = uiManager;
        }

        public void RequestBackToGameplay()
        {
            _exitSceneRequest.OnNext(Unit.Default);
        }
    }
}
