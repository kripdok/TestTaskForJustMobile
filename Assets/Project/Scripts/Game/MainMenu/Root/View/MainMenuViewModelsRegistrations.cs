using Project.Scripts.Game.MainMenu.View;
using Project.Scripts.Game.MainMenu.View.UI;
using Zenject;

namespace Project.Scripts.Game.MainMenu.Root.View
{
    internal class MainMenuViewModelsRegistrations
    {
        public MainMenuViewModelsRegistrations(DiContainer container)
        {
            Register(container);
        }

        private void Register(DiContainer diContainer)
        {
            diContainer.Bind<MainMenuUIManager>().AsCached();
            diContainer.Bind<UIMainMenuRootViewModel>().AsCached();
        }
    }
}
