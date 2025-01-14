using Project.Scripts.Game.State.Bricks;
using Project.Scripts.Game.State.cmd;

namespace Project.Scripts.Game.Gameplay.Commands
{
    public class CmdBrickFollowPointer : ICommand
    {
        public readonly BrickEntiryProxy BrickEntityProxy;
        public CmdBrickFollowPointer(BrickEntiryProxy brickEntityProxy)
        {
            BrickEntityProxy = brickEntityProxy;
        }
    }
}
