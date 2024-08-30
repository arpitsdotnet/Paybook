using Paybook.ServiceLayer.Models;
using System.Collections.Generic;

namespace Paybook.BusinessLayer.Abstracts.Admins
{
    public interface ICategoryProcessor : IBaseProcessor<CategoryMasterModel>
    {
        List<CategoryMasterModel> GetAllByTypeCore(int businessId, string typeCore);
        CategoryMasterModel GetByCore(int businessId, string core);
    }
}
