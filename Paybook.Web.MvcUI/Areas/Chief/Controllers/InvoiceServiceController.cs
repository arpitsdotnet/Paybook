using Newtonsoft.Json;
using Paybook.BusinessLayer.Abstracts.Admins;
using Paybook.BusinessLayer.Abstracts.Invoices;
using Paybook.ServiceLayer.Constants;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Models.Invoices;
using Paybook.ServiceLayer.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paybook.Web.MvcUI.Areas.Chief.Controllers
{
    [RouteArea("Chief")]
    [Authorize]
    public class InvoiceServiceController : Controller
    {
        private readonly IInvoiceServiceProcessor _serviceProcessor;
        private readonly ICategoryProcessor _categoryProcessor;

        public InvoiceServiceController(
            IInvoiceServiceProcessor service,
            ICategoryProcessor category)
        {
            _serviceProcessor = service;
            _categoryProcessor = category;
        }

        private int BusinessId { get { return Convert.ToInt32(Request.Cookies[CookieNames.SelectedBusinessId].Value); } }



        public ActionResult Index()
        {
            List<InvoiceServiceModel> servicesData = GetServices();

            //if (Session[CookieNames.InvoiceServices] == null)
            //    servicesData = new List<InvoiceServiceModel>();
            //else
            //    servicesData = (List<InvoiceServiceModel>)Session[CookieNames.InvoiceServices];

            if (servicesData != null && servicesData.Count() > 0)
            {
                // ADD WORK TYPE FOR EACH ITEM
                foreach (var item in servicesData)
                {
                    item.WorkTypeCategoryMaster = _categoryProcessor.GetById(BusinessId, item.WorkTypeId);
                }
            }

            return PartialView("_InvoiceServicesTablePartial", servicesData);
        }

        [HttpGet]
        public ActionResult Create(int? invoiceId)
        {
            var workTypes = _categoryProcessor.GetAllByTypeCore(BusinessId, "WorkTypes");
            var taxTypes = _categoryProcessor.GetAllByTypeCore(BusinessId, "TaxTypes");

            var serviceVM = new InvoiceServiceViewModel
            {
                Service = new InvoiceServiceModel
                {
                    Qty = 1
                },
                WorkTypes = GetSelectListItemsCategory(workTypes),
                TaxTypes = GetSelectListItemsCategory(taxTypes)
            };

            if (invoiceId != null)
            {
                serviceVM.Service.InvoiceId = invoiceId.Value;
            }

            return PartialView("_InvoiceServiceFormPartial", serviceVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(InvoiceServiceViewModel modelVM)
        {
            var workTypes = _categoryProcessor.GetAllByTypeCore(BusinessId, "WorkTypes");
            var taxTypes = _categoryProcessor.GetAllByTypeCore(BusinessId, "TaxTypes");

            InvoiceServiceViewModel output = new InvoiceServiceViewModel
            {
                Service = new InvoiceServiceModel
                {
                    IsSucceeded = false,
                    ReturnMessage = "Due to technical issue, action could not be performed, please try again."
                },
                WorkTypes = GetSelectListItemsCategory(workTypes),
                TaxTypes = GetSelectListItemsCategory(taxTypes)
            };

            if (ModelState.IsValid)
            {
                List<InvoiceServiceModel> serviceData = GetServices();

                //if (Session[CookieNames.InvoiceServices] == null)
                //    servicesData = new List<InvoiceServiceModel>();
                //else
                //    servicesData = (List<InvoiceServiceModel>)Session[CookieNames.InvoiceServices];

                int orderBy = serviceData.Count();

                modelVM.Service.BusinessId = BusinessId;
                modelVM.Service.CreateBy = User.Identity.Name;
                modelVM.Service.OrderBy = orderBy + 1;

                if (modelVM.Service.TaxTypeId == 0)
                    modelVM.Service.TaxTypeId = null;
                // ADD NEW ITEM
                serviceData.Add(modelVM.Service);

                //Session[CookieNames.InvoiceServices] = servicesData;
                SaveServices(serviceData);

                // UPDATE FEATURE HAS BEEN OBSOLETE
                //if (servicesData != null && servicesData.Count > 0)
                //{
                //    foreach (InvoiceServiceModel service in servicesData.ToList())
                //    {
                //        if (modelVM.Service.Id != 0 && modelVM.Service.Id == service.Id)
                //        {
                //            // UPDATE THE EXISTING SERVICE
                //            //service.Id = modelVM.Service.Id;
                //            //service.BusinessId = modelVM.Service.BusinessId;
                //            service.ModifyBy = User.Identity.Name;
                //            service.InvoiceId = modelVM.Service.InvoiceId;
                //            service.Name = modelVM.Service.Name;
                //            service.WorkTypeId = modelVM.Service.WorkTypeId;
                //            service.VehicleNumber = modelVM.Service.VehicleNumber;
                //            service.Qty = modelVM.Service.Qty;
                //            service.Rate = modelVM.Service.Rate;
                //            service.Subtotal = modelVM.Service.Subtotal;
                //            service.OrderBy = modelVM.Service.OrderBy;
                //            service.IsTaxable = modelVM.Service.IsTaxable;
                //            service.TaxTypeId = modelVM.Service.TaxTypeId;
                //            service.IGSTPercentage = modelVM.Service.IGSTPercentage;
                //            service.IGSTAmount = modelVM.Service.IGSTAmount;
                //            service.CGSTPercentage = modelVM.Service.CGSTPercentage;
                //            service.CGSTAmount = modelVM.Service.CGSTAmount;
                //            service.SGSTPercentage = modelVM.Service.SGSTPercentage;
                //            service.SGSTAmount = modelVM.Service.SGSTAmount;
                //            service.TaxableTotal = modelVM.Service.TaxableTotal;
                //            service.Total = modelVM.Service.Total;
                //        }
                //        else if()
                //        {
                //            modelVM.Service.OrderBy = orderBy + 1;
                //            // ADD IF NOT EXISTS
                //            servicesData.Add(modelVM.Service);
                //        }
                //    }
                //}
                //else
                //{
                //    modelVM.Service.OrderBy = orderBy + 1;
                //    // ADD IF NEW ITEM
                //    servicesData.Add(modelVM.Service);
                //}
                //Session[SessionNames.InvoiceServices] = servicesData;

                output.Service = new InvoiceServiceModel
                {
                    Qty = 1,
                    IsSucceeded = true,
                    ReturnMessage = "Invoice service has been successfully created."
                };

                //return PartialView("_InvoiceServiceFormPartial", serviceVM);
            }

            return PartialView("_InvoiceServiceFormPartial", output);
        }


        [HttpGet, ActionName("RemoveAll")]
        public void RemoveAllServices()
        {
            //Session[CookieNames.InvoiceServices] = null;
            DeleteServices();
        }



        [NonAction]
        private IEnumerable<SelectListItem> GetSelectListItemsCategory(List<CategoryMasterModel> elements)
        {
            var selectList = new List<SelectListItem>();
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.Id.ToString(),
                    Text = element.Name
                });
            }
            return selectList;
        }


        private void SaveServices(List<InvoiceServiceModel> data)
        {
            DeleteServices();
            SaveCookie(CookieNames.InvoiceServices, JsonConvert.SerializeObject(data));
        }
        private List<InvoiceServiceModel> GetServices()
        {
            string services = GetCookie(CookieNames.InvoiceServices);

            if (!string.IsNullOrEmpty(services))
                return JsonConvert.DeserializeObject<List<InvoiceServiceModel>>(services);

            return new List<InvoiceServiceModel>();
        }
        private void DeleteServices()
        {
            SaveCookie(CookieNames.InvoiceServices, string.Empty, -1);
        }
        private void SaveCookie(string cookieName, string data, double expiryDays = 1)
        {
            HttpCookie InvoiceServiceCookie = new HttpCookie(cookieName, data);
            InvoiceServiceCookie.Expires = DateTime.Now.AddDays(expiryDays);
            HttpContext.Response.Cookies.Add(InvoiceServiceCookie);
        }
        private string GetCookie(string cookieName)
        {
            if (Request.Cookies[cookieName] != null)
            {
                return Request.Cookies[cookieName].Value;
            }
            return string.Empty;
        }
    }
}