
using System;

namespace Project.Scripts.Game.State.Root
{
    [Serializable]
    public class GameState
    {
        public int GlobalEmtityID;

        public int CreateEntityId() => GlobalEmtityID++;
    }
}
