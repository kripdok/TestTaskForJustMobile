using R3;
using System;

namespace Project.Scripts.MVVM.UI
{
    public abstract class WindowViewModel : IDisposable
    {
        public Observable<WindowViewModel> CloseRequested => _closeRequested;
        public abstract string Id { get; }

        public readonly Subject<WindowViewModel> _closeRequested = new();

        public void RequestClose()
        {
            _closeRequested.OnNext(this);
        }

        public virtual void Dispose() { }
    }
}
