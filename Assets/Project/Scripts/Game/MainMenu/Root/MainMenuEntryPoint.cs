using Project.Scripts.Game.Gameplay.Root;
using Project.Scripts.Game.MainMenu.Root.View;
using R3;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Game.MainMenu.Root
{
    public class MainMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private SceneContext sceneContext;

        private MainMenuRegistrations _registrations;
        private MainMenuViewModelsRegistrations _viewModelRegistrations;
        public Observable<MainMenuExitParams> Run(MainMenuEnterParams enterParams)
        {
            var container = sceneContext.Container;
            _registrations = new MainMenuRegistrations(container, enterParams);
            _viewModelRegistrations = new MainMenuViewModelsRegistrations(container);

            Debug.Log($"MAIN MENU ENTER PARAMS: Run main menu scene.");

            var exitSceneSignalSubj = new Subject<Unit>();
            var gameplayEnterParams = new GameplayEnterParams();
            var mainMenuExitParams = new MainMenuExitParams(gameplayEnterParams);
            var exitToGamplaySceneSignal = exitSceneSignalSubj.Select(_ => mainMenuExitParams);
            return exitToGamplaySceneSignal;
        }
    }
}
