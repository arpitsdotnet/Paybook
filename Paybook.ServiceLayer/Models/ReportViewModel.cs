using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paybook.ServiceLayer.Models
{
    public class ReportViewModel
    {
        public string Type { get; set; }
        public DateTime? Date { get; set; }
        public string Particular { get; set; }
        public string CustomerName { get; set; }
        public string WorkType { get; set; }
        public string EntityType { get; set; }
        public double BasicAmount { get; set; }
        public int Tax { get; set; }
        public double TaxAmount => BasicAmount * Tax / 100;
        public double Amount => BasicAmount + TaxAmount;
    }
}
