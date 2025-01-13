using UnityEngine;

namespace Project.Scripts.Game.Gameplay.Commands
{
    public class CmdColliderIntersectionCheck : CmdCollisionCheck
    {
        public CmdColliderIntersectionCheck(Collider2D collider) : base(collider)
        {
        }
    }
}
