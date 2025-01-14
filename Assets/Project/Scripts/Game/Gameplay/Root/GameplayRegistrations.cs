using Project.Scripts.Game.Common;
using Project.Scripts.Game.Gameplay.Commands;
using Project.Scripts.Game.Gameplay.Commands.Handlers;
using Project.Scripts.Game.Gameplay.Inputs;
using Project.Scripts.Game.Gameplay.Root.View;
using Project.Scripts.Game.Gameplay.Service;
using Project.Scripts.Game.Settings;
using Project.Scripts.Game.State;
using Project.Scripts.Game.State.cmd;
using Project.Scripts.Utils;
using R3;
using Zenject;

namespace Project.Scripts.Game.Gameplay.Root
{
    public class GameplayRegistrations
    {
        public GameplayRegistrations(DiContainer diContainer, GameplayEnterParams enterParams, WorldGameplayRootBinder worldRootBinder)
        {
            Register(diContainer, enterParams, worldRootBinder);
        }

        private void Register(DiContainer diContainer, GameplayEnterParams enterParams, WorldGameplayRootBinder worldRootBinder)
        {

            var gameplayInput = new GameplayInput(diContainer.Resolve<Coroutines>(), new InputControls());

            var cmd = new CommandProcessor();
            cmd.RegisterHandler(new CmdCreateBrickStateHandler(diContainer.Resolve<IGameStateProvider>().GameState, diContainer.Resolve<ISettingsProvider>().GameSettings));
            cmd.RegisterHandler(new CmdBrickFollowPointerHandler(gameplayInput));
            cmd.RegisterHandler(new CmdColliderIntersectionCheckHandler());
            cmd.RegisterHandler(new CmdBrickCollisionCheckHandler());
            cmd.RegisterHandler(new CmdBlackHoleCollisionCheckHandler());
            cmd.RegisterHandler(new CmdPuttingBrickOnTopOfTheTowerHandler());

            diContainer.Bind<ICommandProcessor>().FromInstance(cmd).AsCached();
            diContainer.Bind<IGameplayInput>().FromInstance(gameplayInput).AsCached();
            diContainer.Bind<BrickService>().AsCached();
            diContainer.Bind<Subject<Unit>>().WithId(AppConstants.ENIT_SCENE_REQUEST_TAG).AsCached();
            diContainer.Bind<WorldGameplayRootBinder>().FromInstance(worldRootBinder).AsCached();

        }
    }
}
