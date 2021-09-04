using Newtonsoft.Json;
using Paybook.BusinessLayer.Client;
using Paybook.BusinessLayer.Common;
using Paybook.BusinessLayer.Invoice;
using Paybook.BusinessLayer.Payment;
using Paybook.BusinessLayer.Setting;
using Paybook.ServiceLayer;
using Paybook.ServiceLayer.Constants;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Paging;
using Paybook.ServiceLayer.Services;
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
        private readonly IInvoicePayProcessor _invoicePay;
        private readonly ICategoryProcessor _category;
        private readonly ILastSavedNumberProcessor _numberProcessor;
        private readonly IStateProcessor _state;
        private readonly ICountryProcessor _country;
        private readonly IActivityProcessor _activity;

        public InvoiceController(IInvoiceProcessor invoice,
            IInvoiceServiceProcessor service,
            IClientProcessor client,
            IPaymentProcessor payment,
            IInvoicePayProcessor invoicePay,
            ICategoryProcessor category,
            ILastSavedNumberProcessor numberProcessor,
            IStateProcessor state,
            ICountryProcessor country,
            IActivityProcessor activity)
        {
            _invoice = invoice;
            _service = service;
            _client = client;
            _payment = payment;
            _invoicePay = invoicePay;
            _category = category;
            _numberProcessor = numberProcessor;
            _state = state;
            _country = country;
            _activity = activity;
        }

        private int BusinessId { get { return Convert.ToInt32(Request.Cookies[CookieNames.SelectedBusinessId].Value); } }


        [HttpGet]
        public ActionResult Index(int page = 1, string search = "", string orderBy = "")
        {
            List<InvoiceModel> model = _invoice.GetAllByPage(BusinessId, page, search, orderBy);

            foreach (var item in model)
            {
                item.Description = string.IsNullOrEmpty(item.Description) ? string.Empty : item.Description;
                item.StatusCategoryMaster = _category.GetById(BusinessId, item.StatusId);
                item.Client = _client.GetById(BusinessId, item.ClientId);
                item.PaidTotal = _invoicePay.GetPaidTotalByInvoiceId(BusinessId, item.Id);
            }

            int totalPages = _invoice.GetAllPagesCount(BusinessId, page, search, orderBy);
            var paging = new PagingEntity { CurrentPage = page, TotalPages = totalPages };

            var invoicePagingVM = new InvoicePagingViewModel { Invoices = model, Paging = paging };

            return View(invoicePagingVM);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var clients = _client.GetAllByPage(BusinessId, 0, "", "");
            var terms = _category.GetAllByTypeCore(BusinessId, "Terms");
            var discountTypes = _category.GetAllByTypeCore(BusinessId, "DiscountTypes");
            var invoiceMessage = _category.GetByCore(BusinessId, "InvoiceMessage");
            var workTypes = _category.GetAllByTypeCore(BusinessId, "WorkTypes");
            var taxTypes = _category.GetAllByTypeCore(BusinessId, "TaxTypes");

            //Session[CookieNames.InvoiceServices] = null;
            SaveCookie(CookieNames.InvoiceServices, string.Empty, -1);

            List<InvoiceServiceModel> servicesData = new List<InvoiceServiceModel>();

            var invoiceVM = new InvoiceViewModel
            {
                Invoice = new InvoiceModel
                {
                    Message = invoiceMessage.Value,
                    Subtotal = 0,
                    TaxableTotal = 0
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
            var invoiceServices = GetServices();

            if (invoiceServices == null || invoiceServices.Count() == 0)
                ModelState.AddModelError(string.Empty, "Products/Services are required to save, please add atleast one product/service.");

            //var invoiceServices = Session[CookieNames.InvoiceServices] == null ? new List<InvoiceServiceModel>() : (List<InvoiceServiceModel>)Session[CookieNames.InvoiceServices];

            if (invoiceServices != null && invoiceServices.Count() > 0)
            {
                // ADD WORK TYPE FOR EACH ITEM
                foreach (var item in invoiceServices)
                {
                    item.WorkTypeCategoryMaster = _category.GetById(BusinessId, item.WorkTypeId);
                }
            }

            var clients = _client.GetAllByPage(BusinessId, 0, "", "");
            var terms = _category.GetAllByTypeCore(BusinessId, "Terms");
            var discountTypes = _category.GetAllByTypeCore(BusinessId, "DiscountTypes");

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

                invoiceVM.Invoice.BusinessId = BusinessId;
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

                var output = new InvoiceViewModel
                {
                    Clients = invoiceVM.Clients,
                    Terms = invoiceVM.Terms,
                    DiscountTypes = invoiceVM.DiscountTypes
                };

                output.Invoice = _invoice.CreateWithServices(invoiceVM.Invoice, invoiceVM.InvoiceServices);

                if (output.Invoice.IsSucceeded == true)
                {
                    var client = _client.GetById(BusinessId, invoiceVM.Invoice.ClientId);

                    _activity.Create(new ActivityBuilderModel
                    {
                        BusinessId = BusinessId,
                        CreateBy = User.Identity.Name,
                        Title = ActivityConst.TitleCreated,
                        TitleCss = ActivityConst.CssSuccess,
                        Date = invoiceVM.Invoice.InvoiceDate.ToString("dd/MMM/yyyy"),
                        Type = ActivityConst.TypeInvoice,
                        TypeNumber = invoiceVM.Invoice.InvoiceNumber,
                        ClientName = client.Name,
                        Amount = invoiceVM.Invoice.Total.ToString("0.00")
                    });

                    return RedirectToAction("Index");
                }

                return View(invoiceVM);
            }

            return View(invoiceVM);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var invoice = _invoice.GetById(BusinessId, id);
            invoice.Client = _client.GetById(BusinessId, invoice.ClientId);
            invoice.PaidTotal = _invoicePay.GetPaidTotalByInvoiceId(BusinessId, invoice.Id);

            var invoicePayments = _invoicePay.GetAllByInvoiceId(BusinessId, id, 0, "", "");

            var terms = _category.GetAllByTypeCore(BusinessId, "Terms");
            var discountTypes = _category.GetAllByTypeCore(BusinessId, "DiscountTypes");


            var servicesData = _service.GetAllByInvoiceId(User.Identity.Name, id);
            SaveServices(servicesData);

            //if (Session[CookieNames.InvoiceServices] == null)
            //    servicesData = new List<InvoiceServiceModel>();
            //else
            //    servicesData = (List<InvoiceServiceModel>)Session[CookieNames.InvoiceServices];

            if (servicesData != null && servicesData.Count() > 0)
            {
                // ADD WORK TYPE FOR EACH ITEM
                foreach (var item in servicesData)
                {
                    item.WorkTypeCategoryMaster = _category.GetById(BusinessId, item.WorkTypeId);
                }
            }

            var invoiceVM = new InvoiceViewModel
            {
                Invoice = invoice,
                InvoiceServices = servicesData,
                InvoicePayments = invoicePayments,
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
                    item.WorkTypeCategoryMaster = _category.GetById(BusinessId, item.WorkTypeId);
                }
            }

            var clients = _client.GetAllByPage(BusinessId, 0, "", "");
            var terms = _category.GetAllByTypeCore(BusinessId, "Terms");
            var discountTypes = _category.GetAllByTypeCore(BusinessId, "DiscountTypes");

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
                // TermsId on Create page is Value of the Terms
                foreach (var item in terms)
                {
                    if (item.Value == modelVM.Invoice.TermsId.ToString())
                    {
                        invoiceVM.Invoice.TermsId = item.Id;
                        break;
                    }
                }

                invoiceVM.Invoice.ModifyBy = User.Identity.Name;

                var output = new InvoiceViewModel
                {
                    Clients = invoiceVM.Clients,
                    Terms = invoiceVM.Terms,
                    DiscountTypes = invoiceVM.DiscountTypes
                };
                //output.Invoice = _invoice.UpdateWithServices(invoiceVM.Invoice, invoiceVM.InvoiceServices);

                if (output.Invoice.IsSucceeded == true)
                    return RedirectToAction("Index");

                return View(invoiceVM);
            }

            return View(invoiceVM);
        }

        [HttpGet]
        public ActionResult Void(int id)
        {
            InvoiceModel output = _invoice.UpdateVoid(BusinessId, id);
            if (output.IsSucceeded)
                return RedirectToAction(nameof(Index));

            var invoice = _invoice.GetById(BusinessId, id);
            invoice.IsSucceeded = output.IsSucceeded;
            invoice.ReturnMessage = output.ReturnMessage;

            invoice.Client = _client.GetById(BusinessId, invoice.ClientId);
            invoice.PaidTotal = _invoicePay.GetPaidTotalByInvoiceId(BusinessId, invoice.Id);

            var invoicePayments = _invoicePay.GetAllByInvoiceId(BusinessId, id, 0, "", "");

            var terms = _category.GetAllByTypeCore(BusinessId, "Terms");
            var discountTypes = _category.GetAllByTypeCore(BusinessId, "DiscountTypes");

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
                    item.WorkTypeCategoryMaster = _category.GetById(BusinessId, item.WorkTypeId);
                }
            }

            var invoiceVM = new InvoiceViewModel
            {
                Invoice = invoice,
                InvoiceServices = servicesData,
                InvoicePayments = invoicePayments,
                //Clients = GetSelectListItemsClient(clients),
                Terms = GetSelectListItemsTerms(terms),
                DiscountTypes = GetSelectListItemsCategory(discountTypes)
            };
            invoiceVM.Invoice.Total = invoiceVM.Invoice.TotalCalculate;

            return View(invoiceVM);
        }

        [HttpGet]
        public ActionResult WriteOff(int id)
        {
            InvoiceModel output = _invoice.UpdateWriteOff(BusinessId, id);
            if (output.IsSucceeded)
            {
                return RedirectToAction(nameof(Index));
            }

            var invoice = _invoice.GetById(BusinessId, id);

            invoice.IsSucceeded = output.IsSucceeded;
            invoice.ReturnMessage = output.ReturnMessage;

            invoice.Client = _client.GetById(BusinessId, invoice.ClientId);
            invoice.PaidTotal = _invoicePay.GetPaidTotalByInvoiceId(BusinessId, invoice.Id);

            var invoicePayments = _invoicePay.GetAllByInvoiceId(BusinessId, id, 0, "", "");

            var terms = _category.GetAllByTypeCore(BusinessId, "Terms");
            var discountTypes = _category.GetAllByTypeCore(BusinessId, "DiscountTypes");

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
                    item.WorkTypeCategoryMaster = _category.GetById(BusinessId, item.WorkTypeId);
                }
            }

            var invoiceVM = new InvoiceViewModel
            {
                Invoice = invoice,
                InvoiceServices = servicesData,
                InvoicePayments = invoicePayments,
                //Clients = GetSelectListItemsClient(clients),
                Terms = GetSelectListItemsTerms(terms),
                DiscountTypes = GetSelectListItemsCategory(discountTypes)
            };
            invoiceVM.Invoice.Total = invoiceVM.Invoice.TotalCalculate;

            return View(invoiceVM);
        }

        [HttpGet]
        public ActionResult Calculate(decimal discountTotal)
        {
            List<InvoiceServiceModel> servicesData = GetServices();

            //if (Session[CookieNames.InvoiceServices] == null)
            //    servicesData = new List<InvoiceServiceModel>();
            //else
            //    servicesData = (List<InvoiceServiceModel>)Session[CookieNames.InvoiceServices];

            InvoiceModel model = new InvoiceModel()
            {
                Subtotal = servicesData.Sum(x => x.Subtotal),
                TaxableTotal = servicesData.Sum(x => x.TaxableTotal),
                DiscountTotal = discountTotal
            };
            model.Total = model.TotalCalculate;

            return Json(model, JsonRequestBehavior.AllowGet);
        }


        [OutputCache(Duration = 180)]
        [HttpGet, AllowAnonymous]
        public ActionResult GetCounters(string username)
        {
            InvoiceCountersModel model = _invoice.GetAllCounters(BusinessId);
            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
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