using R3;
using UnityEngine;

namespace Project.Scripts.Game.Gameplay.Inputs
{
    public interface IGameplayInput
    {
        public ReadOnlyReactiveProperty<Vector3> Position { get; }
        public bool IsEndPointFound { get; }
        public bool IsPointsToUI { get; }
    }
}
