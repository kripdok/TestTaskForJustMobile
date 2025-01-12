using Project.Scripts.Game.Common;
using Project.Scripts.Game.Gameplay.View.UI.PopupA;
using Project.Scripts.Game.Gameplay.View.UI.PopupB;
using Project.Scripts.Game.Gameplay.View.UI.ScreenGameplay;
using Project.Scripts.MVVM.UI;
using R3;
using Zenject;

namespace Project.Scripts.Game.Gameplay.View.UI
{
    public class GameplayUIManager : UIManager
    {
        private readonly Subject<Unit> _extiSceneRequest;

        [Inject]
        public GameplayUIManager(DiContainer container) : base(container)
        {
            _extiSceneRequest = container.ResolveId<Subject<Unit>>(AppConstants.ENIT_SCENE_REQUEST_TAG);
        }

        public ScreenGameplayViewModel OpenScreenGameplay()
        {
            var viewModel = new ScreenGameplayViewModel(this, _extiSceneRequest);
            var rootUI = Container.Resolve<UIGameplayRootViewModel>();

            rootUI.OpenScreen(viewModel);

            return viewModel;
        }

        public PopupAViewModel OpenPupupA()
        {
            var a = new PopupAViewModel();
            var rootUI = Container.Resolve<UIGameplayRootViewModel>();

            rootUI.OpenPupup(a);
            return a;
        }

        public PopupBViewModel OpenPupupB()
        {
            var B = new PopupBViewModel();
            var rootUI = Container.Resolve<UIGameplayRootViewModel>();

            rootUI.OpenPupup(B);
            return B;
        }
    }
}
