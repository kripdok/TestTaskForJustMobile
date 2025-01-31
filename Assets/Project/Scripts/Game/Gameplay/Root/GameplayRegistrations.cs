﻿using Project.Scripts.Game.Common;
using Project.Scripts.Game.Gameplay.Commands.Handlers;
using Project.Scripts.Game.Gameplay.Factories;
using Project.Scripts.Game.Gameplay.Inputs;
using Project.Scripts.Game.Gameplay.ObjectPools;
using Project.Scripts.Game.Gameplay.Root.View;
using Project.Scripts.Game.Gameplay.Service;
using Project.Scripts.Game.Gameplay.Utils;
using Project.Scripts.Game.Gameplay.View.Bricks;
using Project.Scripts.Game.Gameplay.View.UI.Brick;
using Project.Scripts.Game.Settings;
using Project.Scripts.Game.State;
using Project.Scripts.Game.State.cmd;
using Project.Scripts.Utils;
using R3;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Game.Gameplay.Root
{
    public class GameplayRegistrations
    {
        public GameplayRegistrations(DiContainer diContainer, GameplayEnterParams enterParams, WorldGameplayRootBinder worldRootBinder, CameraSystem cameraSystem)
        {
            Register(diContainer, enterParams, worldRootBinder, cameraSystem);
        }

        private void Register(DiContainer diContainer, GameplayEnterParams enterParams, WorldGameplayRootBinder worldRootBinder, CameraSystem cameraSystem)
        {

            var gameplayInput = new GameplayInput(diContainer.Resolve<Coroutines>(), new InputControls());

            var cmd = GetAndRegisteringCommandProcessor(diContainer, gameplayInput, cameraSystem);

            diContainer.Bind<ICommandProcessor>().FromInstance(cmd).AsCached();
            diContainer.Bind<IGameplayInput>().FromInstance(gameplayInput).AsCached();
            diContainer.Bind<BrickService>().AsCached();
            diContainer.Bind<Subject<Unit>>().WithId(AppConstants.ENIT_SCENE_REQUEST_TAG).AsCached();
            diContainer.Bind<WorldGameplayRootBinder>().FromInstance(worldRootBinder).AsCached();
            BindBrickObjectPool(diContainer);
            BindUIBrickfactory(diContainer);
        }

        private CommandProcessor GetAndRegisteringCommandProcessor(DiContainer diContainer, GameplayInput gameplayInput, CameraSystem cameraSystem)
        {
            var cmd = new CommandProcessor();
            cmd.RegisterHandler(new CmdCreateBrickStateHandler(diContainer.Resolve<IGameStateProvider>().GameState, diContainer.Resolve<ISettingsProvider>().GameSettings));
            cmd.RegisterHandler(new CmdBrickFollowPointerHandler(gameplayInput, cameraSystem));
            cmd.RegisterHandler(new CmdColliderIntersectionCheckHandler());
            cmd.RegisterHandler(new CmdBrickCollisionCheckHandler());
            cmd.RegisterHandler(new CmdBlackHoleCollisionCheckHandler());
            cmd.RegisterHandler(new CmdPuttingBrickOnTopOfTheTowerHandler());

            return cmd;
        }

        private void BindBrickObjectPool(DiContainer diContainer)
        {
            var prefabBrickPath = $"Prefabs/Gameplay/Bricks/Brick";
            var brickPrefab = Resources.Load<BrickBinder>(prefabBrickPath);
            diContainer.BindMemoryPool<BrickBinder, BrickBinderObjectPool>().FromComponentInNewPrefab(brickPrefab).UnderTransformGroup("Bricks").AsCached();
        }

        private void BindUIBrickfactory(DiContainer diContainer)
        {
            var uiBrickBinderPrefab = Resources.Load<UIBrickBinder>("Prefabs/UI/UIBrick");
            diContainer.BindFactory<UIBrickBinder,UIBrickBinderFactory>().FromComponentInNewPrefab(uiBrickBinderPrefab).AsCached();
        }
    }
}
