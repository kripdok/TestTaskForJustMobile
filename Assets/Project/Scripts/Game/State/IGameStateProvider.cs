
using R3;

namespace Project.Scripts.Game.State
{
    public interface IGameStateProvider
    {
        public Observable<bool> SaveGameState();
        public Observable<bool> SaveGameSettingsState();
        public Observable<bool> ResetGameState();
        public Observable<bool> ResetGameSettingsState();
    }
}
