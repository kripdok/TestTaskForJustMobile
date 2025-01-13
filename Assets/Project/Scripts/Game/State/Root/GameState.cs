
using Project.Scripts.Game.State.Bricks;
using System;
using System.Collections.Generic;

namespace Project.Scripts.Game.State.Root
{
    [Serializable]
    public class GameState
    {
        public int GlobalEmtityID;
        public List<BrickEntity> Bricks;

        public int CreateEntityId() => GlobalEmtityID++;
    }
}
