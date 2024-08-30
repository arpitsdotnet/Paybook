using System.Data;
using Paybook.DatabaseLayer.Abstracts.Reports;

namespace Paybook.DatabaseLayer.Features.Reports
{
    public class ReportRepository : IReportRepository
    {
        //private readonly IDbContext _dbContext;

        public ReportRepository()
        {
            //_dbContext = DbContextFactory.Instance;
        }


        //Payment/Invoice Report
        public DataTable InvoicePayment_AgencyReport_Select(string sPaymentDateTo, string sPaymentDateFrom, string sAgencyID)
        {
            DataTable dt = new DataTable();
            //   string sWere = "", dPaymentDateTo = "", dPaymentDateFrom = "", sTax = "";

            //   DataTable dtInvoice, dtAdvance, dtPayment, dtTax;
            //   if (sPaymentDateTo != "" && sPaymentDateFrom != "")
            //   {
            //       string sTime = DateTime.Now.ToString("HH:mm:ss");
            //       sPaymentDateTo = sPaymentDateTo + " " + "23:59:59"; ;
            //       sPaymentDateFrom = sPaymentDateFrom + " " + "00:01:01";
            //       dPaymentDateTo = Convert.ToDateTime(sPaymentDateTo).ToString("yyyy-MM-dd HH:mm:ss").ToString();
            //       dPaymentDateFrom = Convert.ToDateTime(sPaymentDateFrom).ToString("yyyy-MM-dd HH:mm:ss").ToString();
            //       sWere = " BETWEEN (" + "\"" + dPaymentDateFrom + "\"" + ")" + " AND (" + "\"" + dPaymentDateTo + "\"" + ")";
            //   }
            //   //Select all invoices between given date  //here amount is total amount with tax                        
            //   string sQueryInvoice = "SELECT TIN.Invoice_ID,TIN.Particular,TIN.Invoice_Date,TIN.Amount AS Amount,TIN.Invoice_MRP, TMC.SubCategory_Disp AS Category_Disp," +
            //       " concat(if(FirstName is null,'-',FirstName),' ',if(MiddleName is null,'',MiddleName),' ',if(LastName is null,'',LastName ),if (VehicleNo is null,'',concat('(V.NO: ', VehicleNo, ' )'))) as CustomerName" +
            //       " FROM T_Invoices AS TIN LEFT JOIN TM_Categories AS TMC ON TIN.Category_Core = TMC.SubCategory_Core " +
            //       "left join t_customers as TC on TIN.Customer_ID=TC.Customer_ID" +
            //       " WHERE TIN.IsActive=1 And TMC.IsActive=1 And TIN.Agency_ID=\"" + sAgencyID + "\" AND Invoice_Date" + sWere + ";";
            //   dtInvoice = _dbContext.LoadDataByQuery(sQueryInvoice);

            //   //Select all Advances between given date
            //   string sQueryAdvance = "SELECT TAP.Customer_ID,TAP.AdvancePayment AS Amount,TAP.AdvancePayment_Date,TAP.Advance_ID,TAP.AdvancePayment_Type, concat(if(FirstName is null,'-',FirstName),' ',if(MiddleName is null,'',MiddleName),' ',if(LastName is null,'',LastName)) as CustomerName" +
            //" FROM t_advance_payments AS TAP" +
            // " left join t_customers as TC on TAP.Customer_ID=TC.Customer_ID" +
            //" WHERE TAP.IsActive=1 And TAP.Agency_ID=\"" + sAgencyID + "\" AND AdvancePayment_Date" + sWere + ";";
            //   dtAdvance = _dbContext.LoadDataByQuery(sQueryAdvance);

            //   //Select all Payments between given date
            //   string sQueryPayment = "SELECT TP.Invoice_ID,TP.Payment_Date,TP.PaymentAmount,TP.ReceiptID,TMC.SubCategory_Disp AS Category_Disp,TMC.SubCategory_Core AS Category_Core,concat(if(FirstName is null,'-',FirstName),' ',if(MiddleName is null,'',MiddleName),' ',if(LastName is null,'',LastName)) as CustomerName" +
            //       " FROM T_Payments AS TP LEFT JOIN TM_Categories AS TMC ON TP.Category_Core = TMC.SubCategory_Core" +
            //        " left join t_customers as TC on TP.Customer_ID=TC.Customer_ID" +
            //" WHERE TP.IsActive=1 And TP.Agency_ID=\"" + sAgencyID + "\" AND TP.Payment_Date " + sWere + ";";
            //   dtPayment = _dbContext.LoadDataByQuery(sQueryPayment);

            //   // select InvoiceTax
            //   string sQueryInvoiceTax = "SELECT TaxType, Percentage,TAX.Amount AS Amount,TAX.Invoice_ID FROM t_invoice_tax as TAX LEFT JOIN " +
            //       "t_invoices as INV on TAX.Invoice_ID = INV.Invoice_ID" +
            //         " WHERE TAX.IsActive = 1 And TAX.Agency_ID =\"" + sAgencyID + "\" AND TAX.Invoice_Date" + sWere + ";";
            //   dtTax = _dbContext.LoadDataByQuery(sQueryInvoiceTax);

            //   dt.Columns.Add("Date", typeof(DateTime));
            //   dt.Columns.Add("Particular");
            //   dt.Columns.Add("CustomerName");
            //   dt.Columns.Add("WorkType");
            //   dt.Columns.Add("EntityType");
            //   dt.Columns.Add("BasicAmount");
            //   dt.Columns.Add("Tax");
            //   dt.Columns.Add("TaxAmount");
            //   dt.Columns.Add("Amount");//its basic amount + tax
            //   dt.Columns.Add("Type");
            //   if (dtInvoice != null && dtInvoice.Rows.Count > 0)
            //   {
            //       foreach (DataRow drInvoice in dtInvoice.Rows)
            //       {
            //           DataRow dr = dt.NewRow();
            //           dr["Date"] = drInvoice["Invoice_Date"].ToString();
            //           dr["Particular"] = drInvoice["Particular"].ToString();
            //           dr["CustomerName"] = drInvoice["CustomerName"].ToString() == "" ? "-" : drInvoice["CustomerName"].ToString();
            //           dr["WorkType"] = drInvoice["Category_Disp"].ToString();
            //           dr["EntityType"] = "Invoice #" + drInvoice["Invoice_ID"].ToString().Replace("_", "");
            //           dr["Amount"] = drInvoice["Amount"].ToString() == "" ? "0" : Convert.ToDouble(drInvoice["Amount"].ToString()).ToString();//0;
            //           dr["BasicAmount"] = drInvoice["Invoice_MRP"].ToString() == "" ? "0.00" : drInvoice["Invoice_MRP"].ToString();//0;
            //           dr["Type"] = "Invoice";
            //           double dTaxAmount = 0;
            //           foreach (DataRow drTax in dtTax.Rows)
            //           {
            //               if (drTax["Invoice_ID"].ToString() == drInvoice["Invoice_ID"].ToString())
            //               {
            //                   sTax = sTax + drTax["TaxType"].ToString() + " " + drTax["Percentage"].ToString() + "%<br>";//0;
            //                   dTaxAmount += Convert.ToDouble(drTax["Amount"].ToString());
            //               }
            //           }
            //           dr["Tax"] = sTax;
            //           dr["TaxAmount"] = dTaxAmount.ToString();

            //           sTax = "";
            //           dt.Rows.Add(dr);
            //       }
            //   }
            //   if (dtAdvance != null && dtAdvance.Rows.Count > 0)
            //   {
            //       foreach (DataRow drAdvance in dtAdvance.Rows)
            //       {
            //           DataRow dr = dt.NewRow();
            //           dr["Date"] = drAdvance["AdvancePayment_Date"].ToString();
            //           dr["Particular"] = "-";
            //           dr["CustomerName"] = drAdvance["CustomerName"].ToString() == "" ? "-" : drAdvance["CustomerName"].ToString();
            //           dr["WorkType"] = "-";
            //           dr["EntityType"] = "Advance #" + drAdvance["Advance_ID"].ToString().Replace("_", "") + " By " + drAdvance["AdvancePayment_Type"].ToString();
            //           dr["BasicAmount"] = "0.00";
            //           dr["Tax"] = "-";
            //           dr["TaxAmount"] = "0.00";
            //           dr["Amount"] = drAdvance["Amount"].ToString() == "" ? "0.00" : Convert.ToDouble(drAdvance["Amount"].ToString()).ToString();
            //           dr["Type"] = "Advance";
            //           dt.Rows.Add(dr);
            //       }
            //   }
            //   if (dtPayment != null && dtPayment.Rows.Count > 0)
            //   {
            //       foreach (DataRow drPayment in dtPayment.Rows)
            //       {
            //           DataRow dr = dt.NewRow();
            //           dr["Date"] = drPayment["Payment_Date"].ToString();
            //           dr["Particular"] = "-";// drPayment["Particular"].ToString();
            //           dr["CustomerName"] = drPayment["CustomerName"].ToString();
            //           dr["WorkType"] = drPayment["Category_Disp"].ToString();
            //           dr["EntityType"] = "Payment #" + drPayment["ReceiptID"].ToString().Replace("_", "") + " for Invoice #" + drPayment["Invoice_ID"].ToString().Replace("_", "");
            //           dr["BasicAmount"] = "0.00";
            //           dr["Tax"] = "-";
            //           dr["TaxAmount"] = "0.00";
            //           dr["Amount"] = drPayment["PaymentAmount"].ToString() == "" ? "0.00" : Convert.ToDouble(drPayment["PaymentAmount"].ToString()).ToString();
            //           dr["Type"] = "Payment";
            //           dt.Rows.Add(dr);
            //       }
            //   }

            return dt;
        }
        public DataTable InvoicePayment_CustomerReport_Select(string sPaymentDateTo, string sPaymentDateFrom, string sCustomer_ID)
        {

            DataTable dt = new DataTable();
            //   string sWere = "", dPaymentDateTo = "", dPaymentDateFrom = "", sTax = "";

            //   DataTable dtInvoice, dtAdvance, dtPayment, dtTax;
            //   if (sPaymentDateTo != "" && sPaymentDateFrom != "")
            //   {
            //       string sTime = DateTime.Now.ToString("HH:mm:ss");
            //       sPaymentDateTo = sPaymentDateTo + " " + "23:59:59"; ;
            //       sPaymentDateFrom = sPaymentDateFrom + " " + "00:01:01";
            //       dPaymentDateTo = Convert.ToDateTime(sPaymentDateTo).ToString("yyyy-MM-dd HH:mm:ss").ToString();
            //       dPaymentDateFrom = Convert.ToDateTime(sPaymentDateFrom).ToString("yyyy-MM-dd HH:mm:ss").ToString();
            //       sWere = " BETWEEN (" + "\"" + dPaymentDateFrom + "\"" + ")" + " AND (" + "\"" + dPaymentDateTo + "\"" + ")";
            //   }
            //   //Select all invoices between given date  //here amount is total amount with tax                        
            //   string sQueryInvoice = "SELECT TIN.Invoice_ID,TIN.Particular,TIN.Invoice_Date,TIN.Amount AS Amount,TIN.Invoice_MRP, TMC.SubCategory_Disp AS Category_Disp,if(VehicleNo is null,'-', VehicleNo) as VehicleNo" +
            //       " FROM T_Invoices AS TIN LEFT JOIN TM_Categories AS TMC ON TIN.Category_Core = TMC.SubCategory_Core WHERE TIN.IsActive=1 And TMC.IsActive=1 And TIN.Customer_ID=\"" + sCustomer_ID + "\" AND Invoice_Date" + sWere + ";";
            //   dtInvoice = _dbContext.LoadDataByQuery(sQueryInvoice);

            //   //Select all Advances between given date
            //   string sQueryAdvance = "SELECT Customer_ID,TAP.AdvancePayment AS Amount,TAP.AdvancePayment_Date,TAP.Advance_ID,TAP.AdvancePayment_Type" +
            // " FROM t_advance_payments AS TAP " + " WHERE IsActive=1 And Customer_ID=\"" + sCustomer_ID + "\" AND AdvancePayment_Date" + sWere + ";";
            //   dtAdvance = _dbContext.LoadDataByQuery(sQueryAdvance);

            //   //Select all Payments between given date
            //   string sQueryPayment = "SELECT TP.Invoice_ID,TP.Payment_Date,TP.PaymentAmount,TP.ReceiptID,TMC.SubCategory_Disp AS Category_Disp,TMC.SubCategory_Core AS Category_Core" +
            //       " FROM T_Payments AS TP LEFT JOIN TM_Categories AS TMC ON TP.Category_Core = TMC.SubCategory_Core" +
            //" WHERE TP.IsActive=1 And TP.Customer_ID=\"" + sCustomer_ID + "\" AND TP.Payment_Date " + sWere + ";";
            //   dtPayment = _dbContext.LoadDataByQuery(sQueryPayment);

            //   // select InvoiceTax
            //   string sQueryInvoiceTax = "SELECT TaxType, Percentage,TAX.Amount AS Amount,TAX.Invoice_ID FROM t_invoice_tax as TAX LEFT JOIN " +
            //       "t_invoices as INV on TAX.Invoice_ID = INV.Invoice_ID" +
            //         " WHERE TAX.IsActive = 1 And TAX.Customer_ID =\"" + sCustomer_ID + "\" AND TAX.Invoice_Date" + sWere + ";";
            //   dtTax = _dbContext.LoadDataByQuery(sQueryInvoiceTax);

            //   dt.Columns.Add("Date");
            //   dt.Columns.Add("Particular");
            //   dt.Columns.Add("VehicleNo");
            //   dt.Columns.Add("WorkType");
            //   dt.Columns.Add("EntityType");
            //   dt.Columns.Add("Amount");//its basic amount + tax
            //   dt.Columns.Add("TAX");
            //   dt.Columns.Add("TaxAmount");
            //   dt.Columns.Add("BasicAmount");
            //   dt.Columns.Add("Type");
            //   if (dtInvoice != null && dtInvoice.Rows.Count > 0)
            //   {
            //       foreach (DataRow drInvoice in dtInvoice.Rows)
            //       {
            //           DataRow dr = dt.NewRow();
            //           dr["Date"] = drInvoice["Invoice_Date"].ToString();
            //           dr["Particular"] = drInvoice["Particular"].ToString();
            //           dr["VehicleNo"] = drInvoice["VehicleNo"].ToString();
            //           dr["WorkType"] = drInvoice["Category_Disp"].ToString();
            //           dr["EntityType"] = "Invoice #" + drInvoice["Invoice_ID"].ToString().Replace("_", "");
            //           dr["Amount"] = drInvoice["Amount"].ToString() == "" ? 0 : Convert.ToDouble(drInvoice["Amount"].ToString());//0;
            //           dr["BasicAmount"] = drInvoice["Invoice_MRP"].ToString() == "" ? 0 : Convert.ToDouble(drInvoice["Invoice_MRP"].ToString());//0;
            //           dr["Type"] = "Invoice";
            //           double dTaxAmount = 0;
            //           foreach (DataRow drTax in dtTax.Rows)
            //           {
            //               if (drTax["Invoice_ID"].ToString() == drInvoice["Invoice_ID"].ToString())
            //               {
            //                   sTax = sTax + drTax["TaxType"].ToString() + " " + drTax["Percentage"].ToString() + "%<br/>";//0;
            //                   dTaxAmount += Convert.ToDouble(drTax["Amount"].ToString());
            //               }
            //           }
            //           dr["Tax"] = sTax;
            //           dr["TaxAmount"] = dTaxAmount.ToString();

            //           sTax = "";
            //           dt.Rows.Add(dr);
            //       }
            //   }
            //   if (dtAdvance != null && dtAdvance.Rows.Count > 0)
            //   {
            //       foreach (DataRow drAdvance in dtAdvance.Rows)
            //       {
            //           DataRow dr = dt.NewRow();
            //           dr["Date"] = drAdvance["AdvancePayment_Date"].ToString();
            //           dr["Particular"] = "-";
            //           dr["VehicleNo"] = "-";
            //           dr["WorkType"] = "-";
            //           dr["EntityType"] = "Advance #" + drAdvance["Advance_ID"].ToString().Replace("_", "") + " By " + drAdvance["AdvancePayment_Type"].ToString();
            //           dr["BasicAmount"] = "0.00";
            //           dr["TAX"] = "0.00";
            //           dr["TaxAmount"] = "0.00";
            //           dr["Amount"] = drAdvance["Amount"].ToString() == "" ? 0 : Convert.ToDouble(drAdvance["Amount"].ToString());
            //           dr["Type"] = "Advance";
            //           dt.Rows.Add(dr);
            //       }
            //   }
            //   if (dtPayment != null && dtPayment.Rows.Count > 0)
            //   {
            //       foreach (DataRow drPayment in dtPayment.Rows)
            //       {
            //           DataRow dr = dt.NewRow();
            //           dr["Date"] = drPayment["Payment_Date"].ToString();
            //           dr["Particular"] = "-";// drPayment["Particular"].ToString();
            //           dr["VehicleNo"] = "-";
            //           dr["WorkType"] = drPayment["Category_Disp"].ToString();
            //           dr["EntityType"] = "Payment #" + drPayment["ReceiptID"].ToString().Replace("_", "") + " for Invoice #" + drPayment["Invoice_ID"].ToString().Replace("_", "");
            //           dr["BasicAmount"] = "0.00";
            //           dr["TAX"] = "0.00";
            //           dr["TaxAmount"] = "0.00";
            //           dr["Amount"] = drPayment["PaymentAmount"].ToString() == "" ? 0 : Convert.ToDouble(drPayment["PaymentAmount"].ToString());
            //           dr["Type"] = "Payment";
            //           dt.Rows.Add(dr);
            //       }
            //   }

            return dt;
        }
        public DataTable RemainingAmount_BeforeFromDate_Select(string sPaymentDateFrom, string sCustomer_ID, string sAgency_ID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Amount");
            dt.Columns.Add("Paid");

            //string sTime = DateTime.Now.ToString("HH:mm:ss");
            //sPaymentDateFrom = sPaymentDateFrom + " " + "00:01:01";// sPaymentDateFrom + " " + sTime;

            //try
            //{
            //    DateTime dPaymentDateFrom = Convert.ToDateTime(sPaymentDateFrom);

            //    // string sMinInvoiceDate = "select count(ID) as RowCount, min(Invoice_Date) As MinInvoiceDate from T_Invoices where Customer_ID=" + "\"" + sCustomer_ID + "\"" + ";";
            //    List<Parameter> oParams = new List<Parameter>();

            //    oParams.Add(new Parameter("sCustomer_ID", sCustomer_ID));
            //    oParams.Add(new Parameter("sAgency_ID", sAgency_ID));
            //    DataTable dtMinInvoiceDate = _dbContext.LoadDataByProcedure("sps_Invoice_SelectMinInvoiceDate", oParams);
            //    // DataTable dtMinInvoiceDate = clsCommon.ToLoad_MySqlDB_ByText(sMinInvoiceDate);
            //    if (dtMinInvoiceDate != null && dtMinInvoiceDate.Rows.Count > 0)
            //    {
            //        if (Convert.ToInt32(dtMinInvoiceDate.Rows[0]["RowCount"]) != 0)
            //        {
            //            if (Convert.ToDateTime(dtMinInvoiceDate.Rows[0]["MinInvoiceDate"]) < dPaymentDateFrom)
            //            {
            //                // Select total invoice amount before FromDate
            //                // string sInvoiceTotalselect_BeforeFromDate = "select if(sum(T_Invoices.amount) is null,0,sum(T_Invoices.amount)) As Amount from T_Invoices where Customer_ID=" + "\"" + sCustomer_ID + "\"" + "And Date(Invoice_Date) < Date(" + "\"" + sPaymentDateFrom + "\"" + ") and IsActive=1 AND InvoiceStatus_Core<>\"IS_CLOSE\";";// AND InvoiceStatus_Core<>\"IS_PAID\"
            //                oParams.Add(new Parameter("sPaymentDateFrom", sPaymentDateFrom));
            //                DataTable invoiceAmounts = _dbContext.LoadDataByProcedure("sps_Invoice_SelectTotal_BeforeFromDate", oParams);

            //                //Select total payment before FromDate which are not fullypaid
            //                //string sPaymentTotalselect_BeforeFromDate = " SELECT (if(SUM(TP.PaymentAmount)>0, SUM(TP.PaymentAmount), 0)) AS Paid FROM ( T_Payments AS TP LEFT JOIN T_Invoices AS TIN ON TIN.Customer_ID=TP.Customer_ID And TIN.Particular = TP.Particular And TIN.Category_Core=TP.Category_Core) WHERE TP.IsActive=1 AND InvoiceStatus_Core<>\"IS_CLOSE\" AND TP.Customer_ID=" + "\"" + sCustomer_ID + "\"" + " AND Date(Payment_Date) < Date(" + "\"" + sPaymentDateFrom + "\"" + ");";//AND InvoiceStatus_Core<>\"IS_PAID\"
            //                DataTable payments = _dbContext.LoadDataByProcedure("sps_Payment_SelectTotal_BeforeFromDate", oParams);

            //                DataRow dr = dt.NewRow();
            //                dr["Amount"] = invoiceAmounts.Rows[0]["Amount"];
            //                dr["Paid"] = payments.Rows[0]["Paid"];
            //                dt.Rows.Add(dr);
            //            }
            //            else
            //            {
            //                DataRow dr = dt.NewRow();
            //                dr["Amount"] = 0.0;
            //                dr["Paid"] = 0.0;
            //                dt.Rows.Add(dr);
            //            }
            //        }
            //        else
            //        {
            //            DataRow dr = dt.NewRow();
            //            dr["Amount"] = 0.0;
            //            dr["Paid"] = 0.0;
            //            dt.Rows.Add(dr);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(_logger.MethodName, ex);
            //    throw;
            //}
            return dt;
        }

