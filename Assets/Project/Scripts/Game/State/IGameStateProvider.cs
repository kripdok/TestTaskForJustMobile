
using Project.Scripts.Game.State.Root;
using R3;

namespace Project.Scripts.Game.State
{
    public interface IGameStateProvider
    {
        public GameStateProxy GameState { get; }
        public Observable<GameStateProxy> LoadGameState();
        public Observable<bool> SaveGameState();
        public Observable<bool> SaveGameSettingsState();
        public Observable<bool> ResetGameState();
        public Observable<bool> ResetGameSettingsState();
    }
}
