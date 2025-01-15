using ObservableCollections;
using Project.Scripts.Game.Gameplay.View.Bricks;
using R3;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Game.Gameplay.Root.View
{
    public class WorldGameplayRootBinder : MonoBehaviour
    {
        private WorldGameplayRootViewModel _viewModel;

        private readonly Dictionary<int, BrickBinder> _createdBuildingsMap = new();
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public void Bind(WorldGameplayRootViewModel viewMode)
        {
            _viewModel = viewMode;

            foreach (var brickViewModel in _viewModel.AllBricks)
            {
                CreateBuilding(brickViewModel);
            }

            _disposables.Add(_viewModel.AllBricks.ObserveAdd().Subscribe(e => CreateBuilding(e.Value)));
            _disposables.Add(_viewModel.AllBricks.ObserveRemove().Subscribe(e => DestroyBuilding(e.Value)));
        }

        public Collider2D GetBrickBinderCollider(int entityId)
        {
            if (_createdBuildingsMap.TryGetValue(entityId, out var Brick))
            {
                return Brick.Collider;
            }

            throw new ArgumentException($"Brick with id {entityId} does not exist");
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        private void CreateBuilding(BrickViewModel buildingViewModel)
        {
            var createBrick = _viewModel.ObjectPool.Spawn(buildingViewModel);
            createBrick.Bind(buildingViewModel);
            _createdBuildingsMap[buildingViewModel.BrickEntityId] = createBrick;
        }

        private void DestroyBuilding(BrickViewModel buildingViewModel)
        {
            if (_createdBuildingsMap.TryGetValue(buildingViewModel.BrickEntityId, out var briclBinder))
            {
                _viewModel.ObjectPool.Despawn(briclBinder);
                _createdBuildingsMap.Remove(buildingViewModel.BrickEntityId);
            }
        }

    }
}
