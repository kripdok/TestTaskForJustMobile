using System.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts.Game.Settings
{
    public class LocalSettingsProvider : ISettingsProvider
    {
        public GameSettings GameSettings => _gameSettings;

        private GameSettings _gameSettings;

        public Task<GameSettings> LoadGameSettings()
        {
            _gameSettings = Resources.Load<GameSettings>("GameSettings");
            return Task.FromResult(GameSettings);
        }
    }
}