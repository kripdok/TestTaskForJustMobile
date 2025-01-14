using UnityEngine;

namespace Project.Scripts.Game.Gameplay.Utils
{
    public class CameraSystem : MonoBehaviour
    {
        [field: SerializeField] public Camera Camera { get; private set; }

        private float CameraHeight;
        private float CameraWidth;

        public void Awake()
        {
            CameraHeight = Camera.orthographicSize * 2;
            CameraWidth = CameraHeight * Camera.aspect;
        }

        public float GetMaxYPosition()
        {
            return transform.position.y + Camera.bladeCount;
        }

        public float GetMaxXPostion()
        {
            return transform.position.x + CameraWidth / 2;
        }

    }
}
