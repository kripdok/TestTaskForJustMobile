using Project.Scripts.Game.Common;
using Project.Scripts.Game.Gameplay.Root.View;
using Project.Scripts.Game.Gameplay.Utils;
using Project.Scripts.Game.Gameplay.View.UI;
using Project.Scripts.Game.GameRoot;
using Project.Scripts.Game.MainMenu.Root;
using R3;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Game.Gameplay.Root
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private SceneContext sceneContext;
        [SerializeField] private UIGameplayRootBinder _sceneUIRootPrefab;
        [SerializeField] private WorldGameplayRootBinder _worldGameplayRoot;
        [SerializeField] private CameraSystem _cameraSystem;

        private GameplayRegistrations _registrations;
        private GameplayViewModelsRegistrations _viewModelRegistrations;

        public Observable<GameplayExitParams> Run(GameplayEnterParams enterParams)
        {
            var container = sceneContext.Container;
            _registrations = new GameplayRegistrations(container, enterParams, _worldGameplayRoot,_cameraSystem);
            _viewModelRegistrations = new GameplayViewModelsRegistrations(container);
            container.Resolve<UIRootView>().SetCamera(_cameraSystem.Camera);

            InitUI(container);
            InitWorld(container);

            var mainMenuEnterParams = new MainMenuEnterParams();
            var exitParams = new GameplayExitParams(mainMenuEnterParams);
            var exitSceneRequest = container.ResolveId<Subject<Unit>>(AppConstants.ENIT_SCENE_REQUEST_TAG);
            var exitToMainMenuSceneSignal = exitSceneRequest.Select(_ => exitParams);
            Debug.Log($"GAMEPLAY ENTER PARAMS: Run gameplay scene.");

            return exitToMainMenuSceneSignal;
        }

        private void InitWorld(DiContainer container)
        {
            _worldGameplayRoot.Bind(container.Resolve<WorldGameplayRootViewModel>());
        }

        private void InitUI(DiContainer container)
        {
            var uiRoot = container.Resolve<UIRootView>();
            var uiSceneRootBinder = Instantiate(_sceneUIRootPrefab);
            uiRoot.AttachSceneUI(uiSceneRootBinder.gameObject);

            var uiSceneRootViewModel = container.Resolve<UIGameplayRootViewModel>();
            uiSceneRootBinder.Bind(uiSceneRootViewModel);

            var uiManager = container.Resolve<GameplayUIManager>();
            uiManager.OpenScreenGameplay();
        }
    }
}
