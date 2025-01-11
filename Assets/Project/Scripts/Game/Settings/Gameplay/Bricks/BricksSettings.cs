using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Game.Settings.Gameplay.Bricks
{
    [CreateAssetMenu(fileName = "BricksSettings", menuName = "Game Settings/Bricks/New Bricks Settings")]
    public class BricksSettings : ScriptableObject
    {
        public List<BrickInitialStateSettings> settings;
    }
}
