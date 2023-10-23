using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CQRSBebars.CommandSender
{
    public  class CommandHandlerRegistration
    {
        private readonly Dictionary<Type, Type> _registery=new Dictionary<Type, Type>();
        public Type this[Type commandType]
        {
            get
            {
                if (_registery.TryGetValue(commandType, out Type handlerType))
                {
                    return handlerType;
                }

                throw new InvalidOperationException($"No handler registered for command type {commandType}");
            }
        }

        private  void RegisterHandlers()
        {
            var assembly = Assembly.GetCallingAssembly();
            var commandTypes = assembly.GetTypes().Where(type => type.Name.EndsWith("Command"));

            foreach (var commandType in commandTypes)
            {
                var handlerType = GetHandlerTypeForCommand(commandType, assembly);
                if (handlerType != null)
                {
                   var isAdded= _registery.TryAdd(commandType, handlerType);
                    if (!isAdded)
                        throw new InvalidOperationException("each command has only one handler");
                }
            }
        }

        

        private Type GetHandlerTypeForCommand(Type commandType, Assembly assembly)
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
                        if (genericTypeArguments.Contains(commandType))
                            return type;
                    }

                }
            }
            return null;
        }
    }

}

