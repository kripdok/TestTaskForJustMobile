using Project.Scripts.Game.State.cmd;

namespace Project.Scripts.Game.Gameplay.Commands
{
    class CmdCreateBrickState : ICommand
    {
        public readonly string TypeId;

        public CmdCreateBrickState(string typeId)
        {
            TypeId = typeId;
        }
    }
}
