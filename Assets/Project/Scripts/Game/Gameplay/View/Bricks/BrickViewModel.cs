using Project.Scripts.Game.State.Bricks;
using R3;
using UnityEngine;

namespace Project.Scripts.Game.Gameplay.View.Bricks
{
    public class BrickViewModel
    {
        private readonly BrickEntiryProxy _brickEntity;

        public readonly int BrickEntityId;
        public readonly string TypeId;
        public readonly Color Color;
        public readonly Vector3 Scale;
        public readonly Subject<int> OnStartHold = new();

        public ReadOnlyReactiveProperty<Vector3> Position { get; }

        public BrickViewModel(BrickEntiryProxy proxy)
        {
            _brickEntity = proxy;
            TypeId = proxy.TypeId;
            BrickEntityId = proxy.Id;
            Color = proxy.Color;
            Position = proxy.Position;
            Scale = proxy.Scale;
        }

        public void RequestStartHold()
        {
            OnStartHold.OnNext(BrickEntityId);
        }
    }
}
