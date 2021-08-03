using Paybook.ServiceLayer.Constants;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace Paybook.DatabaseLayer
{
    public partial class clsCommon
    {
        // private static string strConn = ConfigurationManager.ConnectionStrings["FWT_EPayBook_Connection"].ConnectionString.Replace("#SERVERPATH#", HttpContext.Current.Server.MapPath("~"));
        //private static string strConn = ConfigurationManager.ConnectionStrings["FWT_EPayBook_Connection"].ConnectionString;

        //private static DataTable ToLoad_MySqlDB_ByText(string strQueryText)
        //{
        //    DataTable dt = new DataTable();

        //    MySqlConnection oConn = new MySqlConnection(strConn);
        //    try
        //    {
        //        DataSet dsReturn = new DataSet();
        //        MySqlCommand oComm = new MySqlCommand(strQueryText, oConn);
        //        MySqlDataAdapter oAdapter = new MySqlDataAdapter(oComm);

        //        oComm.CommandType = CommandType.Text;

        //        oConn.Open();
        //        oAdapter.Fill(dsReturn);
        //        if (dsReturn.Tables.Count > 0)
        //            dt = dsReturn.Tables[0].Copy();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        oConn.Close();
        //    }
        //    return dt;
        //}
        //private static DataTable ToLoad_MySqlDB_ByProc(string strQueryName, List<clsParams> oParams)
        //{
        //    DataTable dt = new DataTable();
        //    MySqlConnection oConn = new MySqlConnection(strConn);
        //    try
        //    {
        //        MySqlCommand oComm = new MySqlCommand(strQueryName, oConn);

        //        oComm.CommandType = CommandType.StoredProcedure;
        //        if (oParams != null && oParams.Count() > 0)
        //        {
        //            for (int i = 0; i < oParams.Count(); i++)
        //            {
        //                oComm.Parameters.AddWithValue("" + oParams[i].Key + "", oParams[i].Value);
        //            }
        //        }
        //        oConn.Open();

        //        DataSet dsReturn = new DataSet();
        //        MySqlDataAdapter oAdapter = new MySqlDataAdapter(oComm);
        //        oAdapter.Fill(dsReturn);

        //        if (dsReturn.Tables.Count > 0)
        //            dt = dsReturn.Tables[0].Copy();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        oConn.Close();
        //    }
        //    return dt;
        //}


        //private static DataTable ToLoad_MySqlDB_ByText(string strQueryText)
        //{
        //    DataTable dt = new DataTable();

        //    SqlConnection oConn = new SqlConnection(strConn);
        //    try
        //    {
        //        DataSet dsReturn = new DataSet();
        //        SqlCommand oComm = new SqlCommand(strQueryText, oConn);
        //        SqlDataAdapter oAdapter = new SqlDataAdapter(oComm);

        //        oComm.CommandType = CommandType.Text;

        //        oConn.Open();
        //        oAdapter.Fill(dsReturn);
        //        if (dsReturn.Tables.Count > 0)
        //            dt = dsReturn.Tables[0].Copy();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        oConn.Close();
        //    }
        //    return dt;
        //}
        //private static DataTable ToLoad_MySqlDB_ByProc(string strQueryName, List<clsParams> oParams)
        //{
        //    DataTable dt = new DataTable();
        //    SqlConnection oConn = new SqlConnection(strConn);
        //    try
        //    {
        //        SqlCommand oComm = new SqlCommand(strQueryName, oConn);

        //        oComm.CommandType = CommandType.StoredProcedure;
        //        if (oParams != null && oParams.Count() > 0)
        //        {
        //            for (int i = 0; i < oParams.Count(); i++)
        //            {
        //                oComm.Parameters.AddWithValue("" + oParams[i].Key + "", oParams[i].Value);
        //            }
        //        }
        //        oConn.Open();

        //        DataSet dsReturn = new DataSet();
        //        SqlDataAdapter oAdapter = new SqlDataAdapter(oComm);
        //        oAdapter.Fill(dsReturn);

        //        if (dsReturn.Tables.Count > 0)
        //            dt = dsReturn.Tables[0].Copy();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //    finally
        //    {
        //        oConn.Close();
        //    }
        //    return dt;
        //}


        //public static clsChart[] Count_PaymentInvoice_Chart()
        //{
        //    List<clsChart> oChart = new List<clsChart>();
        //    try
        //    {

        //        int iTotalDays = 7;
        //        DataTable dtInvoice = clsCommon.Invoices_SelectCount();
        //        DataTable dtPayment = clsCommon.Payments_SelectCount();

        //        DataTable dt = new DataTable();
        //        dt.Columns.Add("Date");
        //        dt.Columns.Add("InvoiceCount");
        //        dt.Columns.Add("PaymentCount");
        //        DateTime dTodayDate = Convert.ToDateTime(System.DateTime.Today.ToShortDateString());
        //        int i = 0;
        //        for (i = iTotalDays; i > 0; i--)
        //        {
        //            DataRow dr = dt.NewRow();

        //            DateTime sDate = dTodayDate.AddDays(-i);
        //            string sDay = sDate.Day.ToString();

        //            dr["Date"] = sDay;

        //            if (dtInvoice != null && dtInvoice.Rows.Count > 0)
        //            {
        //                foreach (DataRow drInvoice in dtInvoice.Rows)
        //                {
        //                    if (Convert.ToInt16(drInvoice["DateInvoice"]) == Convert.ToInt16(dr["Date"]))
        //                    {
        //                        dr["InvoiceCount"] = drInvoice["IDCount"].ToString();
        //                        break;
        //                    }
        //                    else
        //                        dr["InvoiceCount"] = "0";
        //                }
        //            }
        //            else
        //            {
        //                dr["InvoiceCount"] = "0";
        //            }

        //            if (dtPayment != null && dtPayment.Rows.Count > 0)
        //            {
        //                foreach (DataRow drpayment in dtPayment.Rows)
        //                {
        //                    if (Convert.ToInt16(drpayment["DatePayment"]) == Convert.ToInt16(dr["Date"]))
        //                    {
        //                        dr["PaymentCount"] = drpayment["IDCount"].ToString();
        //                        break;
        //                    }
        //                    else
        //                        dr["PaymentCount"] = "0";
        //                }

        //            }
        //            else
        //            {
        //                dr["PaymentCount"] = "0";
        //            }
        //            dt.Rows.Add(dr);

        //        }
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow dr in dt.Rows)
        //            {
        //                clsChart oDataRows = new clsChart();
        //                oDataRows.Date = dr["Date"].ToString();
        //                oDataRows.PaymentCount = dr["PaymentCount"].ToString() == " " ? "0" : dr["PaymentCount"].ToString();
        //                oDataRows.InvoiceCount = dr["InvoiceCount"].ToString();
        //                oChart.Add(oDataRows);
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        clsChart oDataRows = new clsChart();
        //        oDataRows.ERROR = ex.Message;
        //        oChart.Add(oDataRows);
        //    }
        //    return oChart.ToArray();
        //}
       



        public static void GetPageRange(Double dGridPageNumber, Double dRowTotal, out Double dPageNumber_Start, out Double dPageNumber_End)
        {
            Double RowCount = 10;
            if (dGridPageNumber == -1)
            {

                if (Math.Floor(dRowTotal / RowCount) != Math.Ceiling(dRowTotal / RowCount))

                    dGridPageNumber = (dRowTotal / RowCount) - 1;
                else
                    dGridPageNumber = dRowTotal / RowCount;

            }

            dPageNumber_Start = dGridPageNumber * RowCount + 1;
            dPageNumber_End = dGridPageNumber * RowCount + RowCount;
        }
        //DAshboard


        //Customer
        //Agency     

        //Agents

        //Category
        //Invoice
        //Advance

        //Activity     


        //Company Profile
        //remark
        //daily notes

    }
}
