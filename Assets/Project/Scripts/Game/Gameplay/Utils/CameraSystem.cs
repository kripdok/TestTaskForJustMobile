using UnityEngine;

namespace Project.Scripts.Game.Gameplay.Utils
{
    public class CameraSystem: MonoBehaviour
    {
        [field: SerializeField] public Camera Camera { get; private set; }

        public float GetMaxYPosition()
        {
            return transform.position.y + Camera.bladeCount;
        }
    }
}
