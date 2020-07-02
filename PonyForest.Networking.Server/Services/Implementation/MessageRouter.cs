using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using PonyForestServer.Core.Models.Messages;
using PonyForestServer.Core.Modules;
using PonyForestServer.Core.Modules.Attributes;

namespace PonyForestServer.Core.Services.Implementation
{
    internal class MessageRouter : IMessageRouter
    {
        private readonly IModuleProvider _moduleProvider;
        private readonly List<MessageRoute> _routes;

        public MessageRouter(IModuleProvider moduleProvider)
        {
            _moduleProvider = moduleProvider;
            _routes = new List<MessageRoute>();
            
            BuildRoutes();
        }
        
        public void Route(MessageBase message)
        {
            foreach (MessageRoute route in _routes)
            {
                if (route.Attribute.MessageType == message.GetType())
                {
                    route.Method.DynamicInvoke(message);
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
                        MessageRoute route = new MessageRoute
                        {
                            Attribute = attribute,
                            Method = Delegate.CreateDelegate(Expression.GetActionType(attribute.MessageType), module,
                                method)
                        };

                        _routes.Add(route);
                    }
                }
            }
        }
        
        private struct MessageRoute
        {
            public MessageHandlerAttribute Attribute;
            public Delegate Method;
        }
    }
}