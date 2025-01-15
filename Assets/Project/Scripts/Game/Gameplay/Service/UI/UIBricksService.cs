using ObservableCollections;
using Project.Scripts.Game.Gameplay.Commands;
using Project.Scripts.Game.Gameplay.Factories;
using Project.Scripts.Game.Gameplay.View.UI.Brick;
using Project.Scripts.Game.Settings;
using Project.Scripts.Game.Settings.Gameplay.Bricks;
using Project.Scripts.Game.State.cmd;
using R3;
using Zenject;

namespace Project.Scripts.Game.Gameplay.Service.UI
{
    public class UIBricksService
    {
        private readonly ICommandProcessor _cmd;
        private readonly ObservableList<UIBrickBinder> _allUIBricksList = new();
        private readonly UIBrickBinderFactory _factory;

        public IObservableCollection<UIBrickBinder> AllUIBricks => _allUIBricksList;

        [Inject]
        public UIBricksService(ISettingsProvider settingsProvider, ICommandProcessor cmd, UIBrickBinderFactory factory)
        {
            _factory = factory;
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
            var uiBrickBinder = _factory.Create();
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
