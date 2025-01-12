﻿using System;
using System.Collections.Generic;

namespace Project.Scripts.Game.State.cmd
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly Dictionary<Type, object> _handlesMap = new();
        private readonly IGameStateProvider _gameStateProvider;

        public CommandProcessor(IGameStateProvider gameStateProvider)
        {
            _gameStateProvider = gameStateProvider;
        }

        public bool Process<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (_handlesMap.TryGetValue(typeof(TCommand), out var handler))
            {
                var typeHandler = (ICommandHandler<TCommand>)handler;
                var result = typeHandler.Handle(command);

                if (result)
                {
                    _gameStateProvider.SaveGameState();
                }

                return result;
            }

            return false;
        }

        public void RegisterHandler<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand
        {
            _handlesMap[typeof(TCommand)] = handler;
        }
    }
}