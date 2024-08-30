using System;

namespace Paybook.ServiceLayer.Services
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; set; }
    }
}