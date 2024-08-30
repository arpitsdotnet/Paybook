using Paybook.ServiceLayer.Models;
using System.Collections.Generic;

namespace Paybook.DatabaseLayer.Abstracts.Admins
{
    public interface IBusinessRepository
    {
        bool IsExist(string createBy, string businessName);

        List<BusinessModel> GetAllByUsername(string username);
        BusinessModel GetSelectedByUsername(string username);
        BusinessModel GetById(int id, string username);
        int Create(BusinessModel model);
        int Update(BusinessModel model);
        int UpdateSelected(int id, string username);
        int Activate(int id, string username, bool active);
        int Delete(int id);
    }
}
