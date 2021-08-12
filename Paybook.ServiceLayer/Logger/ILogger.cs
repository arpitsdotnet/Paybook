using System;

namespace Paybook.ServiceLayer.Logger
{
    public interface ILogger
    {
        string GetMethodName();
        void Error(string methodName, Exception ex);
        void Info(string message);
    }
}