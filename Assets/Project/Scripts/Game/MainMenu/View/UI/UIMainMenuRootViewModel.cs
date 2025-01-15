using Project.Scripts.Game.GameRoot;
using Project.Scripts.MVVM.UI;
using Zenject;

namespace Project.Scripts.Game.MainMenu.View.UI
{
    public class UIMainMenuRootViewModel : UIRootViewModel
    {
        [Inject]
        public UIMainMenuRootViewModel(UIRootView uIRootView) : base(uIRootView)
        {
        }
    }
}
