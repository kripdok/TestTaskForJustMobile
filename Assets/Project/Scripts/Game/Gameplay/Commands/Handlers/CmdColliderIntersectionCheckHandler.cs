using Project.Scripts.Game.State.cmd;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts.Game.Gameplay.Commands.Handlers
{
    public class CmdColliderIntersectionCheckHandler : ICommandHandler<CmdColliderIntersectionCheck>
    {
        
        public Task<bool> Handle(CmdColliderIntersectionCheck command)
        {
            var colliders = new List<Collider2D>();
            var overlapCount = command.Collider.OverlapCollider(new ContactFilter2D(), colliders);

            if(overlapCount > 0)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }
    }
}
