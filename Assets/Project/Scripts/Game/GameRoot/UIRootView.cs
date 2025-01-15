using UnityEngine;

namespace Project.Scripts.Game.GameRoot
{
    public class UIRootView : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private Transform _uiSceneContainer;
        [SerializeField] private Canvas _canvas;
        [SerializeField, Range(0,500)] private int _maxSotingOrder;

        private int _defaultOrderByLevel;

        private void Awake()
        {
            HideLoadingScreen();
            _defaultOrderByLevel = _canvas.sortingOrder;
           
        }

        public void SetDefaultSoringOrder()
        {
            _canvas.sortingOrder = _defaultOrderByLevel;
        }

        public void SetMaxSortingOrder()
        {
            _canvas.sortingOrder = _maxSotingOrder;
        }

        public void ShowLoadingScreen()
        {
            _loadingScreen.SetActive(true);
        }

        public void HideLoadingScreen()
        {
            _loadingScreen.SetActive(false);
        }

        public void AttachSceneUI(GameObject sceneUI)
        {
            ClearSceneUI();

            sceneUI.transform.SetParent(_uiSceneContainer, false);
        }

        public void SetCamera(Camera camera)
        {
            _canvas.renderMode = RenderMode.ScreenSpaceCamera;
            _canvas.worldCamera = camera;
        }

        private void ClearSceneUI()
        {
            var childCount = _uiSceneContainer.childCount;

            for (int i = 0; i < childCount; i++)
            {
                Destroy(_uiSceneContainer.GetChild(i).gameObject);
            }
        }
    }
}
