using System.Threading.Tasks;

namespace Project.Scripts.Game.State.cmd
{
    public interface ICommandProcessor
    {
        public void RegisterHandler<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand;
        public Task<bool> AsuncProcess<TCommand>(TCommand command) where TCommand : ICommand;
        public bool Process<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
