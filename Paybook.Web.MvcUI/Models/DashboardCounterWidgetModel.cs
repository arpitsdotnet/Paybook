using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paybook.Web.MvcUI.Models
{
    public class DashboardCounterWidgetModel
    {
        public string BgColorClass { get; set; }
        public string BgColorHoverClass { get; set; }
        public string RsSymbolColor { get; set; }
        public decimal Total { get; set; }
        public int Count { get; set; }
        public string CountText { get; set; }
    }
}