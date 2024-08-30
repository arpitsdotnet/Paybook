using Paybook.ServiceLayer.Models;
using System.Collections.Generic;

namespace Paybook.DatabaseLayer.Abstracts.Admins
{
    public interface ICountryRepository
    {
        List<CountryMasterModel> GetAllByPage(int page, string search, string orderBy);
        CountryMasterModel GetById(int id);
        int Create(CountryMasterModel model);
        int Update(CountryMasterModel model);
        int Activate(int id, bool active);
        int Delete(int id);
    }
}
