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
            dynamic handler = Activator.CreateInstance(handlerType);
            return await handler.HandleAsync((dynamic)command, cancellationToken);
            
        }
    }

    

}
