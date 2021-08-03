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
    interface INoteRepository
    {
        NoteModel[] GetAllByPage(string sGridPageNumber);
        NoteModel GetByNoteID(string sDataID);
        int GetTotalCount();
        bool Create(NoteModel noteModel);
        bool Update(NoteModel noteModel);
        bool Delete(string sDataID);
    }

    public class NoteRepository : INoteRepository
    {
        private readonly ILogger _logger;
        private readonly IDbContext _dbContext;

        public NoteRepository()
        {
            _logger = FileLogger.Instance;
            _dbContext = DbContextFactory.Instance;
        }


        public NoteModel[] GetAllByPage(string sGridPageNumber)
        {
            List<NoteModel> oDailyNotes = new List<NoteModel>();
            try
            {
                List<Parameter> oParams = new List<Parameter>();
                string sDataID = "";
                oParams.Add(new Parameter("sDataID", sDataID));
                DataTable dt = _dbContext.LoadDataByProcedure("sps_DailyNotes_Select", oParams);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string dtCount = dt.Rows.Count.ToString();
                    // int dRowTotal = int.Parse(dtCount);
                    int iPageNumber = Convert.ToInt32(sGridPageNumber);
                    int iPageStart = iPageNumber == 0 ? 0 : (PagerSetting.iPageSizeDefault * iPageNumber);

                    var list = (from e in dt.AsEnumerable()
                                select new
                                {
                                    RowCount = dtCount,
                                    ID = e.Field<int>("ID"),
                                    CreatedDT = e.Field<DateTime>("CreatedDT").ToString(),
                                    Name = e.Field<string>("Name"),
                                    VehicleNumber = e.Field<string>("VehicleNumber"),
                                    MobileNumber = e.Field<string>("MobileNumber"),
                                    Work = e.Field<string>("Work"),
                                    TotalAmount = e.Field<string>("TotalAmount"),
                                    Awak = e.Field<string>("Awak"),
                                    Jawak = e.Field<string>("Jawak"),
                                    Balance = e.Field<string>("Balance"),
                                    Note = e.Field<string>("Note")
                                }).Skip(iPageStart).Take(PagerSetting.iPageSizeDefault);

                    dt = list.ToList().ToDataTable();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            NoteModel oDataRows = new NoteModel();
                            oDataRows.RowCount = dr["RowCount"].ToString();
                            oDataRows.ID = dr["ID"].ToString();
                            oDataRows.CreatedDT = Convert.ToDateTime(dr["CreatedDT"]).ToString("yyyy-MM-dd HH:mm:ss");
                            oDataRows.Name = dr["Name"].ToString();
                            oDataRows.VehicleNumber = dr["VehicleNumber"].ToString();
                            oDataRows.MobileNumber = dr["MobileNumber"].ToString();
                            oDataRows.Work = dr["Work"].ToString();
                            oDataRows.TotalAmount = dr["TotalAmount"].ToString();
                            oDataRows.Awak = dr["Awak"].ToString();
                            oDataRows.Jawak = dr["Jawak"].ToString();
                            oDataRows.Balance = dr["Balance"].ToString();
                            oDataRows.Note = dr["Note"].ToString();
                            oDailyNotes.Add(oDataRows);

                        }
                    }
                    else
                    {
                        NoteModel oDataRows = new NoteModel();
                        oDataRows.ID = "0";
                        oDailyNotes.Add(oDataRows);

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
            return oDailyNotes.ToArray();
        }
        public NoteModel GetByNoteID(string sDataID)
        {
            try
            {

                List<Parameter> oParams = new List<Parameter>();
                oParams.Add(new Parameter("sDataID", sDataID));
                DataTable dt = _dbContext.LoadDataByProcedure("sps_DailyNotes_Select", oParams);
                if (dt != null && dt.Rows.Count > 0)
                {

                    NoteModel note = new NoteModel
                    {
                        ID = dt.Rows[0]["ID"].ToString(),
                        CreatedDT = Convert.ToDateTime(dt.Rows[0]["CreatedDT"]).ToString("yyyy-MM-dd HH:mm:ss"),
                        Name = dt.Rows[0]["Name"].ToString(),
                        VehicleNumber = dt.Rows[0]["VehicleNumber"].ToString(),
                        MobileNumber = dt.Rows[0]["MobileNumber"].ToString(),
                        Work = dt.Rows[0]["Work"].ToString(),
                        TotalAmount = dt.Rows[0]["TotalAmount"].ToString(),
                        Awak = dt.Rows[0]["Awak"].ToString(),
                        Jawak = dt.Rows[0]["Jawak"].ToString(),
                        Balance = dt.Rows[0]["Balance"].ToString(),
                        Note = dt.Rows[0]["Note"].ToString()
                    };

                    return note;
                }
                else
                {
                    NoteModel note = new NoteModel();
                    note.ID = "0";

                    return note;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        public int GetTotalCount()
        {
            try
            {
                DataTable dt = _dbContext.LoadDataByProcedure("sps_Notes_GetTotalCount", null);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return (int)dt.Rows[0]["Count"];
                }
                return 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        public bool Create(NoteModel noteModel)
        {
            try
            {
                List<Parameter> oParams = new List<Parameter>
                {
                    new Parameter("sNote", noteModel.Note),
                    new Parameter("sVehicleNumber", noteModel.VehicleNumber),
                    new Parameter("sName", noteModel.Name),
                    new Parameter("sMobileNumber", noteModel.MobileNumber),
                    new Parameter("sAwak", noteModel.Awak),
                    new Parameter("sJawak", noteModel.Jawak),
                    new Parameter("sBalance", noteModel.Balance),
                    new Parameter("sTotalAmount", noteModel.TotalAmount),
                    new Parameter("sWork", noteModel.Work),
                    new Parameter("sCreatedBY", noteModel.CreatedBY)
                };

                _dbContext.LoadDataByProcedure("sps_DailyNotes_Insert", oParams);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        public bool Update(NoteModel noteModel)
        {
            try
            {
                List<Parameter> oParams = new List<Parameter>
                {
                    new Parameter("sNote", noteModel.Note),
                    new Parameter("sVehicleNumber", noteModel.VehicleNumber),
                    new Parameter("sName", noteModel.Name),
                    new Parameter("sMobileNumber", noteModel.MobileNumber),
                    new Parameter("sAwak", noteModel.Awak),
                    new Parameter("sJawak", noteModel.Jawak),
                    new Parameter("sBalance", noteModel.Balance),
                    new Parameter("sTotalAmount", noteModel.TotalAmount),
                    new Parameter("sWork", noteModel.Work),
                    new Parameter("sModifiedBY", noteModel.CreatedBY),
                    new Parameter("sDataID", noteModel.ID)
                };

                _dbContext.LoadDataByProcedure("sps_DailyNotes_Update", oParams);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        public bool Delete(string sDataID)
        {
            try
            {
                //Get invoice id
                List<Parameter> oParams = new List<Parameter>();
                oParams.Add(new Parameter("sDataID", sDataID));
                _dbContext.LoadDataByProcedure("sps_DailyNotes_Delete", oParams);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }

        }
    }
}
