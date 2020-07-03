using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using PonyForestServer.Core.Models.Messages;
using PonyForestServer.Core.Modules;
using PonyForestServer.Core.Modules.Attributes;
using PonyForestServer.Core.Tools;

namespace PonyForestServer.Core.Services.Implementation
{
    internal class MessageRouter : IMessageRouter
    {
        private readonly IModuleProvider _moduleProvider;
        private readonly Logger _logger;
        private readonly IWorld _world;
        private readonly List<MessageRoute> _routes;

        public MessageRouter(IModuleProvider moduleProvider, ILoggerProvider loggerProvider, IWorld world)
        {
            _moduleProvider = moduleProvider;
            _logger = loggerProvider.GetLogger("Server");
            _world = world;
            
            _routes = new List<MessageRoute>();
            BuildRoutes();
        }
        
        public void Route(PlayerMessage message)
        {
            foreach (ServerModule module in _moduleProvider.Modules)
            {
                module.OnMessage(ref message);
            }

            Type messageType = message.GetType();
            
            foreach (MessageRoute route in _routes)
            {
                if (route.Attribute.MessageType == messageType)
                {
                    object result = route.Method.DynamicInvoke(message);

                    if (result is PlayerMessage newMessage)
                    {
                        message = newMessage;
                    }
                }
            }
        }

        private void BuildRoutes()
        {
            foreach (ServerModule module in _moduleProvider.Modules)
            {
                List<MethodInfo> methods = module
                    .GetType()
                    .GetMethods()
                    .Where(m => m.GetCustomAttributes(typeof(MessageHandlerAttribute)).Any())
                    .ToList();

                foreach (MethodInfo method in methods)
                {
                    if (method.GetCustomAttribute(typeof(MessageHandlerAttribute)) is MessageHandlerAttribute attribute)
                    {
                        Func<Type[], Type> getType;
                        bool isAction = method.ReturnType == typeof(void);
                        List<Type> types = new List<Type>
                        {
                            attribute.MessageType
                        };

                        if (isAction)
                        {
                            getType = Expression.GetActionType;
                        }
                        else
                        {
                            getType = Expression.GetFuncType;
                            types.Add(method.ReturnType);
                        }

                        MessageRoute route = new MessageRoute
                        {
                            Attribute = attribute,
                            Module = module,
                            Method = Delegate.CreateDelegate(getType(types.ToArray()), module, method)
                        };

                        _routes.Add(route);
                    }
                }
            }
            
            _logger.LogInformation("Routes was built");
        }
        
        private struct MessageRoute
        {
            public MessageHandlerAttribute Attribute;
            public ServerModule Module;
            public Delegate Method;
        }
    }
}