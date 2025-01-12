using Project.Scripts.Game.GameRoot;

namespace Project.Scripts.Game.MainMenu.Root
{
    public class MainMenuExitParams
    {
        public readonly SceneEnterParams TargetSceneEnterParams;

        public MainMenuExitParams(SceneEnterParams targetSceneEnterParams)
        {
            TargetSceneEnterParams = targetSceneEnterParams;
        }
    }
}
