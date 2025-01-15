using Project.Scripts.Game.Gameplay.Inputs;
using Project.Scripts.Game.Gameplay.Utils;
using Project.Scripts.Game.State.cmd;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts.Game.Gameplay.Commands.Handlers
{
    public class CmdBrickFollowPointerHandler : ICommandHandler<CmdBrickFollowPointer>
    {
        private readonly IGameplayInput _gameplayInput;
        private readonly CameraSystem _cameraSystem;

        public CmdBrickFollowPointerHandler(IGameplayInput gameplayInput, CameraSystem cameraSystem)
        {
            _cameraSystem = cameraSystem;
            _gameplayInput = gameplayInput;
        }

        async Task<bool> ICommandHandler<CmdBrickFollowPointer>.Handle(CmdBrickFollowPointer command)
        {
            do
            {
                command.BrickEntityProxy.Position.Value = _gameplayInput.Position.CurrentValue;
                await Task.Yield();
            } while (!_gameplayInput.IsEndPointFound);


            bool isPointingToUI = _gameplayInput.IsPointsToUI;
            Vector3 brickPosition = command.BrickEntityProxy.Position.Value;
            float cameraX = _cameraSystem.transform.position.x;
            float maxX = _cameraSystem.GetMaxXPostion();
            float maxY = _cameraSystem.GetMaxYPosition();
            float halfScaleX = command.BrickEntityProxy.Scale.x / 2;

#if UNITY_EDITOR
            if (brickPosition.y > maxY)
            {
                Debug.Log("Блок находится слишком высоко!");
            }

            if (brickPosition.x > maxX)
            {
                Debug.Log("Блок находится за границей правой стороны экрана!");
            }

            if (isPointingToUI)
            {
                Debug.Log("Блок касается UI");
            }

#endif
            if (isPointingToUI ||
                brickPosition.x < cameraX + halfScaleX ||
                brickPosition.y > maxY ||
                brickPosition.x > maxX)
            {
                return false;
            }

            return true;

        }
    }
}

