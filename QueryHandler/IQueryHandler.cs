using CQRSBebars.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSBebars.QueryHandler
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery : class, IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
    }
}
