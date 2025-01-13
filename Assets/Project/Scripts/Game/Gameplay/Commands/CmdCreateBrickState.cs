using Project.Scripts.Game.State.cmd;

namespace Project.Scripts.Game.Gameplay.Commands
{
    class CmdCreateBrickState : ICommand
    {
        //сюда надо вставлять данные для создания нового BrickEtity
        public readonly string TypeId;

        public CmdCreateBrickState(string typeId)
        {
            TypeId = typeId;
        }
    }
}
