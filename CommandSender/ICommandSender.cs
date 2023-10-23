using CQRSBebars.Comand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSBebars.CommandSender
{
    public interface ICommandSender
    {
        Task<TResult> SendAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken) where TCommand : class, ICommand;

    }
}
