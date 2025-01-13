using ObservableCollections;
using Project.Scripts.Game.Gameplay.View.Bricks;
using Project.Scripts.Game.State;
using Project.Scripts.Game.State.Bricks;
using Project.Scripts.Game.State.cmd;
using R3;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Game.Gameplay.Service
{
    public class BrickService
    {
        private readonly ICommandProcessor _cmd;

        private readonly ObservableList<BrickViewModel> _allBricks = new();
        private readonly Dictionary<int, BrickViewModel> _bricksMap = new();

        public IObservableCollection<BrickViewModel> AllBricks => _allBricks;

        [Inject]
        public BrickService(IGameStateProvider gameStateProvider, ICommandProcessor cmd)
        {
            _cmd = cmd;

            var bricks = gameStateProvider.GameState.Bricks;


            foreach (var buildingEntity in bricks)
            {
                CreateBrickViewModel(buildingEntity);
            }

            bricks.ObserveAdd().Subscribe(e =>
            {
                CreateBrickViewModel(e.Value);
            });

            bricks.ObserveRemove().Subscribe(e =>
            {
                RemoveBrickViewModel(e.Value);
            });
        }

        public bool MoveBuilding(int buildingEntityID, Vector3 position)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBuilding(int buildingEntityId)
        {
            throw new NotImplementedException();
        }

        private void CreateBrickViewModel(BrickEntiryProxy brickEntityProxy)
        {
            var brickViewModel = new BrickViewModel(brickEntityProxy);

            _allBricks.Add(brickViewModel);
            _bricksMap[brickEntityProxy.Id] = brickViewModel;

            Debug.Log("Создан новый блок!");
        }

        private void RemoveBrickViewModel(BrickEntiryProxy brickEntityProxy)
        {
            if (_bricksMap.TryGetValue(brickEntityProxy.Id, out var vieModel))
            {
                _allBricks.Remove(vieModel);
            }
        }
    }
}
