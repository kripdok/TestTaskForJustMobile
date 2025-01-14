using Project.Scripts.Game.Gameplay.View.Bricks.Common;
using Project.Scripts.Game.State.Bricks;
using R3;
using System;
using UnityEngine;

namespace Project.Scripts.Game.Gameplay.View.Bricks
{
    public class BrickViewModel
    {
        private readonly BrickEntityProxy _brickEntity;

        public readonly int BrickEntityId;
        public readonly string TypeId;
        public readonly Color Color;
        public readonly Vector3 Scale;
        public readonly Subject<int> OnStartHold = new();

        public ReadOnlyReactiveProperty<Vector3> Position { get; }

        public bool IsAnimationPlayed { get; private set; }

        public event Action<string> PlayAnimation;
        public event Action<string,Vector3> PlayAnimationWithPosition;

        public BrickViewModel(BrickEntityProxy proxy)
        {
            _brickEntity = proxy;
            TypeId = proxy.TypeId;
            BrickEntityId = proxy.Id;
            Color = proxy.Color;
            Position = proxy.Position;
            Scale = proxy.Scale;
            IsAnimationPlayed = false;
        }

        public void RequestStartHold()
        {
            OnStartHold.OnNext(BrickEntityId);
        }

        public void PlayDeathAnimation()
        {
            IsAnimationPlayed = false;
            PlayAnimation?.Invoke(BrickAnimationNameConstants.DIE);
        }

        public void PlayMoveToTopPositionAnimation(Vector3 position)
        {
            IsAnimationPlayed = false;
            PlayAnimationWithPosition?.Invoke(BrickAnimationNameConstants.MOVE_TO_TOP_POSITION, position);
        }

        public void PlayAnimationOfFallingIntoBlackHole(Vector3 position)
        {
            IsAnimationPlayed = false;
            PlayAnimationWithPosition?.Invoke(BrickAnimationNameConstants.FALL_INTO_A_BLACK_HOLE, position);
        }

        public void ChangeTestBool()
        {
            IsAnimationPlayed = true;
        }
    }
}
