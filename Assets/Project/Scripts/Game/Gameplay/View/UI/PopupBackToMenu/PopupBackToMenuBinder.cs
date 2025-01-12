using Project.Scripts.MVVM.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Game.Gameplay.View.UI.PopupBackToMenu
{
    public class PopupBackToMenuBinder : PopupBinder<PopupBackToMenuViewModel>
    {
        [SerializeField] private Button _buttonBackToMenu;

        protected override void Start()
        {
            base.Start();
            _buttonBackToMenu.onClick.AddListener(OnBakcToMenu);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _buttonBackToMenu.onClick.RemoveListener(OnBakcToMenu);
        }

        private void OnBakcToMenu()
        {
            ViewModel.RequestBackToMenu();
        }
    }
}
