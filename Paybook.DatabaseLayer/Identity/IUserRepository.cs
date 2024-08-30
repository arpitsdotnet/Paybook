using Paybook.ServiceLayer.Models;
using System.Collections.Generic;

namespace Paybook.DatabaseLayer.Identity
{
    public interface IUserRepository
    {
        List<IdentityUserModel> GetAllByPage(int page, string search, string orderBy);
        IdentityUserModel GetById(int id);
        IdentityUserModel GetByUsername(string username);
        int Create(IdentityUserModel model);
        int Update(IdentityUserModel model);
        int Activate(int id, int userId, bool active);
        int Delete(int id, int userId);
    }
}
