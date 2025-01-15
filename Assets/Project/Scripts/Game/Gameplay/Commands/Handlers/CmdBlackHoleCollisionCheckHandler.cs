using Project.Scripts.Game.Gameplay.Commands;
using Project.Scripts.Game.Gameplay.Utils;
using Project.Scripts.Game.Gameplay.View.Bricks;
using Project.Scripts.Game.State.cmd;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts.Game.Gameplay.Commands.Handlers
{
    public class CmdBlackHoleCollisionCheckHandler : ICommandHandler<CmdBlackHoleCollisionCheck>
    {
        private readonly float RayLenght;

        public CmdBlackHoleCollisionCheckHandler()
        {
            RayLenght = 20;
        }


        public Task<bool> Handle(CmdBlackHoleCollisionCheck command)
        {
            Vector2 origin = (Vector2)command.Collider.bounds.min - new Vector2(0, 0.2f);
            origin.x += command.Collider.bounds.size.x / 2;

            Vector2 direction = Vector2.down;

            RaycastHit2D hit = Physics2D.Raycast(origin, direction, RayLenght);

            if (hit.collider != null)
            {
                if (hit.collider.GetComponent<BlackHole>() != null)
                {
                    Debug.Log("Блок упал в дыру!");
                    return Task.FromResult(true);
                }
            }

            Debug.Log("Блок не был сброшен в дыру. Он находился не над ней!");
            return Task.FromResult(false);
        }
    }
}
