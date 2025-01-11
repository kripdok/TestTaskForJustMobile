using Project.Scripts.Game.Settings.Gameplay.Bricks;
using UnityEngine;

namespace Project.Scripts.Game.Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Game Settings/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        public BricksSettings bricksSettings;
    }
}
