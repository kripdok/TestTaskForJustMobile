using ObservableCollections;
using Project.Scripts.Game.Gameplay.Commands;
using Project.Scripts.Game.Gameplay.View.UI.Brick;
using Project.Scripts.Game.Settings;
using Project.Scripts.Game.Settings.Gameplay.Bricks;
using Project.Scripts.Game.State.cmd;
using R3;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Game.Gameplay.Service.UI
{
    public class UIBricksService
    {
        public IObservableCollection<UIBrickBinder> AllUIBricks => _allUIBricksList;

        private readonly ICommandProcessor _cmd;
        private readonly ObservableList<UIBrickBinder> _allUIBricksList = new();

        [Inject]
        public UIBricksService(ISettingsProvider settingsProvider,ICommandProcessor cmd)
        {
            _cmd = cmd;
            var settings = settingsProvider.GameSettings.bricksSettings.settings;

            foreach (var brickSettings in settings)
            {
                CreateUIBrickBinder(brickSettings);
            }
        }

        private void CreateUIBrickBinder(BrickInitialStateSettings brickSettings)
        {
            var uiBrickViewModel = new UIBrickViewModel(brickSettings);
            var uiBrickBinderPrefab = Resources.Load<UIBrickBinder>("Prefabs/UI/UIBrick");
            var uiBrickBinder = GameObject.Instantiate(uiBrickBinderPrefab); //TODO -Возможно не здесь должен находится
            uiBrickBinder.Bind(uiBrickViewModel);
            _allUIBricksList.Add(uiBrickBinder);

            uiBrickViewModel.OnPointDown.Subscribe(CreateNewBrick);
        }

        private void CreateNewBrick(string typeID)
        {
            var command = new CmdCreateBrickState(typeID); 
            _cmd.Process(command);

        }
    }
}
