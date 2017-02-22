namespace CBS.Logic.Loggers
{
    using System;

    public interface ILogger
    {
        void LogError(string message, Exception exception);

        void Log(string message);
    }
}