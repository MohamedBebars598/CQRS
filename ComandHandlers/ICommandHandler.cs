using CQRSBebars.Comand;

namespace CQRSBebars.ComandHandlers
{
    public interface ICommandHandler<TCommand,TResult> where TCommand : class, ICommand
    {
        public Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken = default);


    }
}
