using System.Collections.Generic;
using System.Web.Mvc;

namespace Paybook.ServiceLayer.Models.ViewModels
{
    public class BusinessViewModel
    {
        public BusinessModel Business { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
        public IEnumerable<SelectListItem> States { get; set; }
    }
}