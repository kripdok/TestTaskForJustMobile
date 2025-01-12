using Project.Scripts.Game.MainMenu.Root;

namespace Project.Scripts.Game.Gameplay.Root
{
    public class GameplayExitParams
    {
        public readonly MainMenuEnterParams MainMenuEnterParams;

        public GameplayExitParams(MainMenuEnterParams mainMenuEnterParams)
        {
            MainMenuEnterParams = mainMenuEnterParams;
        }
    }
}
