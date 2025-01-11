using System.Threading.Tasks;

namespace Project.Scripts.Game.Settings
{
    public interface ISettingsProvider
    {
        public GameSettings GameSettings { get; }

        public Task<GameSettings> LoadGameSettings();
    }
}
