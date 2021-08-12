using Paybook.BusinessLayer.Invoice;
using Paybook.ServiceLayer.Constants;
using Paybook.ServiceLayer.Models;
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
        private readonly InvoiceProcessor _invoice;

        public InvoiceController(InvoiceProcessor invoice)
        {
            _invoice = invoice;
        }
        public ActionResult Index()
        {
            int? businessId = GetSelectedBusinessId();

            if (businessId == null)
                return RedirectToAction("Create", "Business");

            List<InvoiceModel> model = _invoice.GetAllByPage(businessId.Value, 0, "", "");


            return View(model);
        }





        [NonAction]
        private int? GetSelectedBusinessId()
        {
            return (int?)TempData.Peek(TempdataNames.SelectedBusinessId);
        }
    }
}