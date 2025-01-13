using Project.Scripts.Game.Settings;
using Project.Scripts.Game.State.Bricks;
using Project.Scripts.Game.State.cmd;
using Project.Scripts.Game.State.Root;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts.Game.Gameplay.Commands.Handlers
{
    public class CmdCreateBrickStateHandler : ICommandHandler<CmdCreateBrickState>
    {
        private readonly GameStateProxy _gameState;
        private readonly GameSettings _gameSettings;

        public CmdCreateBrickStateHandler(GameStateProxy gameState, GameSettings gameSettings)
        {
            _gameState = gameState;
            _gameSettings = gameSettings;
        }

        Task<bool> ICommandHandler<CmdCreateBrickState>.Handle(CmdCreateBrickState command)
        {
            var brickSettings = _gameSettings.bricksSettings.settings.FirstOrDefault(brick => brick.TypeId == command.TypeId);
            var brickEntity = new BrickEntity
            {
                Color = brickSettings.Color,
                TypeID = command.TypeId,
                Id = _gameState.CreateEntityId(),
                Position = Vector3.zero
            };

            _gameState.Bricks.Add(new BrickEntiryProxy(brickEntity));

            return Task.FromResult(true);
        }
    }
}
