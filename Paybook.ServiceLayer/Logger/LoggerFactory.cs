using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paybook.ServiceLayer.Logger
{
    public class LoggerFactory
    {
        private LoggerFactory() { }
        private static readonly Lazy<ILogger> _Instance = new Lazy<ILogger>(() => new FileLogger());
        public static ILogger Instance
        {
            get
            {
                return _Instance.Value;
            }
        }

    }
}
