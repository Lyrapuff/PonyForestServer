using PonyForest.Networking.Api.Tools;
using PonyForest.Networking.Common;
using PonyForest.Networking.Common.Messages;

namespace TestModule
{
    public class TestMessageHandler : MessageHandler<PlayerPositionMessage>
    {
        private Logger _logger;

        public TestMessageHandler()
        {
            _logger = new Logger("TestModule");
        }
        
        protected override void OnExecuted(ref PlayerPositionMessage message)
        {
            _logger.LogInformation($"new position: {message.Position}, cancel");
            message.Canceled = true;
        }
    }
}