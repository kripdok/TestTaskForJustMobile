using Project.Scripts.Game.Gameplay.Utils;
using Project.Scripts.Game.Gameplay.Inputs;
using Project.Scripts.Game.State.cmd;
using System.Threading.Tasks;
using Project.Scripts.Game.State.Bricks;

namespace Project.Scripts.Game.Gameplay.Commands.Handlers
{
    public class CmdBrickFollowPointerHandler : ICommandHandler<CmdBrickFollowPointer>
    {
        private readonly IGameplayInput _gameplayInput;
        private readonly CameraSystem _cameraSystem;

        public CmdBrickFollowPointerHandler(IGameplayInput gameplayInput,CameraSystem cameraSystem)
        {
            _cameraSystem = cameraSystem;
            _gameplayInput = gameplayInput;
        }

        async Task<bool> ICommandHandler<CmdBrickFollowPointer>.Handle(CmdBrickFollowPointer command)
        {
            do
            {
                command.BrickEntityProxy.Position.Value = _gameplayInput.Position.CurrentValue;
                await Task.Yield();
            } while (!_gameplayInput.IsEndPointFound);

            if(_gameplayInput.IsPointsToUI == true)
            {
                return false;
            }

            if(command.BrickEntityProxy.Position.Value.x< _cameraSystem.transform.position.x + command.BrickEntityProxy.Scale.x/2)
            {
                return false;
            }

            if(command.BrickEntityProxy.Position.Value.y > _cameraSystem.GetMaxYPosition() )
            {
                return false;
            }

            return true;
        }
    }
}

