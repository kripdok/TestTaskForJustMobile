using Project.Scripts.Utils;
using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Project.Scripts.Game.Gameplay.Inputs
{
    public class GameplayInput : IGameplayInput
    {
        public ReadOnlyReactiveProperty<Vector3> Position => _position;
        public bool IsEndPointFound => _isEndPointFound;
        public bool IsPointsToUI => _isPointsToUI;

        private readonly ReactiveProperty<Vector3> _position;
        private readonly InputControls _controls;
        private readonly Coroutines _coroutines;
        private bool _isEndPointFound;
        private bool _isPointsToUI;

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
            _isPointsToUI = false;
            _isEndPointFound = false;
            _coroutines.StartCoroutine(MoveMerker());
            TryGeTRespondOnHoldObject();
        }

        private void TryGeTRespondOnHoldObject()
        {
            var hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

            if (hit.collider == null)
                return;

            if (hit.collider.TryGetComponent(out IRespondOnHold component))
            {
                component.StartHold();
            }
        }

        private void OnClickCamceled()
        {
            _isPointsToUI = CheckIfItPointsToUI();
            _isEndPointFound = true;
        }

        private bool CheckIfItPointsToUI()
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> results = new List<RaycastResult>();
            GraphicRaycaster raycaster = GameObject.FindObjectOfType<GraphicRaycaster>();

            raycaster.Raycast(pointerData, results);

            foreach (RaycastResult result in results)
            {
                Image image = result.gameObject.GetComponent<Image>();

                if (image != null)
                {
                    return true;
                }
            }

            return false;
        }

        private IEnumerator MoveMerker()
        {
            do
            {
                var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                position.z = 0;
                _position.Value = position;
                yield return null;
            }
            while (!_isEndPointFound);
        }
    }
}
