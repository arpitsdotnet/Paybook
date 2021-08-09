using System.Collections.Generic;
using System.Data;

namespace Paybook.DatabaseLayer
{
    public interface IDbContext
    {
        List<T> LoadData<T, U>(string storedProcedure, U parameters);
        int SaveData<T>(string storedProcedure, T parameters);
        int SaveDataOutParam<T, U>(string storedProcedure, T parameters, out U returnVar, DbType outputDbType, string outputVarName);
        int SaveMultipleData<T>(string storedProcedure, List<T> parameters);
        //DataTable LoadDataByProcedure(string strQueryName, List<Parameter> oParams);
        //DataTable LoadDataByQuery(string strQueryText);
    }
}
