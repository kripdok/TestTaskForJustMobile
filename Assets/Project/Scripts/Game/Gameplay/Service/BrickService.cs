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


           // _gameStateProvider.SaveGameState(); TODO - надо придумать когда сохранять игру

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

        private async void MoveBrickAndCollisionCheck(BrickEntityProxy brickEntityProxy)
        {
            var isComplite = await MoveBrick(brickEntityProxy);

            if (isComplite == false)
            {
                await WaitForBlockRemoveAnimation(brickEntityProxy);
                return;
            }

            TryPuttingBrickOnTopOfTheTower(brickEntityProxy);
        }

        private async Task<bool> MoveBrick(BrickEntityProxy brickEntityProxy)
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

        private void CreateBrickViewModel(BrickEntityProxy brickEntityProxy)
        {
            var brickViewModel = new BrickViewModel(brickEntityProxy);
            brickViewModel.OnStartHold.Subscribe(TryToThrowOutBrick);
            _allBricks.Add(brickViewModel);
            _bricksMap[brickEntityProxy.Id] = brickViewModel;

            Debug.Log("Создан новый блок!");
        }


        private void RemoveBrickViewModel(BrickEntityProxy brickEntityProxy)
        {
            DeleteBrick(brickEntityProxy.Id);

            if (_bricksMap.TryGetValue(brickEntityProxy.Id, out var vieModel))
            {
                vieModel.OnStartHold.Dispose();
                _allBricks.Remove(vieModel);
            }

            
            Debug.Log("Блок уничтожен");
        }

        private async void TryPuttingBrickOnTopOfTheTower(BrickEntityProxy brickEntityProxy)
        {
            var isColliderIntersection = _cmd.Process(
                new CmdColliderIntersectionCheck(_worldRootBinder.GetBrickBinderCollider(brickEntityProxy.Id)));

            var isBrickCollisionCheked = _cmd.Process(
                new CmdBrickCollisionCheck(_worldRootBinder.GetBrickBinderCollider(brickEntityProxy.Id)));

            if (!isColliderIntersection || !isBrickCollisionCheked)
            {
                if (_allBricks.Count > 1)
                {
                    await WaitForBlockRemoveAnimation(brickEntityProxy);

                    return;
                }
            }

            var SortedBricksByHeight = _allBricks.OrderBy(brick => brick.Position.CurrentValue.y).ToList();

            if (SortedBricksByHeight.Count > 1)
            {
                var brick = SortedBricksByHeight[SortedBricksByHeight.Count - 2];
                var newPosition = GetNewPositionForBrick(brickEntityProxy, brick.Position.CurrentValue, brickEntityProxy.Scale);

                var brickViewModel = _bricksMap.Values.First(brick => brick.BrickEntityId == brickEntityProxy.Id);
                brickViewModel.PlayMoveToTopPositionAnimation(newPosition);

                await WaitForTheAnimationPlay(brickViewModel);
                brickEntityProxy.Position.Value = newPosition;
            }
        }

        private Vector3 GetNewPositionForBrick(BrickEntityProxy brickThatIsPlaced, Vector3 topBrickPosition, Vector3 topBrickScale)
        {
            var newXPosition = Random.Range(topBrickPosition.x - topBrickScale.x / 2, topBrickPosition.x + topBrickScale.x / 2);
            var newYPosition = topBrickPosition.y + brickThatIsPlaced.Scale.y;

            return new Vector3(newXPosition, newYPosition, 0);
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

                DropBrickIntoBlackHole(brickEntityProxy);

                if (!isBrickJustRemoved)
                {

                    List<BrickEntityProxy> LoweredBricks = new List<BrickEntityProxy>();

                    foreach (var brick in SortedBricksByHeight)
                    {
                        if (brick.Id != brickEntityId)
                        {
                            LoweredBricks.Add(brick);
                        }
                    }

                    LowerAllBrick(LoweredBricks);
                }
            }
            else
            {
                brickEntityProxy.Position.Value = oldPosition;
            }


        }

        private async Task WaitForBlockRemoveAnimation(BrickEntityProxy brickEntityProxy)
        {
            var brickViewModel = _bricksMap.Values.First(brick => brick.BrickEntityId == brickEntityProxy.Id);

            brickViewModel.PlayDeathAnimation();
            await WaitForTheAnimationPlay(brickViewModel);

            RemoveBrickViewModel(brickEntityProxy);
        }

        private async void DropBrickIntoBlackHole(BrickEntityProxy brickEntityProxy)
        {
            var brickViewModel = _bricksMap.Values.First(brick => brick.BrickEntityId == brickEntityProxy.Id);

            brickViewModel.PlayAnimationOfFallingIntoBlackHole(new Vector3(brickViewModel.Position.CurrentValue.x, -10, 0)); //TODO - надо получать это значение?
            await WaitForTheAnimationPlay(brickViewModel);

            RemoveBrickViewModel(brickEntityProxy);
        }

        private void LowerAllBrick(List<BrickEntityProxy> bricks)
        {
            var SortedBricksByHeight = bricks.OrderBy(brick => brick.Position.CurrentValue.y).ToList();


            for (var i = 1; i < SortedBricksByHeight.Count; i++)
            {
                SortedBricksByHeight[i].Position.Value = new Vector3(SortedBricksByHeight[i].Position.Value.x,
                    SortedBricksByHeight[i - 1].Position.Value.y + SortedBricksByHeight[i - 1].Scale.y);

            }
        }

        private async Task WaitForTheAnimationPlay(BrickViewModel brickViewModel)
        {
            while (brickViewModel.IsAnimationPlayed == false)
            {
                await Task.Yield();
            }
        }
    }
}
