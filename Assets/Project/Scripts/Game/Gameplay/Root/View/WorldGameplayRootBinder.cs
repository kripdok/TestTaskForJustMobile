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
        private readonly Dictionary<int, BrickBinder> _createdBuildingsMap = new();

        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        private WorldGameplayRootViewModel _viewModel;

        public void Bind(WorldGameplayRootViewModel viewMode)
        {
            _viewModel = viewMode;

            foreach(var brickViewModel in _viewModel.AllBuildongs)
            {
                CreateBuilding(brickViewModel);
            }

            _disposables.Add(_viewModel.AllBuildongs.ObserveAdd().Subscribe(e => CreateBuilding(e.Value)));
            _disposables.Add(_viewModel.AllBuildongs.ObserveRemove().Subscribe(e => DestroyBuilding(e.Value)));
        }

        public Collider2D GetBrickBinderCollider(int entityId)
        {
            if(_createdBuildingsMap.TryGetValue(entityId, out var Brick))
            {
                return Brick.Collider;
            }

            throw new ArgumentException($"brick with id {entityId} does not exist");
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        private void CreateBuilding(BrickViewModel buildingViewModel)
        {
            var brickType = buildingViewModel.TypeId;
            var prefabBrickPath = $"Prefabs/Gameplay/Bricks/Brick";
            var brickPrefab = Resources.Load<BrickBinder>(prefabBrickPath);

            var createBrick = Instantiate(brickPrefab);
            createBrick.Bind(buildingViewModel);
            _createdBuildingsMap[buildingViewModel.BrickEntityId] = createBrick;

            //TODO - здесь нужен poolObject
        }
        private void DestroyBuilding(BrickViewModel buildingViewModel)
        {
            if(_createdBuildingsMap.TryGetValue(buildingViewModel.BrickEntityId, out var briclBinder))
            {
                Destroy(briclBinder.gameObject);
                _createdBuildingsMap.Remove(buildingViewModel.BrickEntityId);
            }
        }

    }
}
