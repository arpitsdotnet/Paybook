using System.Linq;
using Paybook.ServiceLayer.Models;

namespace Paybook.DatabaseLayer.Common
{
    public class LastSavedNumberRepository : ILastSavedNumberRepository
    {
        private readonly IDbContext _dbContext;

        public LastSavedNumberRepository()
        {
            _dbContext = DbContextFactory.Instance;
        }

        public LastSavedNumberModel GetNewNumberByType(int businessId, string type)
        {
            var p = new { BusinessId = businessId, Type = type };

            var result = _dbContext.LoadData<LastSavedNumberModel, dynamic>("sps_LastSavedNumbers_GetNewNumberByType", p);

            return result.FirstOrDefault();
        }


        public int Update(LastSavedNumberModel model)
        {
            var result = _dbContext.SaveData("spu_LastSavedNumber_Update", model);
            //var result = _dbContext.SaveData("sps_LastSavedID_Update", model);

            return result;
        }
    }
}
