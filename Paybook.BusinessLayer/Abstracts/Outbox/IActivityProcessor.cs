using Paybook.ServiceLayer.Models.Activities;
using System.Collections.Generic;

namespace Paybook.BusinessLayer.Abstracts.Outbox
{
    public interface IActivityProcessor
    {
        List<ActivityModel> GetAllByPage(int businessId, int page, string search, string orderBy);
        ActivityModel Create(ActivityBuilderModel model);
    }
}
