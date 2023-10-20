using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSBebars.ComandHandlers
{
    public interface ICommandHandler<TCommand,TResult>
    {
        public Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken = default);


    }
}
