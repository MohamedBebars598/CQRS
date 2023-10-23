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
        public  void RegisterHandlers()
        {
            var assembly = Assembly.GetCallingAssembly();
            var commandTypes = assembly.GetTypes().Where(type => type.Name.EndsWith("Command"));

            foreach (var commandType in commandTypes)
            {
                var handlerType = GetHandlerTypeForCommand(commandType, assembly);
                if (handlerType != null)
                {
                    _registery.Add(commandType, handlerType);
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

