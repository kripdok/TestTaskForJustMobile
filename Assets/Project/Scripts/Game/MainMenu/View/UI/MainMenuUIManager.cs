using Project.Scripts.Game.Common;
using Project.Scripts.Game.MainMenu.View.UI;
using Project.Scripts.Game.MainMenu.View.UI.ScreenMainMenu;
using Project.Scripts.MVVM.UI;
using R3;
using Zenject;

namespace Project.Scripts.Game.MainMenu.View
{
    public class MainMenuUIManager : UIManager
    {
        private readonly Subject<Unit> _extiSceneRequest;
        private readonly DiContainer _diContainer;

        [Inject]
        public MainMenuUIManager(DiContainer container) : base(container)
        {
            _diContainer = container;
            _extiSceneRequest = container.ResolveId<Subject<Unit>>(AppConstants.ENIT_SCENE_REQUEST_TAG);
        }

        public ScreenMainMenuViewModel OpenScreenGameplay()
        {
            var viewModel = new ScreenMainMenuViewModel(this, _extiSceneRequest);
            var rootUI = Container.Resolve<UIMainMenuRootViewModel>();

            rootUI.OpenScreen(viewModel);

            return viewModel;
        }
    }
}
