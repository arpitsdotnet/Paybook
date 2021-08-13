using System.Collections.Generic;
using System.Data;

namespace Paybook.DatabaseLayer
{
    public interface IDbContext
    {

        /// <summary>
        ///  Method will select the data from the database based on parameters.
        ///  It will execute the SELECT query into database.
        /// </summary>
        /// <typeparam name="T">Return object of type generics.</typeparam>
        /// <typeparam name="U">Passing parameters object of type generics.</typeparam>
        /// <param name="storedProcedure">Pass stored procedure name.</param>
        /// <param name="parameters">Pass U type of generics object as parameters, type "new { }" for no parameters and U as dynamics.</param>
        /// <returns>Returns list of T type of generics object</returns>
        List<T> LoadData<T, U>(string storedProcedure, U parameters);

        /// <summary>
        /// Method will create, update or delete the data into database based on parameters.
        /// It will execute INSERT, UPDATE, DELETE query into database.
        /// </summary>
        /// <typeparam name="T">Passing parameters object of type generics.</typeparam>
        /// <param name="storedProcedure">Pass stored procedure name.</param>
        /// <param name="parameters">Pass T type of generics object as parameters, type "new { }" for no parameters and T as dynamics.</param>
        /// <returns>Returns an integer about how many rows updated.</returns>
        int SaveData<T>(string storedProcedure, T parameters);

        /// <summary>
        /// Method will create, update or delete the data into database based on parameters 
        /// and out parameter result the recent id or message.
        /// It will execute INSERT, UPDATE, DELETE query into database.
        /// </summary>
        /// <typeparam name="T">Passing parameters object of type generics.</typeparam>
        /// <param name="storedProcedure">Pass stored procedure name.</param>
        /// <param name="parameters">Pass T type of generics object as parameters, type "new { }" for no parameters and T as dynamics.</param>
        /// <returns>Returns an integer about how many rows updated.</returns>
        int SaveDataOutParam<T, U>(string storedProcedure, T parameters, out U returnVar, DbType outputDbType, int? size, string outputVarName);

        /// <summary>
        /// Method will create, update its model and submodel with transactional query
        /// and out parameter result the recent id or message.
        /// It will execute INSERT, UPDATEquery into database.
        /// </summary>
        /// <typeparam name="T">Passing parameters object of type generics.</typeparam>
        /// <typeparam name="U">Passing parameters object of type generics.</typeparam>
        /// <typeparam name="V">Passing parameters object of type generics.</typeparam>
        /// <param name="storedProcedureT">Pass stored procedure name.</param>
        /// <param name="storedProcedureU">Pass stored procedure name.</param>
        /// <param name="model">Pass T type of generics object as parameters, type "new { }" for no parameters and T as dynamics.</param>
        /// <param name="submodel">Pass U type of generics object as parameters, type "new { }" for no parameters and U as dynamics.</param>
        /// <param name="modelIdName">Column Name of T type to be passed on submodel U</param>
        /// <returns>Returns an integer about how many rows updated.</returns>
        int SaveDataWithSubdata<T, U, V>(string storedProcedureT, string storedProcedureU, T model, List<U> submodel, string modelIdName, out V returnVar, DbType outputDbType, int? size, string outputVarName);
        int SaveMultipleData<T>(string storedProcedure, List<T> parameters);
        //DataTable LoadDataByProcedure(string strQueryName, List<Parameter> oParams);
        //DataTable LoadDataByQuery(string strQueryText);
    }
}
