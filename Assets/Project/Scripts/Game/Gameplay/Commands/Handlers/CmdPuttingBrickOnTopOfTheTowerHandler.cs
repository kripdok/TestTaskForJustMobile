using Project.Scripts.Game.State.cmd;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts.Game.Gameplay.Commands.Handlers
{
    public class CmdPuttingBrickOnTopOfTheTowerHandler : ICommandHandler<CmdPuttingBrickOnTopOfTheTower>
    {
        public Task<bool> Handle(CmdPuttingBrickOnTopOfTheTower command)
        {
            var brickPosition = command.TopBrickPosition;
            var topBrickScale = command.TopBrickScale;
            var brickThatIsPlaced = command.BrickThatIsPlaced;

            var newXPosition = Random.Range(brickPosition.x - topBrickScale.x / 2, brickPosition.x + topBrickScale.x / 2);
            var newYPosition = brickPosition.y + brickThatIsPlaced.Scale.y;

            var newPosition = new Vector3(newXPosition, newYPosition, 0);
            brickThatIsPlaced.Position.Value = newPosition;

            return Task.FromResult(true);
        }
    }
}
