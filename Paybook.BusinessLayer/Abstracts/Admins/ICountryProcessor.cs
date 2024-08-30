using Paybook.ServiceLayer.Models;
using System.Collections.Generic;

namespace Paybook.BusinessLayer.Abstracts.Admins
{
    public interface ICountryProcessor
    {
        List<CountryMasterModel> GetAllByPage(int page, string search, string orderBy);
        CountryMasterModel GetById(int id);
        CountryMasterModel Create(CountryMasterModel model);
        CountryMasterModel Update(CountryMasterModel model);
        CountryMasterModel Activate(int id, bool active);
        CountryMasterModel Delete(int id);
    }
}
