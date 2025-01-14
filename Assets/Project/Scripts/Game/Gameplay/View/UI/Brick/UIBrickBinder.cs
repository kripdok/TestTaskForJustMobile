using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Scripts.Game.Gameplay.View.UI.Brick
{
    public class UIBrickBinder : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image _image;

        private UIBrickViewModel _viewModel;
        private Tween _tween;
        private float _animationDuration = 0.3f;
        public void Bind(UIBrickViewModel viewModel)
        {
            _viewModel = viewModel;
            _image.color = viewModel.Color;

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _viewModel.RequestOnPointDown();


            playClickAnimation();
        }

        public void playClickAnimation()
        {
            var sequence = DOTween.Sequence();

            _tween = sequence.Append(transform.DOScale(0, _animationDuration))
                .Insert(0, transform.DORotate(new Vector3(0, 0, 360), _animationDuration, RotateMode.FastBeyond360))
                .Insert(_animationDuration, transform.DOScale(1, _animationDuration));
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
            transform.localScale = Vector3.one;
        }
    }
}
