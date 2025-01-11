using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Scripts.Game.Settings.Gameplay.Bricks
{
    [Serializable]
    public class BrickInitialStateSettings
    {
        public Color Color;
        public string TypeId => Color.ToHexString();
    }
}
