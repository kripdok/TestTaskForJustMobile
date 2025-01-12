using Zenject;

namespace Project.Scripts.Game.MainMenu.View.UI
{
    public class UIMainMenuRootBinder
    {
        public UIMainMenuRootBinder(DiContainer container)
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
