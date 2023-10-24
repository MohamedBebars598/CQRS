using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CQRSBebars.QuerySender
{
    public class QueryHandlerRegisteration
    {

        private readonly Dictionary<Type, Type> _registery = new Dictionary<Type, Type>();
        public Type this[Type QueryType]
        {
            get
            {
                if (_registery.TryGetValue(QueryType, out Type handlerType))
                {
                    return handlerType;
                }

                throw new InvalidOperationException($"No handler registered for Query type {QueryType}");
            }
        }

        private void RegisterHandlers()
        {
            var assembly = Assembly.GetCallingAssembly();
            var QueryTypes = assembly.GetTypes().Where(type => type.Name.EndsWith("Query"));

            foreach (var QueryType in QueryTypes)
            {
                var handlerType = GetHandlerTypeForQuery(QueryType, assembly);
                if (handlerType != null)
                {
                    var isAdded = _registery.TryAdd(QueryType, handlerType);
                    if (!isAdded)
                        throw new InvalidOperationException("each Query has only one handler");
                }
            }
        }



        private Type GetHandlerTypeForQuery(Type QueryType, Assembly assembly)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsGenericType)
                {
                    var genericTypeArguments = type.GetGenericArguments();
                    if (genericTypeArguments.Length < 2)
                        continue;
                    else
                    {
                        if (genericTypeArguments.Contains(QueryType))
                            return type;
                    }

                }
            }
            return null;
        }
    }

}
