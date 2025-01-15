using Project.Scripts.Game.GameRoot;
using Project.Scripts.MVVM.UI;
using Zenject;

namespace Project.Scripts.Game.Gameplay.View.UI
{
    public class UIGameplayRootViewModel : UIRootViewModel
    {
        [Inject]
        public UIGameplayRootViewModel(UIRootView uIRootView) : base(uIRootView)
        {
        }
    }

}
