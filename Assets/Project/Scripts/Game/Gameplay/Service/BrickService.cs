using ObservableCollections;
using Project.Scripts.Game.Gameplay.Commands;
using Project.Scripts.Game.Gameplay.Root.View;
using Project.Scripts.Game.Gameplay.View.Bricks;
using Project.Scripts.Game.State;
using Project.Scripts.Game.State.Bricks;
using Project.Scripts.Game.State.cmd;
using R3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Game.Gameplay.Service
{
    public class BrickService
    {
        private readonly ICommandProcessor _cmd;
        private readonly IGameStateProvider _gameStateProvider;
        private readonly WorldGameplayRootBinder _worldRootBinder;
        private readonly ObservableList<BrickViewModel> _allBricks = new();
        private readonly Dictionary<int, BrickViewModel> _bricksMap = new();

        public IObservableCollection<BrickViewModel> AllBricks => _allBricks;

        [Inject]
        public BrickService(IGameStateProvider gameStateProvider, ICommandProcessor cmd, WorldGameplayRootBinder worldRootBinder)
        {
            _cmd = cmd;
            _worldRootBinder = worldRootBinder;
            _gameStateProvider = gameStateProvider;
            var bricks = gameStateProvider.GameState.Bricks;

            foreach (var buildingEntity in bricks)
            {
                CreateBrickViewModel(buildingEntity);
            }

            bricks.ObserveAdd().Subscribe(e =>
            {
                CreateBrickViewModel(e.Value);
                MoveBrickAndCollisionCheck(e.Value);
            });

            bricks.ObserveRemove().Subscribe(e =>
            {
                RemoveBrickViewModel(e.Value);
            });
        }

        private async void MoveBrickAndCollisionCheck(BrickEntiryProxy brickEntityProxy)
        {

            await MoveBrick(brickEntityProxy);

            var isColliderIntersection = _cmd.Process(
                new CmdColliderIntersectionCheck(_worldRootBinder.GetBrickBinderCollider(brickEntityProxy.Id)));

            var isBrickCollisionCheked = _cmd.Process(
                new CmdBrickCollisionCheck(_worldRootBinder.GetBrickBinderCollider(brickEntityProxy.Id)));

            if (!isColliderIntersection || !isBrickCollisionCheked)
            {
                if (_allBricks.Count > 1)
                {
                    Debug.Log("Блок уничтожен");
                    RemoveBrickViewModel(brickEntityProxy);
                    DeleteBuilding(brickEntityProxy.Id);
                    return;
                }
               
            }

      
            ChangeBrickPosition(brickEntityProxy);
            _gameStateProvider.SaveGameState();
            //TODO - сделать команду,  которая будет делать приследование блока за курсором
            //Если все прошло успешно выполняется проверка на лазер или на нахождение в другом блоке
            //После проверки блок либо сохраняет позицию, либо удаляется, либо падает в яму
        }

        private async Task MoveBrick(BrickEntiryProxy brickEntityProxy)
        {
            await _cmd.AsuncProcess(new CmdBrickFollowPointer(brickEntityProxy));
        }

        private bool DeleteBuilding(int brickEntityId)
        {
            var bricks = _gameStateProvider.GameState.Bricks;
            var brick = bricks.FirstOrDefault(b => b.Id == brickEntityId);

            if(brick != null)
            {
                bricks.Remove(brick);
                return true;
            }

            return false;
        }

        private void CreateBrickViewModel(BrickEntiryProxy brickEntityProxy)
        {
            var brickViewModel = new BrickViewModel(brickEntityProxy);
            brickViewModel.OnStartHold.Subscribe(TryToThrowOutBrick);
            _allBricks.Add(brickViewModel);
            _bricksMap[brickEntityProxy.Id] = brickViewModel;

            Debug.Log("Создан новый блок!");
        }


        private void RemoveBrickViewModel(BrickEntiryProxy brickEntityProxy)
        {
            if (_bricksMap.TryGetValue(brickEntityProxy.Id, out var vieModel))
            {
                vieModel.OnStartHold.Dispose();
                _allBricks.Remove(vieModel);
            }
        }

        private void ChangeBrickPosition(BrickEntiryProxy brickEntityProxy)
        {
            var sortBricks = _allBricks.OrderBy(brick => brick.Position.CurrentValue.y).ToList();

            if(sortBricks.Count > 1)
            {
                var brick = sortBricks[sortBricks.Count - 2];
                var newPosition = new Vector3(brickEntityProxy.Position.Value.x, brick.Position.CurrentValue.y + 1, 0);
                brickEntityProxy.Position.Value = newPosition;
            }

            
            //TODO - Надо узновать размер объекта;
        }

        private async void TryToThrowOutBrick(int brickEntityId)
        {
            var bricks = _gameStateProvider.GameState.Bricks;
            var brickEntityProxy = bricks.FirstOrDefault(b => b.Id == brickEntityId);

            var position = brickEntityProxy.Position.Value;

            await MoveBrick(brickEntityProxy);

            var isBlackHoleCollisionCheked = _cmd.Process(
                new CmdBlackHoleCollisionCheck(_worldRootBinder.GetBrickBinderCollider(brickEntityProxy.Id)));

            if(isBlackHoleCollisionCheked)
            {
                Debug.Log("Блок уничтожен");
                RemoveBrickViewModel(brickEntityProxy);
                DeleteBuilding(brickEntityProxy.Id);
                return;
                //TODO - сделать сдвиг всех блоков вниз, если был удален верхний блок
            }

            brickEntityProxy.Position.Value = position;

        }
    }
}