        public string GetLastFileNameByDateAndCustomerID(string sPaymentDateTo, string sPaymentDateFrom, string sCustomer_ID)
        {
            //try
            //{
            //    List<Parameter> oParams = new List<Parameter>();
            //    oParams.Add(new Parameter("sPaymentDateTo", sPaymentDateTo));
            //    oParams.Add(new Parameter("sPaymentDateFrom", sPaymentDateFrom));
            //    oParams.Add(new Parameter("sCustomer_ID", sCustomer_ID)); ;
            //    DataTable dt = _dbContext.LoadDataByProcedure("sps_ReportVersion_Select", oParams);
            //    if (dt != null && dt.Rows.Count > 0)
            //    {
            //        return dt.Rows[0]["FilePath"].ToString();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(_logger.MethodName, ex);
            //    throw;
            //}
            return string.Empty;
        }
        public bool ReportVersion_Insert(string iVersionNumber, string sCustomer_ID, string sPaymentDateFrom, string sPaymentDateTo, string sFilePath)
        {
            //string sTime = DateTime.Now.ToString("HH:mm");
            //sPaymentDateTo = sPaymentDateTo + " " + sTime;
            ////sPaymentDateFrom = sPaymentDateFrom + " " + "00:01:01";

            //List<Parameter> oParams = new List<Parameter>
            //{
            //    new Parameter("sVersionNumber", iVersionNumber),
            //    new Parameter("sCustomer_ID", sCustomer_ID)
            //};

            //_dbContext.LoadDataByProcedure("sps_ReportVersionNumber_Update", oParams);

            //oParams.Clear();
            //oParams = new List<Parameter>
            //{
            //    new Parameter("sCustomer_ID", sCustomer_ID),
            //    new Parameter("sVersionNumber", iVersionNumber),
            //    new Parameter("sDateFrom", sPaymentDateFrom),
            //    new Parameter("sDateTo", sPaymentDateTo),
            //    new Parameter("sFilePath", sFilePath),
            //    new Parameter("sCreatedBY", "admin")
            //};

            //_dbContext.LoadDataByProcedure("sps_ReportVersion_Insert", oParams);

            return true;
        }
        public string ReportVersionNumber_Select(string sCustomer_ID)
        {
            //List<Parameter> oParams = new List<Parameter>();
            //oParams.Add(new Parameter("sCustomer_ID", sCustomer_ID));
            //DataTable dt = _dbContext.LoadDataByProcedure("sps_ReportVersionNumber_Select", oParams);
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    return dt.Rows[0]["VersionNumber"].ToString();
            //}
            return string.Empty;
        }

    }
}
