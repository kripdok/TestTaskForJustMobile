using Project.Scripts.MVVM.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Game.Gameplay.View.UI.ScreenGameplay
{
    public class ScreenGameplayBinder : WindowBinder<ScreenGameplayViewModel>
    {
        [SerializeField] private Button _buttonPopupA;
        [SerializeField] private Button _buttonPopupB;
        [SerializeField] private Button _buttonGoToMenu;

        private void OnEnable()
        {
            _buttonPopupA?.onClick.AddListener(OnPopupAButtonClicked);
            _buttonPopupB?.onClick.AddListener(OnPopupBButtonClicked);
            _buttonGoToMenu?.onClick.AddListener(OnGoToMenuButtonClicked);
        }

        private void OnDisable()
        {

            _buttonPopupA?.onClick.RemoveListener(OnPopupAButtonClicked);
            _buttonPopupB?.onClick.RemoveListener(OnPopupBButtonClicked);
            _buttonGoToMenu?.onClick.RemoveListener(OnGoToMenuButtonClicked);
        }

        private void OnPopupAButtonClicked()
        {
            ViewModel.RequestOpenPopupA();
        }

        private void OnPopupBButtonClicked()
        {
            ViewModel.RequestOpenPopupB();
        }

        private void OnGoToMenuButtonClicked()
        {
            ViewModel.RequestGoToMainMenu();
        }
    }
}
