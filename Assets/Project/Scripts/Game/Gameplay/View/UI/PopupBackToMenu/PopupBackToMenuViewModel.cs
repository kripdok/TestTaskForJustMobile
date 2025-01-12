using Project.Scripts.MVVM.UI;
using R3;

namespace Project.Scripts.Game.Gameplay.View.UI.PopupBackToMenu
{
    public class PopupBackToMenuViewModel : WindowViewModel
    {
        private readonly Subject<Unit> _exitSceneRequest;

        public override string Id => "BackToMenu";

        public PopupBackToMenuViewModel(Subject<Unit> exitSceneRequest)
        {
            _exitSceneRequest = exitSceneRequest;
        }

        public void RequestBackToMenu()
        {
            _exitSceneRequest.OnNext(Unit.Default);
        }
    }
}
