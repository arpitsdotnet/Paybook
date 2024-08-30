using System.Collections.Generic;
using System.Web.Mvc;
using Paybook.ServiceLayer.Models;

namespace Paybook.ServiceLayer.Services
{
    public class SelectListItemGenerator
    {
        public static IEnumerable<SelectListItem> GetCountryItemList(List<CountryMasterModel> elements)
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
        public static IEnumerable<SelectListItem> GetStateItemList(List<StateMasterModel> elements)
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
