using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paybook.ServiceLayer.Models
{
    public class LoginModel
    {
        public string ID { get; set; }
        public int IsActive { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string IsSucceeded { get; set; }
        public List<string> Messages { get; set; }
    }
}
