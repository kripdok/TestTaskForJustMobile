using Project.Scripts.Game.Gameplay.Inputs;
using Project.Scripts.Game.State.cmd;
using R3;
using System.Threading.Tasks;

namespace Project.Scripts.Game.Gameplay.Commands.Handlers
{
    public class CmdBrickFollowPointerHandler : ICommandHandler<CmdBrickFollowPointer>
    {
        private readonly IGameplayInput _gameplayInput;

        public CmdBrickFollowPointerHandler(IGameplayInput gameplayInput)
        {
            _gameplayInput = gameplayInput;
        }

        async Task<bool> ICommandHandler<CmdBrickFollowPointer>.Handle(CmdBrickFollowPointer command)
        {
            var subscription = _gameplayInput.Position.Subscribe(position =>
            {
                 command._brickEntityProxy.Position.Value = position;
            });

            do
            {
                await Task.Yield();
            } while (!_gameplayInput.IsEndPointFound);

            subscription?.Dispose();

            return true;
        }
    }
}

