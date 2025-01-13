using ObservableCollections;
using Project.Scripts.Game.Gameplay.Commands;
using Project.Scripts.Game.Gameplay.Commands.Handlers;
using Project.Scripts.Game.Gameplay.View.Bricks;
using Project.Scripts.Game.State;
using Project.Scripts.Game.State.Bricks;
using Project.Scripts.Game.State.cmd;
using R3;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
                MoveBuilding(e.Value);
            });

            bricks.ObserveRemove().Subscribe(e =>
            {
                RemoveBrickViewModel(e.Value);
            });
        }

        public async void MoveBuilding(BrickEntiryProxy brickEntityProxy)
        {
           

            var command = new CmdBrickFollowPointer(brickEntityProxy);

            await _cmd.AsuncProcess(command);
            //TODO - сделать команду,  которая будет делать приследование блока за курсором
            //Если все прошло успешно выполняется проверка на лазер или на нахождение в другом блоке
            //После проверки блок либо сохраняет позицию, либо удаляется, либо падает в яму
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
