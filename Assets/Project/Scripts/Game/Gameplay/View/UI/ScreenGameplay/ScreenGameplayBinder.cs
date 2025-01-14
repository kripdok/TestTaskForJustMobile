using ObservableCollections;
using Project.Scripts.Game.Gameplay.View.UI.Brick;
using Project.Scripts.MVVM.UI;
using UnityEngine;
using UnityEngine.UI;
using R3;

namespace Project.Scripts.Game.Gameplay.View.UI.ScreenGameplay
{
    public class ScreenGameplayBinder : WindowBinder<ScreenGameplayViewModel>
    {
        [SerializeField] private Button _buttonBackToMenu;
        [SerializeField] private Transform _brickContainer;

        protected override void OnBind(ScreenGameplayViewModel viewModel)
        {
            base.OnBind(viewModel);
            foreach(var uiBrick in viewModel.AllUIBricks)
            {
                SetParent(uiBrick);
            }

            viewModel.AllUIBricks.ObserveAdd().Subscribe(e => 
            {
                SetParent(e.Value); 
            });
        }

        private void OnEnable()
        {
            _buttonBackToMenu?.onClick.AddListener(OnPopupAButtonClicked);

        }

        private void OnDisable()
        {
            _buttonBackToMenu?.onClick.RemoveListener(OnPopupAButtonClicked);
            
        }

        private void OnPopupAButtonClicked()
        {
            ViewModel.RequestOpenPopupBackMenu();
        }

        private void SetParent(UIBrickBinder brick)
        {
            brick.SetParent(_brickContainer);
        }

    }
}
