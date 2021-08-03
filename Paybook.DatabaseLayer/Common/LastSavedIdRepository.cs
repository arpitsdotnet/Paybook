using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.DatabaseLayer.Common
{
    public interface ILastSavedIdRepository
    {
        DataTable GetByType(string type);
        void Update(string sID, string type);
    }

    public class LastSavedIdRepository : ILastSavedIdRepository
    {
        private readonly IDbContext _dbContext;

        public LastSavedIdRepository()
        {
            _dbContext = DbContextFactory.Instance;
        }

        public DataTable GetByType(string type)
        {
            try
            {
                List<Parameter> parameters = new List<Parameter>();
                parameters.Add(new Parameter("sType", type));

                return _dbContext.LoadDataByProcedure("sps_LastSavedID_Select", parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void Update(string sID, string type)
        {
            try
            {
                string[] sValue = sID.Split('_');
                string lastNumber = sValue[2];
                string year = sValue[1];

                List<Parameter> oParams = new List<Parameter>();
                oParams.Add(new Parameter("sModifiedBY", "Administrator"));
                oParams.Add(new Parameter("sType", type));
                oParams.Add(new Parameter("sYear", year));
                oParams.Add(new Parameter("sLastNumber", lastNumber));

                _dbContext.LoadDataByProcedure("sps_LastSavedID_Update", oParams);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
