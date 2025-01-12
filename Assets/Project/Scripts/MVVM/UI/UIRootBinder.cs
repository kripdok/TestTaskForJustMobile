using ObservableCollections;
using R3;
using UnityEngine;

namespace Project.Scripts.MVVM.UI
{
    public class UIRootBinder : MonoBehaviour
    {
        [SerializeField] private WindowsContainer _container;
        private readonly CompositeDisposable _subscrtiptions = new();

        public void Bind(UIRootViewModel viewModel)
        {
            _subscrtiptions.Add(viewModel.OpenedScreen.Subscribe(newScreenViewModel =>
            {
                _container.OpenScreen(newScreenViewModel);
            }));

            foreach (var openedPopups in viewModel.OpenedPopups)
            {
                _container.OpenPopup(openedPopups);
            }

            _subscrtiptions.Add(viewModel.OpenedPopups.ObserveAdd().Subscribe(e =>
            {
                _container.OpenPopup(e.Value);
            }
            ));

            _subscrtiptions.Add(viewModel.OpenedPopups.ObserveRemove().Subscribe(e =>
            {
                _container.ClosePopup(e.Value);
            }));

            OnBind(viewModel);

        }

        protected virtual void OnBind(UIRootViewModel viewModel = null) { }

        private void OnDestroy()
        {
            _subscrtiptions.Dispose();
            Debug.Log("Dispos");
        }
    }
}
