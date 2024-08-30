using System;
using Paybook.ServiceLayer.Services;

namespace Paybook.ServiceLayer.Logger
{
    public class LoggerFactory
    {
        private LoggerFactory() { }
        private static readonly Lazy<ILogger> _Instance = new Lazy<ILogger>(() => new FileLogger(new DateTimeHelper()));
        public static ILogger Instance
        {
            get
            {
                return _Instance.Value;
            }
        }

    }
}
