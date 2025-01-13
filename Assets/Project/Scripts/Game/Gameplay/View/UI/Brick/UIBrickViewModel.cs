using Project.Scripts.Game.Settings.Gameplay.Bricks;
using UnityEngine;
using R3;
using Unity.VisualScripting;

namespace Project.Scripts.Game.Gameplay.View.UI.Brick
{
    public class UIBrickViewModel
    {
        public readonly Color Color;
        public readonly Subject<string> OnPointDown = new ();
        
        private readonly string _typeId;

        public UIBrickViewModel(BrickInitialStateSettings brickSettings)
        {
            Color = brickSettings.Color;
            _typeId = brickSettings.TypeId;
        }

        public void RequestOnPointDown()
        {
            OnPointDown.OnNext(_typeId);
        }
    }
}
