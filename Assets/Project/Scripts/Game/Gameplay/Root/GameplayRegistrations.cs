using Zenject;

namespace Project.Scripts.Game.Gameplay.Root
{
    public class GameplayRegistrations
    {

        public GameplayRegistrations(DiContainer diContainer, GameplayEnterParams enterParams)
        {
            Register(diContainer, enterParams);
        }

        private void Register(DiContainer diContainer, GameplayEnterParams enterParams)
        {
            
        }
    }
}
