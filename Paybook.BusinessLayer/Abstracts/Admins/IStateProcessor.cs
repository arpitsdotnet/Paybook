using Paybook.ServiceLayer.Models;
using System.Collections.Generic;

namespace Paybook.BusinessLayer.Abstracts.Admins
{
    public interface IStateProcessor
    {
        List<StateMasterModel> GetAllByPage(int countryId, int page, string search, string orderBy);
        StateMasterModel GetById(int id);
        StateMasterModel Create(StateMasterModel model);
        StateMasterModel Update(StateMasterModel model);
        StateMasterModel Activate(int id, bool active);
        StateMasterModel Delete(int id);
    }
}
