using PonyForestServer.Core.Models.Messages;

namespace PonyForestServer.Core.Modules
{
    public abstract class ServerModule
    {
        public virtual void OnEnabled()
        {
        }

        public virtual void OnDisabled()
        {
        }

        public virtual void OnMessage(ref PlayerMessage message)
        {
            
        }
    }
}