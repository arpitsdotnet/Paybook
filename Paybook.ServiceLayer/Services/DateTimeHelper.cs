using System;

namespace Paybook.ServiceLayer.Services
{
    public class DateTimeHelper : IDateTimeProvider
    {
        public DateTime Now { get; set; } = DateTime.Now;
    }
}
