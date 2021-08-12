using Paybook.ServiceLayer.Logger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using Dapper;

namespace Paybook.DatabaseLayer
{
    /// <summary>
    /// Purpose:    This class will call the database stored procedures or views [TODO] to access data or to save data.
    /// Created By: Arpit Shrivastava
    /// Created Dt: 20 Dec 2019 04:52
    /// </summary>
    public class SQLDataAccess : IDbContext
    {
        private readonly ILogger _logger;

        public SQLDataAccess()
        {
            _logger = LoggerFactory.Instance;
        }


        private string GetConnectionString()
        {
            string connStringType = HttpContext.Current.Request.Url.ToString().Contains("localhost") == true ? "DEV" : "PROD";
            if (connStringType == "PROD")
                return ConfigurationManager.ConnectionStrings["FWT_PaybookProduction_Connection"].ConnectionString;
            else
                return ConfigurationManager.ConnectionStrings["FWT_Paybook_Connection"].ConnectionString;
        }

        /// <summary>
        ///  Method will select the data from the database based on parameters.
        ///  It will execute the SELECT query into database.
        /// </summary>
        /// <typeparam name="T">Return object of type generics.</typeparam>
        /// <typeparam name="U">Passing parameters object of type generics.</typeparam>
        /// <param name="storedProcedure">Pass stored procedure name.</param>
        /// <param name="parameters">Pass U type of generics object as parameters, type "new { }" for no parameters and U as dynamics.</param>
        /// <returns>Returns list of T type of generics object</returns>
        public List<T> LoadData<T, U>(string storedProcedure, U parameters)
        {
            string sConnectionString = GetConnectionString();

            try
            {
                using (SqlConnection con = new SqlConnection(sConnectionString))
                {
                    List<T> rows = con.Query<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ToList();

                    return rows;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }

        /// <summary>
        /// Method will create, update or delete the data into database based on parameters.
        /// It will execute INSERT, UPDATE, DELETE query into database.
        /// </summary>
        /// <typeparam name="T">Passing parameters object of type generics.</typeparam>
        /// <param name="storedProcedure">Pass stored procedure name.</param>
        /// <param name="parameters">Pass T type of generics object as parameters, type "new { }" for no parameters and T as dynamics.</param>
        /// <returns>Returns an integer about how many rows updated.</returns>
        public int SaveData<T>(string storedProcedure, T parameters)
        {
            string sConnectionString = GetConnectionString();

            try
            {
                using (SqlConnection con = new SqlConnection(sConnectionString))
                {
                    int i = con.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                    return i;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }

        /// <summary>
        /// Method will create, update or delete the data into database based on parameters 
        /// and out parameter result the recent id or message.
        /// It will execute INSERT, UPDATE, DELETE query into database.
        /// </summary>
        /// <typeparam name="T">Passing parameters object of type generics.</typeparam>
        /// <param name="storedProcedure">Pass stored procedure name.</param>
        /// <param name="parameters">Pass T type of generics object as parameters, type "new { }" for no parameters and T as dynamics.</param>
        /// <returns>Returns an integer about how many rows updated.</returns>
        public int SaveDataOutParam<T, U>(string storedProcedure, T parameters, out U returnVar, DbType outputDbType, int? size, string outputVarName)
        {
            string sConnectionString = GetConnectionString();

            try
            {
                var dynamicp = new Dapper.DynamicParameters();
                dynamicp.AddDynamicParams(parameters);

                if (size == null)
                    dynamicp.Add(outputVarName, null, dbType: outputDbType, direction: ParameterDirection.Output);
                else
                    dynamicp.Add(outputVarName, null, dbType: outputDbType, direction: ParameterDirection.Output, size);

                using (SqlConnection con = new SqlConnection(sConnectionString))
                {
                    int i = con.Execute(storedProcedure, dynamicp, commandType: CommandType.StoredProcedure);

                    returnVar = dynamicp.Get<U>(outputVarName);

                    return i;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }

        public int SaveMultipleData<T>(string storedProcedure, List<T> parameters)
        {
            string sConnectionString = GetConnectionString();

            SqlTransaction transaction = null;
            try
            {
                using (SqlConnection con = new SqlConnection(sConnectionString))
                {
                    int i = 0;
                    transaction = con.BeginTransaction();
                    foreach (var model in parameters)
                    {
                        i = con.Execute(storedProcedure, model, commandType: CommandType.StoredProcedure);
                        if (i == 0)
                        {
                            transaction.Rollback();
                            return 0;
                        }
                    }
                    transaction.Commit();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }

    }
    //public class SQLDataAccess : IDbContext
    //{
    //    private readonly static string connString = ConfigurationManager.ConnectionStrings["FWT_EPayBook_Connection"].ConnectionString;
    //    public DataTable LoadDataByProcedure(string procName, List<Parameter> parameters)
    //    {
    //        DataSet returnDataset = new DataSet();
    //        SqlConnection connection = new SqlConnection(connString);
    //        try
    //        {
    //            SqlCommand command = new SqlCommand(procName, connection);
    //            command.CommandType = CommandType.StoredProcedure;

    //            if (parameters != null && parameters.Count() > 0)
    //            {
    //                foreach (var param in parameters)
    //                {
    //                    command.Parameters.AddWithValue(param.Key, param.Value);
    //                }
    //            }
    //            connection.Open();

    //            SqlDataAdapter adapter = new SqlDataAdapter(command);
    //            adapter.Fill(returnDataset);
    //        }
    //        catch (Exception)
    //        {
    //            throw;
    //        }
    //        finally
    //        {
    //            connection.Close();
    //        }

    //        if (returnDataset.Tables.Count > 0)
    //            return returnDataset.Tables[0].Copy();
    //        return null;
    //    }


    //    public DataTable LoadDataByQuery(string query)
    //    {
    //        SqlConnection connection = new SqlConnection(connString);
    //        DataSet returnDataset = new DataSet();
    //        try
    //        {
    //            SqlCommand command = new SqlCommand(query, connection);
    //            command.CommandType = CommandType.Text;

    //            connection.Open();

    //            SqlDataAdapter adapter = new SqlDataAdapter(command);
    //            adapter.Fill(returnDataset);
    //        }
    //        catch (Exception)
    //        {
    //            throw;
    //        }
    //        finally
    //        {
    //            connection.Close();
    //        }

    //        if (returnDataset.Tables.Count > 0)
    //            return returnDataset.Tables[0].Copy();
    //        return null;
    //    }
    //}
}
