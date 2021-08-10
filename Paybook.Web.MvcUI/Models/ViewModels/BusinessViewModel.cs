using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paybook.Web.MvcUI.Models.ViewModels
{
    public class BusinessViewModel
    {
        public BusinessModel Business { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
        public IEnumerable<SelectListItem> States { get; set; }
    }
}