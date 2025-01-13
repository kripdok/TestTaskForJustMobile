using R3;
using UnityEngine;

namespace Project.Scripts.Game.State.Bricks
{
    public class BrickEntiryProxy
    {
        public int Id { get; }
        public string TypeId { get; }

        public Color Color { get; }
        public BrickEntity Origin { get; }
        public ReactiveProperty<Vector3> Position { get; }

        public BrickEntiryProxy(BrickEntity brick)
        { 
            Id = brick.Id;
            TypeId = brick.TypeID;
            Origin = brick;
            Color = brick.Color;    

            Position = new ReactiveProperty<Vector3>(brick.Position);

            Position.Skip(1).Subscribe(value => brick.Position = value);
        }

    }
}
