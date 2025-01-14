using ObservableCollections;
using Project.Scripts.Game.Gameplay.Commands;
using Project.Scripts.Game.Gameplay.Root.View;
using Project.Scripts.Game.Gameplay.View.Bricks;
using Project.Scripts.Game.State;
using Project.Scripts.Game.State.Bricks;
using Project.Scripts.Game.State.cmd;
using R3;
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
            var isComplite = await MoveBrick(brickEntityProxy);

            if (isComplite == false)
            {
                RemoveBrickViewModel(brickEntityProxy);
                return;
            } 

            TryPuttingBrickOnTopOfTheTower(brickEntityProxy);
            _gameStateProvider.SaveGameState();
        }

        private async Task<bool> MoveBrick(BrickEntiryProxy brickEntityProxy)
        {
            return await _cmd.AsuncProcess(new CmdBrickFollowPointer(brickEntityProxy));
        }

        private bool DeleteBrick(int brickEntityId)
        {
            var bricks = _gameStateProvider.GameState.Bricks;
            var brick = bricks.FirstOrDefault(b => b.Id == brickEntityId);

            if (brick != null)
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
            DeleteBrick(brickEntityProxy.Id);

            if (_bricksMap.TryGetValue(brickEntityProxy.Id, out var vieModel))
            {
                vieModel.OnStartHold.Dispose();
                _allBricks.Remove(vieModel);
            }

            Debug.Log("Блок уничтожен");
        }

        private void TryPuttingBrickOnTopOfTheTower(BrickEntiryProxy brickEntityProxy)
        {
            var isColliderIntersection = _cmd.Process(
                new CmdColliderIntersectionCheck(_worldRootBinder.GetBrickBinderCollider(brickEntityProxy.Id)));

            var isBrickCollisionCheked = _cmd.Process(
                new CmdBrickCollisionCheck(_worldRootBinder.GetBrickBinderCollider(brickEntityProxy.Id)));

            if (!isColliderIntersection || !isBrickCollisionCheked)
            {
                if (_allBricks.Count > 1)
                {
                    RemoveBrickViewModel(brickEntityProxy);

                    return;
                }
            }

            var SortedBricksByHeight = _allBricks.OrderBy(brick => brick.Position.CurrentValue.y).ToList();

            if (SortedBricksByHeight.Count > 1)
            {
                var brick = SortedBricksByHeight[SortedBricksByHeight.Count - 2];
                _cmd.Process(new CmdPuttingBrickOnTopOfTheTower(brickEntityProxy, brick.Position.CurrentValue, brickEntityProxy.Scale));
            }
        }

        private async void TryToThrowOutBrick(int brickEntityId)
        {
            var bricks = _gameStateProvider.GameState.Bricks;

            var SortedBricksByHeight = bricks.OrderBy(brick => brick.Position.CurrentValue.y).ToList();
            var brickEntityProxy = SortedBricksByHeight.FirstOrDefault(b => b.Id == brickEntityId);

            var oldPosition = brickEntityProxy.Position.Value;

            await MoveBrick(brickEntityProxy);
            var isBlackHoleCollisionCheked = _cmd.Process(
                new CmdBlackHoleCollisionCheck(_worldRootBinder.GetBrickBinderCollider(brickEntityProxy.Id)));

            if (isBlackHoleCollisionCheked)
            {
                bool isBrickJustRemoved = SortedBricksByHeight.First() == brickEntityProxy || SortedBricksByHeight.Last() == brickEntityProxy;

                RemoveBrickViewModel(brickEntityProxy);

                if (!isBrickJustRemoved)
                {
                    LowerAllBrick();
                }
            }
            else
            {
                brickEntityProxy.Position.Value = oldPosition;
            }

            _gameStateProvider.SaveGameState();
        }

        private void LowerAllBrick()
        {
            var bricks = _gameStateProvider.GameState.Bricks;
            var SortedBricksByHeight = bricks.OrderBy(brick => brick.Position.CurrentValue.y).ToList();


            for (var i = 1; i < SortedBricksByHeight.Count; i++)
            {
                SortedBricksByHeight[i].Position.Value = new Vector3(SortedBricksByHeight[i].Position.Value.x,
                    SortedBricksByHeight[i - 1].Position.Value.y + SortedBricksByHeight[i - 1].Scale.y);

            }
        }
    }
}
