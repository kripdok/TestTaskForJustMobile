using Project.Scripts.Utils;
using R3;
using System.Collections;
using UnityEngine;

namespace Project.Scripts.Game.Gameplay.Inputs
{
    public class GameplayInput : IGameplayInput
    {
        public ReadOnlyReactiveProperty<Vector3> Position => _position;
        public bool IsEndPointFound => _isEndPointFound;

        private readonly ReactiveProperty<Vector3> _position;
        private readonly InputControls _controls;
        private readonly Coroutines _coroutines;
        private bool _isEndPointFound;

        public GameplayInput(Coroutines coroutines, InputControls controls)
        {
            _position = new();
            _controls = controls;
            _coroutines = coroutines;
            EnableControls();

            _controls.GameplayMap.Click.started += cnt => OnClickStarted();
            _controls.GameplayMap.Click.canceled += cnt => OnClickCamceled();
        }

        public void DisableControls()
        {
            _controls.Disable();
        }

        public void EnableControls()
        {
            _controls.Enable();
        }
        private void OnClickStarted()
        {
            _isEndPointFound = false;
            _coroutines.StartCoroutine(MoveMerker());
        }

        private void OnClickCamceled()
        {
            _isEndPointFound = true;
        }

        private IEnumerator MoveMerker()
        {
            do
            {
                var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                position.z = 0;
                _position.Value = position;
                Debug.Log(position);
                yield return null;
            }
            while (!_isEndPointFound);
        }
    }
}
