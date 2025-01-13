using Project.Scripts.Game.State.Bricks;
using Project.Scripts.Game.State.Root;
using R3;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Game.State
{
    public class PlayerPrefsGameStateProvider : IGameStateProvider
    {
        private const string GAME_STATE_KEY = nameof(GAME_STATE_KEY);
        private const string GAME_SETTINGS_STATE_KEY = nameof(GAME_SETTINGS_STATE_KEY);

        public GameStateProxy GameState { get; private set; }

        private GameState _gameStateOrigin;


        public Observable<GameStateProxy> LoadGameState()
        {
            if (!PlayerPrefs.HasKey(GAME_STATE_KEY))
            {
                GameState = CreateGameStateFromSettings();
                Debug.Log("Game State created from settings: " + JsonUtility.ToJson(_gameStateOrigin, true));

                SaveGameState();
            }
            else
            {
                var json = PlayerPrefs.GetString(GAME_STATE_KEY);
                _gameStateOrigin = JsonUtility.FromJson<GameState>(json);
                GameState = new GameStateProxy(_gameStateOrigin);

                Debug.Log("Game State Loadded: " + json);
            }

            return Observable.Return(GameState);
        }

        public Observable<bool> ResetGameState()
        {
            GameState = CreateGameStateFromSettings();
            SaveGameState();
            return Observable.Return(true);
        }


        public Observable<bool> SaveGameState()
        {
            var json = JsonUtility.ToJson(_gameStateOrigin, true);
            PlayerPrefs.SetString(GAME_STATE_KEY, json);
            return Observable.Return(true);
        }

        public Observable<bool> SaveGameSettingsState()
        {
            throw new NotImplementedException();
        }

        public Observable<bool> ResetGameSettingsState()
        {
            throw new NotImplementedException();
        }

        private GameStateProxy CreateGameStateFromSettings()
        {
            //Default State
            _gameStateOrigin = new GameState
            {
                Bricks = new List<BrickEntiry>()
            };
            
            return new GameStateProxy(_gameStateOrigin);
        }
    }
}
