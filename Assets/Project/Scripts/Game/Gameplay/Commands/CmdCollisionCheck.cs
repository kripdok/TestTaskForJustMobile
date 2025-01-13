using Project.Scripts.Game.State.cmd;
using UnityEngine;

namespace Project.Scripts.Game.Gameplay.Commands
{
    public abstract class CmdCollisionCheck : ICommand
    {
        public readonly Collider2D Collider;

        public CmdCollisionCheck(Collider2D collider)
        {
            Collider = collider;
        }
    }
}
