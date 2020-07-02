using System;

namespace PonyForest.Networking.Api.Tools
{
    public class Logger
    {
        private string _name;
        
        public Logger(string name)
        {
            _name = name;
        }
        
        public void Log(object data, ConsoleColor color)
        {
            Console.Write($"[{DateTime.Now:t}][{_name}] ");
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
            Log(data, ConsoleColor.DarkGreen);
        }
        
        public void LogFailure(object data)
        {
            Log(data, ConsoleColor.DarkRed);
        }
    }
}