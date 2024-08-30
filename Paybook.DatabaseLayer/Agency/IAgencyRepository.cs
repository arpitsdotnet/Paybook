using Paybook.ServiceLayer.Models.Agencies;
using System.Collections.Generic;

namespace Paybook.DatabaseLayer.Agency
{
    public interface IAgencyRepository : IBaseRepository<AgencyModel>
    {
        List<AgencyModel> GetAllName(int businessId);
    }
}
