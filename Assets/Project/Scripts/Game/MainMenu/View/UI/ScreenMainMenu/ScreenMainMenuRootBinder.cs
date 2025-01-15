using Project.Scripts.Game.MainMenu.View.UI.ScreenMainMenu;
using Project.Scripts.MVVM.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Game.MainMenu.View
{
    public class ScreenMainMenuRootBinder : WindowBinder<ScreenMainMenuViewModel>
    {
        [SerializeField] private Button _buttonBackToGameplay;

        protected override void OnBind(ScreenMainMenuViewModel viewModel)
        {
            base.OnBind(viewModel);
            _buttonBackToGameplay.onClick.AddListener(OnBakcToMenu);
        }

        public override void Close()
        {
            base.Close();
            _buttonBackToGameplay.onClick.RemoveListener(OnBakcToMenu);
        }

        private void OnBakcToMenu()
        {
            ViewModel.RequestBackToGameplay();
        }
    }

}
