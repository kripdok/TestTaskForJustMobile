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

        public ReadOnlyReactiveProperty<Vector3> Position { get; }

        public BrickViewModel(BrickEntiryProxy proxy)
        {
            _brickEntity = proxy;
            TypeId = proxy.TypeId;
            BrickEntityId = proxy.Id;
            Color = proxy.Color;
            Position = proxy.Position;
        }
    }
}
