using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.MVVM.UI
{
    public abstract class PopupBinder<T> : WindowBinder<T> where T : WindowViewModel
    {
        [SerializeField] private Button _buttonClose;
        [SerializeField] private Button _buttonCloseAll;

        protected virtual void Start()
        {
            _buttonClose?.onClick.AddListener(OnCloseButtonClick);
            _buttonCloseAll?.onClick.AddListener(OnCloseButtonClick);
        }

        protected virtual void OnDestroy()
        {
            _buttonClose?.onClick.RemoveListener(OnCloseButtonClick);
            _buttonCloseAll?.onClick.RemoveListener(OnCloseButtonClick);
        }

        protected virtual void OnCloseButtonClick()
        {
            ViewModel.RequestClose();
        }
    }
}
