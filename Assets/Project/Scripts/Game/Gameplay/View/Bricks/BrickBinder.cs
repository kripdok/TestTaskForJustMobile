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

        private BrickViewModel _viewModel;
        private Tween _tween;
        private float _animationDuration = 0.5f;

        public void Bind(BrickViewModel viewModel)
        {
            _viewModel = viewModel;
            _sprite.color = viewModel.Color;
            transform.localScale = viewModel.Scale;
            _viewModel.Position.Subscribe(e => transform.position = e);
            _viewModel.PlayAnimation += PlayAninmation;
            _viewModel.PlayAnimationWithPosition += PlayAnimationWithPosition;
        }

        private void OnDestroy()
        {
            _tween?.Kill();
            _viewModel.PlayAnimation -= PlayAninmation;
            _viewModel.PlayAnimationWithPosition -= PlayAnimationWithPosition;
        }

        public void StartHold()
        {
            _viewModel.RequestStartHold();
        }

        private void PlayAninmation(string animationNam)
        {
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
            switch (animationNam)
            {
                case BrickAnimationNameConstants.MOVE_TO_TOP_POSITION:
                    PlayMoveToTopPositionAnimation(position);
                    break;
                case BrickAnimationNameConstants.FALL_INTO_A_BLACK_HOLE:
                    PlayAnimationOfFallingIntoBlackHole(position);
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
                .AppendCallback(() => _viewModel.ChangeTestBool());
        }

        private void PlayMoveToTopPositionAnimation(Vector3 position)
        {
            float topYPosition = transform.position.y + 0.5f;

            var sequence = DOTween.Sequence();

            _tween = sequence.Append(transform.DOMoveY(topYPosition, _animationDuration))
                .Insert(0, transform.DORotate(new Vector3(0, 0, 360), _animationDuration, RotateMode.FastBeyond360))
                .Insert(_animationDuration, transform.DOMove(position, _animationDuration))
                .AppendCallback(() => _viewModel.ChangeTestBool());
        }

        private void PlayAnimationOfFallingIntoBlackHole(Vector3 position)
        {
            _sprite.sortingOrder = _concealingOrder;
            var sequence = DOTween.Sequence();

            _tween = sequence.Append(transform.DORotate(new Vector3(0, 0, 360), _animationDuration, RotateMode.FastBeyond360))
                .Insert(0, transform.DOMove(position, _animationDuration))
                .AppendCallback(() => _viewModel.ChangeTestBool());
        }
    }
}
