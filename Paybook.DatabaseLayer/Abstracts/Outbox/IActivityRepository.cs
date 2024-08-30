using Paybook.ServiceLayer.Models.Activities;
using System.Collections.Generic;

namespace Paybook.DatabaseLayer.Abstracts.Outbox
{
    public interface IActivityRepository
    {
        List<ActivityModel> GetAllByPage(int businessId, int page, string search, string orderBy);
        int Create(ActivityModel model);
    }
}
