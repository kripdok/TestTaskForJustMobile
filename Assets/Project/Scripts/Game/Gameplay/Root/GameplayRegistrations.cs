using Project.Scripts.Game.Common;
using Project.Scripts.Game.Gameplay.Commands.Handlers;
using Project.Scripts.Game.Gameplay.Service;
using Project.Scripts.Game.Settings;
using Project.Scripts.Game.State;
using Project.Scripts.Game.State.cmd;
using R3;
using Zenject;

namespace Project.Scripts.Game.Gameplay.Root
{
    public class GameplayRegistrations
    {
        public GameplayRegistrations(DiContainer diContainer, GameplayEnterParams enterParams)
        {
            Register(diContainer, enterParams);
        }

        private void Register(DiContainer diContainer, GameplayEnterParams enterParams)
        {
            //TODO -сделать сначало создание cmd а потом ее регистрацию. Так будет по уму
            diContainer.Bind<ICommandProcessor>().To<CommandProcessor>().AsCached();
            var cmd = diContainer.Resolve<ICommandProcessor>();
            cmd.RegisterHandler(new CmdCreateBrickStateHandler(diContainer.Resolve<IGameStateProvider>().GameState, diContainer.Resolve<ISettingsProvider>().GameSettings));


            diContainer.Bind<Subject<Unit>>().WithId(AppConstants.ENIT_SCENE_REQUEST_TAG).AsCached();
            diContainer.Bind<BrickService>().AsCached();

        }
    }
}
