using CQRSBebars.ComandHandlers;

namespace CQRSBebars.CommandSender
{
    public class CommandSender : ICommandSender
    {

       private readonly CommandHandlerRegistration _registeryHandler;

        public CommandSender(CommandHandlerRegistration registeryHandler)
        {
            _registeryHandler = registeryHandler;
        }

        async Task<TResult> ICommandSender.SendAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken)
        {
            Type commandType = command.GetType();
            Type handlerType = _registeryHandler[commandType];
            var handler = Activator.CreateInstance(handlerType) as ICommandHandler<TCommand,TResult>;
            if (handler == null)
                throw new InvalidOperationException($"No handler registered for command type {commandType}");
            return await handler.HandleAsync((dynamic)command, cancellationToken);
        }
    }

    

}
