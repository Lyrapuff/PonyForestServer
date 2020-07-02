using PonyForest.Networking.Common.Messages;

namespace PonyForest.Networking.Common
{
    public interface IMessageHandler
    {
        void Handle(ref MessageBase message);
    }
    
    public class MessageHandler<T> : IMessageHandler where T : MessageBase
    {
        public void Handle(ref MessageBase messageBase)
        {
            T message = messageBase as T;
            
            if (!messageBase.Canceled)
            {
                OnExecuted(ref message);
            }
            else
            {
                OnCanceled(ref message);
            }
        }

        protected virtual void OnExecuted(ref T message)
        {
            
        }

        protected virtual void OnCanceled(ref T message)
        {
            
        }
    }
}