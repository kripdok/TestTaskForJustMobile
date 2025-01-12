using Project.Scripts.Game.Settings.Gameplay.Bricks;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Game.Gameplay.View.UI.Brick
{
    public class UIBrickViewModel
    {
        public readonly Color Color;

        private readonly string Id;

        public UIBrickViewModel(BrickInitialStateSettings brickSettings)
        {
            Color = brickSettings.Color;
            Id = brickSettings.TypeId;
        }

        public void RequestOnPointDown()
        {
            //TODO - должен создавать блок на экране, который можно перемещать

            Debug.Log("Brick Clicked!");
        }
    }
}
