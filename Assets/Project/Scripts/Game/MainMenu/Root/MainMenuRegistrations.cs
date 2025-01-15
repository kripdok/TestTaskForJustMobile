using Project.Scripts.Game.Common;
using R3;
using Zenject;

namespace Project.Scripts.Game.MainMenu.Root
{
    public class MainMenuRegistrations
    {
        public MainMenuRegistrations(DiContainer diContainer, MainMenuEnterParams enterParams)
        {
            Register(diContainer, enterParams);
        }

        private void Register(DiContainer diContainer, MainMenuEnterParams enterParams)
        {
            diContainer.Bind<Subject<Unit>>().WithId(AppConstants.ENIT_SCENE_REQUEST_TAG).AsCached();
        }
    }
}
