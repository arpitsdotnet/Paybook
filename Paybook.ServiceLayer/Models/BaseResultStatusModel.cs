using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paybook.ServiceLayer.Models
{
    public class BaseResultStatusModel
    {
        public bool IsSucceeded { get; set; }
        public string ReturnMessage { get; set; }
    }
}
