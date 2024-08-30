using Paybook.ServiceLayer.Models;
using System.Collections.Generic;

namespace Paybook.BusinessLayer.Abstracts.Identity
{
    public interface IUserProcessor
    {
        List<IdentityUserModel> GetAllByPage(int page, string search, string orderBy);
        IdentityUserModel GetById(int id);
        IdentityUserModel GetByUsername(string username);
        IdentityUserModel Create(IdentityUserModel model);
        IdentityUserModel Update(IdentityUserModel model);
        IdentityUserModel Activate(int id, int userId, bool active);
        IdentityUserModel Delete(int id, int userId);
    }
}
