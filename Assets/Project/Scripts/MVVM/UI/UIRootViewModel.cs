using ObservableCollections;
using Project.Scripts.Game.GameRoot;
using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Project.Scripts.MVVM.UI
{
    public class UIRootViewModel : IDisposable
    {
        public ReadOnlyReactiveProperty<WindowViewModel> OpenedScreen => _openedScreen;
        public IObservableCollection<WindowViewModel> OpenedPopups => _openedPopups;

        private readonly ReactiveProperty<WindowViewModel> _openedScreen = new();
        private readonly ObservableList<WindowViewModel> _openedPopups = new();
        private readonly Dictionary<WindowViewModel, IDisposable> _popupSubscriptions = new();
        private readonly UIRootView _uiRootView;

        [Inject]
        public UIRootViewModel(UIRootView uIRootView)
        {
            _uiRootView = uIRootView;
        }

        public void Dispose()
        {
            //TODO - нужен ли здесь IDisposable?
            CloseAllPopups();
            _openedScreen.Value?.Dispose();
        }

        public void OpenScreen(WindowViewModel screenViewModel)
        {
            _openedScreen.Value?.Dispose();
            _openedScreen.Value = screenViewModel;
            _uiRootView.SetDefaultSoringOrder();
        }

        public void OpenPupup(WindowViewModel popupViewModel)
        {
            if (_openedPopups.Contains(popupViewModel))
            {
                return;
            }

            var subscription = popupViewModel.CloseRequested.Subscribe(ClosePupup);
            _popupSubscriptions[popupViewModel] = subscription;
            _openedPopups.Add(popupViewModel);
            _uiRootView.SetMaxSortingOrder();
        }

        public void ClosePupup(WindowViewModel popupViewModel)
        {
            if (_openedPopups.Contains(popupViewModel))
            {
                popupViewModel.Dispose();
                _openedPopups.Remove(popupViewModel);

                var popupSubscription = _popupSubscriptions[popupViewModel];
                popupSubscription?.Dispose();
                _popupSubscriptions.Remove(popupViewModel);
            }

            if(_openedPopups.Count == 0)
            {
                _uiRootView.SetDefaultSoringOrder();
            }
        }

        public void ClodePopup(string popupId)
        {
            var openedPopupViewModel = _openedPopups.FirstOrDefault(x => x.Id == popupId);
            ClosePupup(openedPopupViewModel);
            
        }

        public void CloseAllPopups()
        {
            foreach (var openedPopup in _openedPopups)
            {
                ClosePupup(openedPopup);
            }

            _uiRootView.SetDefaultSoringOrder();
        }
    }
}
