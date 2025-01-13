using Project.Scripts.Game.Gameplay.View.Bricks;
using Project.Scripts.Game.State.cmd;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts.Game.Gameplay.Commands.Handlers
{
    public class CmdBrickCollisionCheckHandler : ICommandHandler<CmdBrickCollisionCheck>
    {
        private readonly float RayLenght;

        public CmdBrickCollisionCheckHandler()
        {
            RayLenght = 20;
        }

        public Task<bool> Handle(CmdBrickCollisionCheck command)
        {
            Vector2 origin = (Vector2)command.Collider.bounds.min - new Vector2(0, 0.2f); 
            origin.x += command.Collider.bounds.size.x / 2; 

            Vector2 direction = Vector2.down;

            RaycastHit2D hit = Physics2D.Raycast(origin, direction, RayLenght);

            if (hit.collider != null)
            {
                if (hit.collider.GetComponent<BrickBinder>() != null)
                {
                    return Task.FromResult(true); 
                }
            }

            return Task.FromResult(false);
        }
    }
}
