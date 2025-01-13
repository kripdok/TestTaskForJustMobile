using ObservableCollections;
using Project.Scripts.Game.State.Bricks;
using R3;
using System.Linq;

namespace Project.Scripts.Game.State.Root
{
    public class GameStateProxy
    {
        private readonly GameState _gameState;
        public readonly ObservableList<BrickEntiryProxy> Bricks = new();

        public GameStateProxy(GameState gameState)
        {
            _gameState = gameState;

            gameState.Bricks.ForEach(brick => Bricks.Add(new BrickEntiryProxy(brick)));

            Bricks.ObserveAdd().Subscribe(e =>
            {
                var addBrick = e.Value;
                _gameState.Bricks.Add(addBrick.Origin);
            });

            Bricks.ObserveRemove().Subscribe(e =>
            {
                var removeBrick = e.Value;
                var removeBrickState = _gameState.Bricks.FirstOrDefault(b=> b.Id == removeBrick.Id);
                _gameState.Bricks.Remove(removeBrickState);
            });
            //TODO - Сделать инициализацию данных для игры
        }
    }
}
