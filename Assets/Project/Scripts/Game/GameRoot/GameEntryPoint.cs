using Project.Scripts.Game.Settings;
using Project.Scripts.Utils;
using System.Collections;
using System.ComponentModel;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Project.Scripts.Game.GameRoot
{
    public class GameEntryPoint
    {
        private static GameEntryPoint _instance;
        private Coroutines _coroutines;
        private ProjectContext _projectContext;

        public static DiContainer  Container;


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
            _coroutines = new GameObject(name: "[COROUTINES]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(_coroutines.gameObject);
            Container = new DiContainer();

            Container.Bind<ISettingsProvider>().To<LocalSettingsProvider>().AsSingle().NonLazy();

            RunGame();
        }

        
        private async void RunGame()
        {
            await Container.Resolve<ISettingsProvider>().LoadGameSettings();
            _coroutines.StartCoroutine(LoadGameplay());
            //TODO Сделать загрузку сцены с зенджектом, который осуществляет создание основных настроект приложения
        }


        private IEnumerator LoadGameplay()
        {
            Debug.Log("StartCoroutin");
            yield return new WaitForSeconds(3);
            yield return LoadScene(Scenes.GAMEPLAY);
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}


