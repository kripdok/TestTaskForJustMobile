using Project.Scripts.Game.Common;
using Project.Scripts.Game.Gameplay.Service.UI;
using Project.Scripts.Game.Gameplay.View.UI.PopupBackToMenu;
using Project.Scripts.Game.Gameplay.View.UI.ScreenGameplay;
using Project.Scripts.MVVM.UI;
using R3;
using Zenject;

namespace Project.Scripts.Game.Gameplay.View.UI
{
    public class GameplayUIManager : UIManager
    {
        private readonly Subject<Unit> _extiSceneRequest;
        private readonly DiContainer _diContainer;

        [Inject]
        public GameplayUIManager(DiContainer container) : base(container)
        {
            _diContainer = container;
            _extiSceneRequest = container.ResolveId<Subject<Unit>>(AppConstants.ENIT_SCENE_REQUEST_TAG);
        }

        public ScreenGameplayViewModel OpenScreenGameplay()
        {
            var viewModel = new ScreenGameplayViewModel(this, _diContainer.Resolve<UIBricksService>());
            var rootUI = Container.Resolve<UIGameplayRootViewModel>();

            rootUI.OpenScreen(viewModel);

            return viewModel;
        }

        public PopupBackToMenuViewModel OpenPupupBackMenu()
        {
            var popup = new PopupBackToMenuViewModel(_extiSceneRequest);
            var rootUI = Container.Resolve<UIGameplayRootViewModel>();

            rootUI.OpenPupup(popup);
            return popup;
        }
    }
}
