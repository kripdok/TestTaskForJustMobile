using Project.Scripts.Game.Gameplay.Root;
using Project.Scripts.Game.MainMenu.Root;
using Project.Scripts.Game.Settings;
using Project.Scripts.Game.State;
using Project.Scripts.Utils;
using R3;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using Zenject;

namespace Project.Scripts.Game.GameRoot
{
    public class GameEntryPoint
    {
        private Coroutines _coroutines;
        private UIRootView _uiRoot;

        private static GameEntryPoint _instance;

        private readonly DiContainer _rootContainer;


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Input.multiTouchEnabled = false;

            _instance = new GameEntryPoint();
        }

        private GameEntryPoint()
        {
            _rootContainer = ProjectContext.Instance.Container;

            CreateCoroutines();
            CreateUIRootView();
            BindSystems();
            RunGame();
        }

        private void CreateCoroutines()
        {
            _coroutines = new GameObject(name: "[COROUTINES]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(_coroutines.gameObject);
        }

        private void CreateUIRootView()
        {
            var prefabUIRoot = Resources.Load<UIRootView>("UIRoot");
            _uiRoot = Object.Instantiate(prefabUIRoot, null);
            Object.DontDestroyOnLoad(_uiRoot.gameObject);
        }

        private void BindSystems()
        {

            _rootContainer.Bind<Coroutines>().FromInstance(_coroutines).AsSingle();
            _rootContainer.Bind<ISettingsProvider>().To<LocalSettingsProvider>().AsSingle().NonLazy();
            _rootContainer.Bind<IGameStateProvider>().To<PlayerPrefsGameStateProvider>().AsSingle().NonLazy();
            _rootContainer.Bind<UIRootView>().FromInstance(_uiRoot).AsSingle();
        }


        private async void RunGame()
        {
            await LocalizationSettings.InitializationOperation.Task;
            await _rootContainer.Resolve<ISettingsProvider>().LoadGameSettings();

#if UNITY_EDITOR

            var sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == Scenes.GAMEPLAY)
            {
                var gameplayEnterParams = new GameplayEnterParams();
                _coroutines.StartCoroutine(LoadAndStartGameplay(gameplayEnterParams));
                return;
            }

            if (sceneName == Scenes.MAIN_MENU)
            {
                _coroutines.StartCoroutine(LoadAndStarMainMenu());
                return;
            }

            if (sceneName != Scenes.BOOT)
            {
                return;
            }
#endif
            _coroutines.StartCoroutine(LoadAndStarMainMenu());
        }


        private IEnumerator LoadAndStartGameplay(GameplayEnterParams enterParams)
        {
            _uiRoot.ShowLoadingScreen();

            yield return LoadScene(Scenes.BOOT);
            yield return new WaitForSeconds(1);
            yield return LoadScene(Scenes.GAMEPLAY);


            var isGameStateLoaded = false;
            _rootContainer.Resolve<IGameStateProvider>().LoadGameState().Subscribe(_ => isGameStateLoaded = true);
            yield return new WaitUntil(() => isGameStateLoaded);

            var sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();

            sceneEntryPoint.Run(enterParams).Subscribe(gameplayExitParams =>
            {
                _coroutines.StartCoroutine(LoadAndStarMainMenu(gameplayExitParams.MainMenuEnterParams));
            });

            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadAndStarMainMenu(MainMenuEnterParams enterParams = null)
        {
            _uiRoot.ShowLoadingScreen();
            yield return LoadScene(Scenes.BOOT);
            yield return new WaitForSeconds(1);
            yield return LoadScene(Scenes.MAIN_MENU);


            var sceneEntryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();

            sceneEntryPoint.Run(enterParams).Subscribe(mainMenuExitParams =>
            {
                var targetSceneName = mainMenuExitParams.TargetSceneEnterParams.SceneName;

                if (targetSceneName == Scenes.GAMEPLAY)
                {
                    _coroutines.StartCoroutine(LoadAndStartGameplay(mainMenuExitParams.TargetSceneEnterParams.As<GameplayEnterParams>()));
                }
            });

            _uiRoot.HideLoadingScreen();
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}
