using DG.Tweening;
using Project.Scripts.Game.Gameplay.View.Bricks.Common;
using Project.Scripts.Utils;
using R3;
using System;
using UnityEngine;

namespace Project.Scripts.Game.Gameplay.View.Bricks
{
    public class BrickBinder : MonoBehaviour, IRespondOnHold
    {
        [field: SerializeField] public Collider2D Collider { get; private set; }

        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private Rigidbody2D _body2D;
        [SerializeField] private int _concealingOrder;

        private bool _isInteractable;
        private BrickViewModel _viewModel;
        private Tween _tween;
        private float _animationDuration = 0.5f;
        private int _defaultOrder;

        private void Awake()
        {
            _defaultOrder = _sprite.sortingOrder;
        }

        public void Bind(BrickViewModel viewModel)
        {
            _isInteractable = true;
            _viewModel = viewModel;
            _sprite.color = viewModel.Color;
            transform.localScale = viewModel.Scale;
            _sprite.sortingOrder = _defaultOrder;
            _viewModel.Position.Subscribe(e => transform.position = e);
            _viewModel.PlayAnimation += PlayAninmation;
            _viewModel.PlayAnimationWithPosition += PlayAnimationWithPosition;
        }

        private void OnDisable()
        {
            TryUnsubscibeFromViewModelAction();
            _viewModel = null;
        }

        private void OnDestroy()
        {
            _tween?.Kill();

            TryUnsubscibeFromViewModelAction();
        }

        public void StartHold()
        {
            if (_isInteractable)
            {
                _viewModel.RequestStartHold();
            }
        }

        private void TryUnsubscibeFromViewModelAction()
        {
            if (_viewModel == null)
                return;

            _viewModel.PlayAnimation -= PlayAninmation;
            _viewModel.PlayAnimationWithPosition -= PlayAnimationWithPosition;
        }

        private void PlayAninmation(string animationNam)
        {
            _isInteractable = false;

            switch (animationNam)
            {
                case BrickAnimationNameConstants.DIE:
                    PlayDeathAnimation();
                    break;
                default:
                    throw new ArgumentException($"Animation for Brick with name {animationNam} is not registered");
            }
        }

        private void PlayAnimationWithPosition(string animationNam, Vector3 position)
        {
            _isInteractable = false;

            switch (animationNam)
            {
                case BrickAnimationNameConstants.MOVE_TO_TOP_POSITION:
                    PlayMoveToTopPositionAnimation(position);
                    break;
                case BrickAnimationNameConstants.FALL_INTO_A_BLACK_HOLE:
                    PlayAnimationOfFallingIntoBlackHole(position);
                    break;
                case BrickAnimationNameConstants.MOVE_TO_DOWN:
                    PlayAnimationOfMovingDown(position);
                    break;
                default:
                    throw new ArgumentException($"Animation for Brick with name {animationNam} is not registered");
            }
        }

        private void PlayDeathAnimation()
        {
            var sequence = DOTween.Sequence();

            _tween = sequence.Append(transform.DOScale(0, _animationDuration))
                .Insert(0, transform.DORotate(new Vector3(0, 0, 360), _animationDuration, RotateMode.FastBeyond360))
                .AppendCallback(() => ChangeViewState());
        }

        private void PlayMoveToTopPositionAnimation(Vector3 position)
        {
            float topYPosition = transform.position.y + 0.5f;

            var sequence = DOTween.Sequence();

            _tween = sequence.Append(transform.DOMoveY(topYPosition, _animationDuration))
                .Insert(0, transform.DORotate(new Vector3(0, 0, 360), _animationDuration, RotateMode.FastBeyond360))
                .Insert(_animationDuration, transform.DOMove(position, _animationDuration))
                .AppendCallback(() => ChangeViewState());
        }

        private void PlayAnimationOfFallingIntoBlackHole(Vector3 position)
        {
            _sprite.sortingOrder = _concealingOrder;
            var sequence = DOTween.Sequence();

            _tween = sequence.Append(transform.DORotate(new Vector3(0, 0, 360), _animationDuration, RotateMode.FastBeyond360))
                .Insert(0, transform.DOMove(position, _animationDuration))
                .AppendCallback(() => ChangeViewState());
        }

        private void PlayAnimationOfMovingDown(Vector3 position)
        {
            var sequence = DOTween.Sequence();

            _tween = sequence.Append(transform.DOMove(position, _animationDuration / 2))
                .AppendCallback(() => ChangeViewState());
        }

        private void ChangeViewState()
        {
            _isInteractable = true;
            _viewModel.ChangeAnimationState();
        }
    }
}
