using Project.Scripts.MVVM.UI;
using R3;

namespace Project.Scripts.Game.Gameplay.View.UI.ScreenGameplay
{
    public class ScreenGameplayViewModel : WindowViewModel
    {
        private readonly GameplayUIManager _uiManager;
        private readonly Subject<Unit> _exitSceneRequest;

        public override string Id => "ScreenGameplay";

        public ScreenGameplayViewModel(GameplayUIManager uiManager, Subject<Unit> exitSceneRequest)
        {
            _uiManager = uiManager;
            _exitSceneRequest = exitSceneRequest;
        }

        public void RequestOpenPopupA()
        {
            _uiManager.OpenPupupA();
        }

        public void RequestOpenPopupB()
        {
            _uiManager.OpenPupupB();
        }

        public void RequestGoToMainMenu()
        {
            _exitSceneRequest.OnNext(Unit.Default);
        }
    }
}
