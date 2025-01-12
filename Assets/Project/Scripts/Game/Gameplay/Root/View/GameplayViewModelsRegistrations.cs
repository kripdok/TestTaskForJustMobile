using Project.Scripts.Game.Gameplay.View.UI;
using Zenject;

namespace Project.Scripts.Game.Gameplay.Root.View
{
    public class GameplayViewModelsRegistrations
    {
        public GameplayViewModelsRegistrations(DiContainer container)
        {
            Register(container);
        }

        private void Register(DiContainer diContainer)
        {
            diContainer.Bind<GameplayUIManager>().AsCached();
            diContainer.Bind<UIGameplayRootViewModel>().AsCached();
        }
    }
}
