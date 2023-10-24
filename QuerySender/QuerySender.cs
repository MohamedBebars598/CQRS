using CQRSBebars.ComandHandlers;
using CQRSBebars.QuerySender;
using CQRSBebars.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRSBebars.QueryHandler;

namespace CQRSBebars.QuerySender
{
    internal class QuerySender : IQuerySender
    {

        private readonly QueryHandlerRegisteration _registeryHandler;

        public QuerySender(QueryHandlerRegisteration registeryHandler)
        {
            _registeryHandler = registeryHandler;
        }

        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default)
        {
            Type queryType = query.GetType();
            Type handlerType = _registeryHandler[queryType];
            var handler = Activator.CreateInstance(handlerType) as IQueryHandler<IQuery<TResult>, TResult>;
            if (handler == null)
                throw new InvalidOperationException($"No handler registered for Query type {queryType}");
            return await handler.HandleAsync((dynamic)query, cancellationToken);
        }
    }
}
