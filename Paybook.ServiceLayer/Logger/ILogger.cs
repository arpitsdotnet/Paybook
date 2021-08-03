using System;

namespace Paybook.ServiceLayer.Logger
{
    public interface ILogger
    {
        string MethodName { get; }
        void LogError(string methodName, Exception ex);
        void LogInformation(string message);
    }
}