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
            //diContainer.Bind<GameplayUIManager>().AsCached();
            //diContainer.Bind<UIGameplayRootViewModel>().AsCached();
        }
    }
}
