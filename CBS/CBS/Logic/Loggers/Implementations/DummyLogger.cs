namespace CBS.Logic.Loggers.Implementations
{
    using System;

    public class DummyLogger : ILogger
    {
        public void LogError(string message, Exception exception)
        {
            Console.WriteLine($"Custom message: {message}; Exception message: {exception.Message}");
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}