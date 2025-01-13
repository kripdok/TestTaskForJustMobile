using System.Threading.Tasks;

namespace Project.Scripts.Game.State.cmd
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        public Task<bool> Handle(TCommand command);
    }
   
}
