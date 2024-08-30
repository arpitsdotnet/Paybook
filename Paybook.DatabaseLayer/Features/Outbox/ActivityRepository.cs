using System.Collections.Generic;
using Paybook.DatabaseLayer.Abstracts.Outbox;
using Paybook.ServiceLayer.Models.Activities;

namespace Paybook.DatabaseLayer.Features.Outbox
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IDbContext _dbContext;

        public ActivityRepository()
        {
            _dbContext = DbContextFactory.Instance;
        }

        public List<ActivityModel> GetAllByPage(int businessId, int page, string search, string orderBy)
        {
            var p = new { BusinessId = businessId, CreateBy = "" };

            var result = _dbContext.LoadData<ActivityModel, dynamic>("sps_Activities_GetAll", p);

            return result;
        }

        public int Create(ActivityModel model)
        {
            var p = new { model.BusinessId, model.CreateBy, model.Status, model.Text, model.TextHtml };
            int result = _dbContext.SaveData("spi_Activities_Insert", p);

            return result;
        }
    }
}
