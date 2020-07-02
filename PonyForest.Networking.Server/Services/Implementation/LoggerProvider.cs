using System.Collections.Generic;
using System.Linq;
using PonyForestServer.Core.Tools;

namespace PonyForestServer.Core.Services.Implementation
{
    public class LoggerProvider : ILoggerProvider
    {
        private readonly List<Logger> _loggers = new List<Logger>();
        
        public Logger GetLogger(string name)
        {
            Logger logger = _loggers.FirstOrDefault(log => log.Name == name);
            
            if (logger == null)
            {
                logger = new Logger(name);
                _loggers.Add(logger);
            }

            return logger;
        }
    }
}