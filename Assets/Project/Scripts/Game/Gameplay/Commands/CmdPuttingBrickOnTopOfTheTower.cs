﻿using Project.Scripts.Game.State.Bricks;
using Project.Scripts.Game.State.cmd;
using UnityEngine;

namespace Project.Scripts.Game.Gameplay.Commands
{
    public class CmdPuttingBrickOnTopOfTheTower: ICommand
    {
        public readonly BrickEntityProxy BrickThatIsPlaced;
        public readonly Vector3 TopBrickPosition;
        public readonly Vector3 TopBrickScale;

        public CmdPuttingBrickOnTopOfTheTower(BrickEntityProxy brickThatIsPlaced, Vector3 topBrickLocation, Vector3 topBrickScale)
        {
            BrickThatIsPlaced = brickThatIsPlaced;
            TopBrickPosition = topBrickLocation;
            TopBrickScale = topBrickScale;
        }
    }
}
