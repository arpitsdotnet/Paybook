using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Paybook.ServiceLayer.Models
{
    public class BaseResultStatusModel
    {
        [NotMapped]
        public bool IsSucceeded { get; set; } = false;
        [NotMapped]
        public string ReturnMessage { get; set; }
    }
}
