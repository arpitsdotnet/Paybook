using Paybook.ServiceLayer.Models;
using System.Collections.Generic;

namespace Paybook.DatabaseLayer.Setting
{
    public interface ICategoryRepository : IBaseRepository<CategoryMasterModel>
    {
        CategoryMasterModel GetByCore(int businessId, string core);
        List<CategoryMasterModel> GetAllByTypeCore(int businessId, string typeCore);
    }
}
