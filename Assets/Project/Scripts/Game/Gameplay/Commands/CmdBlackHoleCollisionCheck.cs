using UnityEngine;

namespace Project.Scripts.Game.Gameplay.Commands
{
    public class CmdBlackHoleCollisionCheck : CmdCollisionCheck
    {
        public CmdBlackHoleCollisionCheck(Collider2D collider) : base(collider)
        {
        }
    }
}
