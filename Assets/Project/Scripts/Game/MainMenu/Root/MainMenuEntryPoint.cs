using Project.Scripts.Game.Common;
using Project.Scripts.Game.Gameplay.Root;
using Project.Scripts.Game.Gameplay.View.UI;
using Project.Scripts.Game.GameRoot;
using Project.Scripts.Game.MainMenu.Root.View;
using Project.Scripts.Game.MainMenu.View;
using Project.Scripts.Game.MainMenu.View.UI;
using R3;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Game.MainMenu.Root
{
    public class MainMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private SceneContext _sceneContext;
        [SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefab;

        private MainMenuRegistrations _registrations;
        private MainMenuViewModelsRegistrations _viewModelRegistrations;
        public Observable<MainMenuExitParams> Run(MainMenuEnterParams enterParams)
        {
            var container = _sceneContext.Container;

            _registrations = new MainMenuRegistrations(container, enterParams);
            _viewModelRegistrations = new MainMenuViewModelsRegistrations(container);

            InitUI(container);

            var exitSceneSignalSubj = new Subject<Unit>();
            var gameplayEnterParams = new GameplayEnterParams();
            var mainMenuExitParams = new MainMenuExitParams(gameplayEnterParams);
            var exitSceneRequest = container.ResolveId<Subject<Unit>>(AppConstants.ENIT_SCENE_REQUEST_TAG);
            var exitToGamplaySceneSignal = exitSceneRequest.Select(_ => mainMenuExitParams);
            return exitToGamplaySceneSignal;
        }

        private void InitUI(DiContainer container)
        {
            var uiRoot = container.Resolve<UIRootView>();
            var uiSceneRootBinder = Instantiate(_sceneUIRootPrefab);
            uiRoot.AttachSceneUI(uiSceneRootBinder.gameObject);

            var uiSceneRootViewModel = container.Resolve<UIMainMenuRootViewModel>();
            uiSceneRootBinder.Bind(uiSceneRootViewModel);

            var uiManager = container.Resolve<MainMenuUIManager>();
            uiManager.OpenScreenGameplay();
        }
    }
}
