using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.DatabaseLayer.Setting
{
    public interface IParticularRepository
    {
        ParticularModel[] Particular_IsExist(string sParticular, string sCategory_Core);
    }

    public class ParticularRepository : IParticularRepository
    {
        private readonly IDbContext _dbContext;

        public ParticularRepository()
        {
            _dbContext = DbContextFactory.Instance;
        }



        public ParticularModel[] Particular_IsExist(string sParticular, string sCategory_Core)
        {
            List<ParticularModel> oParticular = new List<ParticularModel>();
            try
            {
                List<Parameter> oParams = new List<Parameter>();
                oParams.Add(new Parameter("sParticular", sParticular));
                oParams.Add(new Parameter("sCategory_Core", sCategory_Core));
                DataTable dt = _dbContext.LoadDataByProcedure("sps_Particular_IsExist", oParams);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ParticularModel oDataRows = new ParticularModel();

                        oDataRows.ParticularCount = dr["ParticularCount"].ToString();
                        oParticular.Add(oDataRows);
                    }
                }
            }
            catch (Exception ex)
            {
                ParticularModel oDataRows = new ParticularModel();
                oDataRows.ERROR = ex.Message;
                oParticular.Add(oDataRows);
            }
            return oParticular.ToArray();
        }
    }
}
