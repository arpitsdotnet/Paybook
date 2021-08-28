using Paybook.BusinessLayer.Client;
using Paybook.BusinessLayer.Common;
using Paybook.BusinessLayer.Invoice;
using Paybook.BusinessLayer.Payment;
using Paybook.BusinessLayer.Setting;
using Paybook.ServiceLayer;
using Paybook.ServiceLayer.Constants;
using Paybook.ServiceLayer.Models;
using Paybook.Web.MvcUI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paybook.Web.MvcUI.Areas.Chief.Controllers
{
    [RouteArea("Chief")]
    [Authorize]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceProcessor _invoice;
        private readonly IInvoiceServiceProcessor _service;
        private readonly IClientProcessor _client;
        private readonly IPaymentProcessor _payment;
        private readonly ICategoryProcessor _category;
        private readonly ILastSavedNumberProcessor _numberProcessor;
        private readonly IStateProcessor _state;
        private readonly ICountryProcessor _country;

        public InvoiceController(IInvoiceProcessor invoice,
            IInvoiceServiceProcessor service,
            IClientProcessor client,
            IPaymentProcessor payment,
            ICategoryProcessor category,
            ILastSavedNumberProcessor numberProcessor,
            IStateProcessor state,
            ICountryProcessor country)
        {
            _invoice = invoice;
            _service = service;
            _client = client;
            _payment = payment;
            _category = category;
            _numberProcessor = numberProcessor;
            _state = state;
            _country = country;
        }

        [HttpGet, AllowAnonymous]
        public ActionResult GetCounters(string username)
        {
            InvoiceCountersModel model = _invoice.GetAllCounters(username);
            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Index()
        {
            List<InvoiceModel> model = _invoice.GetAllByPage(User.Identity.Name, 0, "", "");

            foreach (var item in model)
            {
                item.StatusCategoryMaster = _category.GetById(User.Identity.Name, item.StatusId);
                item.Client = _client.GetById(User.Identity.Name, item.ClientId);
                item.PaidTotal = _payment.GetPaidAmountByInvoiceId(User.Identity.Name, item.Id);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var clients = _client.GetAllByPage(User.Identity.Name, 0, "", "");
            var terms = _category.GetAllByTypeCore(User.Identity.Name, "Terms");
            var discountTypes = _category.GetAllByTypeCore(User.Identity.Name, "DiscountTypes");
            var invoiceMessage = _category.GetByCore(User.Identity.Name, "InvoiceMessage");

            List<InvoiceServiceModel> servicesData;

            if (Session[CookieNames.InvoiceServices] == null)
                servicesData = new List<InvoiceServiceModel>();
            else
                servicesData = (List<InvoiceServiceModel>)Session[CookieNames.InvoiceServices];


            var invoiceVM = new InvoiceViewModel
            {
                Invoice = new InvoiceModel
                {
                    Message = invoiceMessage.Value,
                    Subtotal = servicesData.Sum(x => x.Subtotal),
                    TaxableTotal = servicesData.Sum(x => x.TaxableTotal)
                },
                InvoiceServices = servicesData,
                Clients = GetSelectListItemsClient(clients),
                Terms = GetSelectListItemsTerms(terms),
                DiscountTypes = GetSelectListItemsCategory(discountTypes)
            };
            invoiceVM.Invoice.Total = invoiceVM.Invoice.TotalCalculate;

            return View(invoiceVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(InvoiceViewModel modelVM)
        {
            if (Session[CookieNames.InvoiceServices] == null)
                ModelState.AddModelError(string.Empty, "Services are required to save, please add atleast one service.");

            var invoiceServices = Session[CookieNames.InvoiceServices] == null ? null : (List<InvoiceServiceModel>)Session[CookieNames.InvoiceServices];

            if (invoiceServices != null && invoiceServices.Count() > 0)
            {
                // ADD WORK TYPE FOR EACH ITEM
                foreach (var item in invoiceServices)
                {
                    item.WorkTypeCategoryMaster = _category.GetById(User.Identity.Name, item.WorkTypeId);
                }
            }

            var clients = _client.GetAllByPage(User.Identity.Name, 0, "", "");
            var terms = _category.GetAllByTypeCore(User.Identity.Name, "Terms");
            var discountTypes = _category.GetAllByTypeCore(User.Identity.Name, "DiscountTypes");

            var invoiceVM = new InvoiceViewModel
            {
                Invoice = modelVM.Invoice,
                InvoiceServices = invoiceServices,
                Clients = GetSelectListItemsClient(clients),
                Terms = GetSelectListItemsTerms(terms),
                DiscountTypes = GetSelectListItemsCategory(discountTypes)
            };

            if (ModelState.IsValid)
            {
                string newNumber = _numberProcessor.GetNewNumberByType(User.Identity.Name, LastSavedNumberTypes.Invoice);

                invoiceVM.Invoice.CreateBy = User.Identity.Name;
                invoiceVM.Invoice.InvoiceNumber = newNumber;
                
                // TermsId on Create page is Value of the Terms
                foreach (var item in terms)
                {
                    if (item.Value == modelVM.Invoice.TermsId.ToString())
                    {
                        invoiceVM.Invoice.TermsId = item.Id;
                        break;
                    }
                }                

                foreach (var service in invoiceVM.InvoiceServices)
                {
                    service.CreateBy = User.Identity.Name;
                }

                var output = new InvoiceViewModel
                {
                    Clients = invoiceVM.Clients,
                    Terms = invoiceVM.Terms,
                    DiscountTypes = invoiceVM.DiscountTypes
                };
                output.Invoice = _invoice.CreateWithServices(invoiceVM.Invoice, invoiceVM.InvoiceServices);

                if (output.Invoice.IsSucceeded == true)
                    return RedirectToAction("Index");

                return View(invoiceVM);
            }

            return View(invoiceVM);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var invoice = _invoice.GetById(User.Identity.Name, id);
            invoice.Client = _client.GetById(User.Identity.Name, invoice.ClientId);
            invoice.PaidTotal = _payment.GetPaidAmountByInvoiceId(User.Identity.Name, id);

            var payments = _payment.GetAllByInvoiceId(User.Identity.Name, id, 0, "", "");

            var terms = _category.GetAllByTypeCore(User.Identity.Name, "Terms");
            var discountTypes = _category.GetAllByTypeCore(User.Identity.Name, "DiscountTypes");

            Session[CookieNames.InvoiceServices] = _service.GetAllByInvoiceId(User.Identity.Name, id);

            List<InvoiceServiceModel> servicesData;
            if (Session[CookieNames.InvoiceServices] == null)
                servicesData = new List<InvoiceServiceModel>();
            else
                servicesData = (List<InvoiceServiceModel>)Session[CookieNames.InvoiceServices];

            if (servicesData != null && servicesData.Count() > 0)
            {
                // ADD WORK TYPE FOR EACH ITEM
                foreach (var item in servicesData)
                {
                    item.WorkTypeCategoryMaster = _category.GetById(User.Identity.Name, item.WorkTypeId);
                }
            }

            var invoiceVM = new InvoiceViewModel
            {
                Invoice = invoice,
                InvoiceServices = servicesData,
                InvoicePayments = payments,
                //Clients = GetSelectListItemsClient(clients),
                Terms = GetSelectListItemsTerms(terms),
                DiscountTypes = GetSelectListItemsCategory(discountTypes)
            };
            invoiceVM.Invoice.Total = invoiceVM.Invoice.TotalCalculate;

            return View(invoiceVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(InvoiceViewModel modelVM)
        {
            if (Session[CookieNames.InvoiceServices] == null)
                ModelState.AddModelError(string.Empty, "Services are required to save, please add atleast one service.");

            var invoiceServices = Session[CookieNames.InvoiceServices] == null ? null : (List<InvoiceServiceModel>)Session[CookieNames.InvoiceServices];

            if (invoiceServices != null && invoiceServices.Count() > 0)
            {
                // ADD WORK TYPE FOR EACH ITEM
                foreach (var item in invoiceServices)
                {
                    item.WorkTypeCategoryMaster = _category.GetById(User.Identity.Name, item.WorkTypeId);
                }
            }

            var clients = _client.GetAllByPage(User.Identity.Name, 0, "", "");
            var terms = _category.GetAllByTypeCore(User.Identity.Name, "Terms");
            var discountTypes = _category.GetAllByTypeCore(User.Identity.Name, "DiscountTypes");

            var invoiceVM = new InvoiceViewModel
            {
                Invoice = modelVM.Invoice,
                InvoiceServices = invoiceServices,
                Clients = GetSelectListItemsClient(clients),
                Terms = GetSelectListItemsTerms(terms),
                DiscountTypes = GetSelectListItemsCategory(discountTypes)
            };

            if (ModelState.IsValid)
            {
                string newNumber = _numberProcessor.GetNewNumberByType(User.Identity.Name, LastSavedNumberTypes.Invoice);

                // TermsId on Create page is Value of the Terms
                foreach (var item in terms)
                {
                    if (item.Value == modelVM.Invoice.TermsId.ToString())
                    {
                        invoiceVM.Invoice.TermsId = item.Id;
                        break;
                    }
                }
                foreach (var service in invoiceVM.InvoiceServices)
                {
                    service.CreateBy = User.Identity.Name;
                }

                invoiceVM.Invoice.ModifyBy = User.Identity.Name;
                invoiceVM.Invoice.InvoiceNumber = newNumber;

                foreach (var service in invoiceVM.InvoiceServices)
                {
                    service.CreateBy = User.Identity.Name;
                }

                var output = new InvoiceViewModel
                {
                    Clients = invoiceVM.Clients,
                    Terms = invoiceVM.Terms,
                    DiscountTypes = invoiceVM.DiscountTypes
                };
                output.Invoice = _invoice.CreateWithServices(invoiceVM.Invoice, invoiceVM.InvoiceServices);

                if (output.Invoice.IsSucceeded == true)
                    return RedirectToAction("Index");

                return View(invoiceVM);
            }

            return View(invoiceVM);
        }


        [HttpGet]
        public ActionResult Calculate(decimal discountTotal)
        {
            List<InvoiceServiceModel> servicesData;

            if (Session[CookieNames.InvoiceServices] == null)
                servicesData = new List<InvoiceServiceModel>();
            else
                servicesData = (List<InvoiceServiceModel>)Session[CookieNames.InvoiceServices];

            InvoiceModel model = new InvoiceModel()
            {
                Subtotal = servicesData.Sum(x => x.Subtotal),
                TaxableTotal = servicesData.Sum(x => x.TaxableTotal),
                DiscountTotal = discountTotal
            };
            model.Total = model.TotalCalculate;

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetClientById(int id)
        {
            ClientModel client = _client.GetById(User.Identity.Name, id);
            client.StateMaster = _state.GetById(client.StateId);
            client.CountryMaster = _country.GetById(client.CountryId);

            ClientViewModel clientVM = new ClientViewModel
            {
                Client = client,
                RemainingTotal = _client.GetRemainingAmountById(User.Identity.Name, id)
            };

            return Json(clientVM, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        private IEnumerable<SelectListItem> GetSelectListItemsClient(List<ClientModel> elements)
        {
            var selectList = new List<SelectListItem>();
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.Id.ToString(),
                    Text = element.Name + (!string.IsNullOrWhiteSpace(element.AgencyName) ? " (" + element.AgencyName + ")" : "")
                });
            }
            return selectList;
        }
        [NonAction]
        private IEnumerable<SelectListItem> GetSelectListItemsTerms(List<CategoryMasterModel> elements)
        {
            var selectList = new List<SelectListItem>();
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element.Value,
                    Text = element.Name
                });
            }
            return selectList;
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
    }
}