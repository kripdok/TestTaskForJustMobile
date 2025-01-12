﻿using UnityEngine;

namespace Project.Scripts.MVVM.UI
{
    public abstract class WindowBinder<T> : MonoBehaviour, IWindowBinder where T : WindowViewModel
    {
        protected T ViewModel;

        public void Bind(WindowViewModel viewModel)
        {
            ViewModel = (T)viewModel;
            OnBind(ViewModel);
        }

        public virtual void Close()
        {
            //Здесь мы будем уничтожать, а потом делать анимации на закрытие
            Destroy(gameObject);
        }

        protected virtual void OnBind(T viewModel) { }
    }
}
