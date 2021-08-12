using Paybook.ServiceLayer.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paybook.Web.MvcUI.Areas.Chief.Controllers
{
    [RouteArea("Chief")]

    public class InvoiceController : Controller
    {
        public ActionResult Index()
        {
            //int? businessId = GetSelectedBusinessId();

            //if (businessId == null)
                //return RedirectToAction("Create", "Business");

            //List<ClientModel> model = _client.GetAllByPage(businessId.Value, 0, "", "");


            return View();
        }





        [NonAction]
        private int? GetSelectedBusinessId()
        {
            return (int?)TempData.Peek(TempdataNames.SelectedBusinessId);
        }
    }
}