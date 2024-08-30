using System.Collections.Generic;
using System.Data;
using System.Linq;
using Paybook.ServiceLayer.Models;

namespace Paybook.DatabaseLayer.Note
{
    public class NoteRepository : INoteRepository
    {
        private readonly IDbContext _dbContext;

        public NoteRepository()
        {
            _dbContext = DbContextFactory.Instance;
        }

        public int GetTotalCount(int businessId)
        {
            var p = new { BusinessId = businessId };

            var result = _dbContext.SaveDataOutParam("sps_Notes_GetTotalCount", p, out int count, DbType.Int32, null, "Count");

            return count;
        }

        public List<NoteModel> GetAllByPage(int businessId, int page, string search, string orderBy)
        {
            var p = new { BusinessId = businessId, Page = page, Search = search, OrderBy = orderBy };

            var result = _dbContext.LoadData<NoteModel, dynamic>("sps_Notes_GetAllByPage", p);
            //return _dbContext.LoadDataByProcedure("sps_Agency_SelectName", null);

            return result;
        }
        public NoteModel GetById(int businessId, int id)
        {
            var p = new { BusinessId = businessId, Id = id };

            var result = _dbContext.LoadData<NoteModel, dynamic>("sps_Notes_GetById", p);

            return result.FirstOrDefault();
        }
        public int Create(NoteModel model)
        {
            var result = _dbContext.SaveDataOutParam("spi_Notes_Insert", model, out int noteId, DbType.Int32, null, "Id");
            //_dbContext.LoadDataByProcedure("sps_Agency_Insert", oParams);

            model.Id = noteId;

            return result;
        }
        public int Update(NoteModel model)
        {
            var result = _dbContext.SaveData("spu_Notes_Update", model);
            //_dbContext.LoadDataByProcedure("sps_Agency_Update", oParams);

            return result;
        }
        public int Activate(int businessId, string username, int id, bool active)
        {
            var p = new { BusinessId = businessId, Username = username, Id = id, IsActive = active };

            var result = _dbContext.SaveData("spu_Notes_Activate", p);
            //_dbContext.LoadDataByProcedure("sps_Agency_Update", oParams);

            return result;
        }
        public int Delete(int businessId, string username, int id)
        {
            var p = new { BusinessId = businessId, Username = username, Id = id };

            var result = _dbContext.SaveData("spd_Notes_Delete", p);

            return result;
        }
    }
}
