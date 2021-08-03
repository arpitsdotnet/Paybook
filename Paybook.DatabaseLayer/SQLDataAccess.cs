using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Paybook.DatabaseLayer
{
    public class SQLDataAccess : IDbContext
    {
        private static string connString = ConfigurationManager.ConnectionStrings["FWT_EPayBook_Connection"].ConnectionString;
        public DataTable LoadDataByProcedure(string strQueryName, List<Parameter> oParams)
        {
            DataTable dt = new DataTable();
            SqlConnection oConn = new SqlConnection(connString);
            try
            {
                SqlCommand oComm = new SqlCommand(strQueryName, oConn);

                oComm.CommandType = CommandType.StoredProcedure;
                if (oParams != null && oParams.Count() > 0)
                {
                    for (int i = 0; i < oParams.Count(); i++)
                    {
                        oComm.Parameters.AddWithValue("" + oParams[i].Key + "", oParams[i].Value);
                    }
                }
                oConn.Open();

                DataSet dsReturn = new DataSet();
                SqlDataAdapter oAdapter = new SqlDataAdapter(oComm);
                oAdapter.Fill(dsReturn);

                if (dsReturn.Tables.Count > 0)
                    dt = dsReturn.Tables[0].Copy();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                oConn.Close();
            }
            return dt;
        }


        public DataTable LoadDataByQuery(string strQueryText)
        {
            DataTable dt = new DataTable();

            SqlConnection oConn = new SqlConnection(connString);
            try
            {
                DataSet dsReturn = new DataSet();
                SqlCommand oComm = new SqlCommand(strQueryText, oConn);
                SqlDataAdapter oAdapter = new SqlDataAdapter(oComm);

                oComm.CommandType = CommandType.Text;

                oConn.Open();
                oAdapter.Fill(dsReturn);
                if (dsReturn.Tables.Count > 0)
                    dt = dsReturn.Tables[0].Copy();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                oConn.Close();
            }
            return dt;
        }
    }
}
