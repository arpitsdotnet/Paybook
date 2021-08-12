using Paybook.BusinessLayer.Client;
using Paybook.BusinessLayer.Identity;
using Paybook.BusinessLayer.Setting;
using Paybook.ServiceLayer.Constants;
using Paybook.ServiceLayer.Logger;
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
    public class ClientController : Controller
    {
        private readonly IClientProcessor _client;
        private readonly ICountryProcessor _country;
        private readonly IStateProcessor _state;

        public ClientController(IClientProcessor client, ICountryProcessor country, IStateProcessor state)
        {
            _client = client;
            _country = country;
            _state = state;
        }

        [HttpGet]
        public ActionResult Index()
        {
            int? businessId = GetSelectedBusinessId();

            if (businessId == null)
                return RedirectToAction("Create", "Business");

            List<ClientModel> model = _client.GetAllByPage(businessId.Value, 0, "", "");


            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            int? businessId = GetSelectedBusinessId();

            if (businessId == null)
                return RedirectToAction("Create", "Business");

            var countries = _country.GetAllByPage(0, "", "");
            int countryId = countries[0].Id;
            var clientVM = new ClientViewModel
            {
                Countries = GetSelectListItemsCountry(countries),
                States = GetSelectListItemsState(_state.GetAllByPage(countryId, 0, "", ""))
            };

            return View(clientVM);
        }

        [HttpPost, ActionName("Create")]
        public ActionResult CreatePost(ClientViewModel model)
        {
            int? businessId = GetSelectedBusinessId();
            if (businessId == null)
                return RedirectToAction("Create", "Business");

            var countries = _country.GetAllByPage(0, "", "");
            int countryId = countries[0].Id;
            var clientVM = new ClientViewModel
            {
                Countries = GetSelectListItemsCountry(countries),
                States = GetSelectListItemsState(_state.GetAllByPage(countryId, 0, "", ""))
            };

            if (ModelState.IsValid)
            {
                model.Client.BusinessId = businessId.Value;
                model.Client.CreateBy = User.Identity.Name;
                ClientModel output = _client.Create(model.Client);
                clientVM.Client = output;

                return View(clientVM);
            }

            clientVM.Client = model.Client;

            return View(clientVM);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            int? businessId = GetSelectedBusinessId();
            if (businessId == null)
                return RedirectToAction("Create", "Business");

            var clientData = _client.GetById(businessId.Value, id);

            var countries = _country.GetAllByPage(0, "", "");
            int countryId = countries[0].Id;
            var clientVM = new ClientViewModel
            {
                Client = clientData,
                Countries = GetSelectListItemsCountry(countries),
                States = GetSelectListItemsState(_state.GetAllByPage(countryId, 0, "", ""))
            };

            return View(clientVM);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult EditPost(ClientViewModel model)
        {
            int? businessId = GetSelectedBusinessId();
            if (businessId == null)
                return RedirectToAction("Create", "Business");

            var countries = _country.GetAllByPage(0, "", "");
            int countryId = countries[0].Id;
            var clientVM = new ClientViewModel
            {
                Countries = GetSelectListItemsCountry(countries),
                States = GetSelectListItemsState(_state.GetAllByPage(countryId, 0, "", ""))
            };

            if (ModelState.IsValid)
            {
                model.Client.BusinessId = businessId.Value;
                model.Client.ModifyBy = User.Identity.Name;
                
                ClientModel output = _client.Update(model.Client);
                output.Name = model.Client.Name;
                clientVM.Client = output;

                return View(clientVM);
            }

            clientVM.Client = model.Client;

            return View(clientVM);
        }

        [NonAction]
        private int? GetSelectedBusinessId()
        {
            return (int?)TempData.Peek(TempdataNames.SelectedBusinessId);
        }
        [NonAction]
        private IEnumerable<SelectListItem> GetSelectListItemsState(List<StateMasterModel> elements)
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
        [NonAction]
        private IEnumerable<SelectListItem> GetSelectListItemsCountry(List<CountryMasterModel> elements)
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