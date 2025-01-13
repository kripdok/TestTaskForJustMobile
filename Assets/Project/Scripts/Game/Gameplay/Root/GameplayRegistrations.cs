using Project.Scripts.Game.Common;
using Project.Scripts.Game.Gameplay.Service.UI;
using Project.Scripts.Game.State;
using Project.Scripts.Game.State.cmd;
using R3;
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
            
            diContainer.Bind<ICommandProcessor>().To<CommandProcessor>().AsCached();
            var cmd = diContainer.Resolve<ICommandProcessor>();
            //TODO - Сделать регистрацию команд хэндлеров, для дальнейшей обработки

            
            diContainer.Bind<Subject<Unit>>().WithId(AppConstants.ENIT_SCENE_REQUEST_TAG).AsCached();
            diContainer.Bind<BrickService>().AsCached(); 

            //Чтобы получить список с блоками, надо зарегистрировать GameState
        }
    }
}
