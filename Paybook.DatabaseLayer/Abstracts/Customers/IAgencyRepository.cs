using Paybook.ServiceLayer.Models.Agencies;
using System.Collections.Generic;

namespace Paybook.DatabaseLayer.Abstracts.Customers
{
    public interface IAgencyRepository : IBaseRepository<AgencyModel>
    {
        List<AgencyModel> GetAllName(int businessId);
    }
}
