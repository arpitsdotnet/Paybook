using System.Collections.Generic;
using System.Data;

namespace Paybook.DatabaseLayer
{
    public interface IDbContext
    {
        DataTable LoadDataByProcedure(string strQueryName, List<Parameter> oParams);
        DataTable LoadDataByQuery(string strQueryText);
    }
}
