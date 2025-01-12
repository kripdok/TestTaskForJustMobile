using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Scripts.Game.Gameplay.View.UI.Brick
{
    public class UIBrickBinder : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image _image;
        private UIBrickViewModel _viewModel;

        public void Bind(UIBrickViewModel viewModel)
        {
            _viewModel = viewModel;
            _image.color = viewModel.Color;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _viewModel.RequestOnPointDown();
        }
    }
}
