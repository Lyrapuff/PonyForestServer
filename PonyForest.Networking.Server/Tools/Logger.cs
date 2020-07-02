using System;

namespace PonyForestServer.Core.Tools
{
    public class Logger
    {
        public string Name { get; }

        public Logger(string name)
        {
            Name = name;
        }
        
        public void Log(object data, ConsoleColor color)
        {
            Console.Write($"[{DateTime.Now:t}][{Name}] ");
            Console.ForegroundColor = color;
            Console.WriteLine(data);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void LogInformation(object data)
        {
            Log(data, ConsoleColor.Gray);
        }
        
        public void LogWarning(object data)
        {
            Log(data, ConsoleColor.DarkYellow);
        }
        
        public void LogSuccess(object data)
        {
            Log(data, ConsoleColor.Green);
        }
        
        public void LogFailure(object data)
        {
            Log(data, ConsoleColor.DarkRed);
        }
        
        public void LogDebug(object data)
        {
            Log(data, ConsoleColor.Cyan);
        }
    }
}