using UnityEngine;

namespace Project.Scripts.Game.Gameplay.Commands
{
    public class CmdBrickCollisionCheck : CmdCollisionCheck
    {
        public CmdBrickCollisionCheck(Collider2D collider) : base(collider)
        {
        }
    }
}
