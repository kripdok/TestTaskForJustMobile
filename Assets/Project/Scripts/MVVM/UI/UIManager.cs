using Zenject;

namespace Project.Scripts.MVVM.UI
{
    public class UIManager
    {
        protected readonly DiContainer Container;

        public UIManager(DiContainer container)
        {
            Container = container;
        }
    }
}
