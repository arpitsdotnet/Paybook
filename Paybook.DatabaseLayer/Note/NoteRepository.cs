using Paybook.ServiceLayer.Constants;
using Paybook.ServiceLayer.Extensions;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.DatabaseLayer.Note
{
    interface INoteRepository : IBaseRepository<NoteModel>
    {
        int GetTotalCount(int businessId);
    }

    public class NoteRepository : INoteRepository
    {
        private readonly ILogger _logger;
        private readonly IDbContext _dbContext;

        public NoteRepository()
        {
            _logger = LoggerFactory.Instance;
            _dbContext = DbContextFactory.Instance;
        }

        public int GetTotalCount(int businessId)
        {
            try
            {
                var p = new { BusinessId = businessId };

                var result = _dbContext.SaveDataOutParam("sps_Notes_GetTotalCount", p, out int count, DbType.Int32, null, "Count");

                return count;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }

        public List<NoteModel> GetAllByPage(int businessId, int page, string search, string orderBy)
        {
            try
            {
                var p = new { BusinessId = businessId, Page = page, Search = search, OrderBy = orderBy };

                var result = _dbContext.LoadData<NoteModel, dynamic>("sps_Notes_GetAllByPage", p);
                //return _dbContext.LoadDataByProcedure("sps_Agency_SelectName", null);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public NoteModel GetById(int businessId, int id)
        {
            try
            {
                var p = new { BusinessId = businessId, Id = id };

                var result = _dbContext.LoadData<NoteModel, dynamic>("sps_Notes_GetById", p);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public int Create(NoteModel model)
        {
            try
            {
                var result = _dbContext.SaveDataOutParam("spi_Notes_Insert", model, out int noteId, DbType.Int32, null, "Id");
                //_dbContext.LoadDataByProcedure("sps_Agency_Insert", oParams);

                model.Id = noteId;

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }

        }
        public int Update(NoteModel model)
        {
            try
            {
                var result = _dbContext.SaveData("spu_Notes_Update", model);
                //_dbContext.LoadDataByProcedure("sps_Agency_Update", oParams);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public int Activate(int businessId, int id, bool active)
        {
            try
            {
                var p = new { BusinessId = businessId, Id = id, IsActive = active };

                var result = _dbContext.SaveData("spu_Notes_Activate", p);
                //_dbContext.LoadDataByProcedure("sps_Agency_Update", oParams);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public int Delete(int businessId, int id)
        {
            try
            {
                var p = new { BusinessId = businessId, Id = id };

                var result = _dbContext.SaveData("spd_Notes_Delete", p);
                //_dbContext.LoadDataByProcedure("sps_Agency_Update", oParams);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
    }
}
