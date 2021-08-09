using Paybook.BusinessLayer.Agency;
using Paybook.BusinessLayer.Business;
using Paybook.BusinessLayer.Client;
using Paybook.DatabaseLayer.Report;
using Paybook.ServiceLayer;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Paybook.BusinessLayer.Report
{
    public interface IReportProcessor
    {
        ReportModel[] GenrateReport(int businessId, int clientId, string sPaymentDateTo, string sPaymentDateFrom, string sRemainingAmount);
        ReportModel[] GenrateReportForAgency(int businessId, int agencyId, string sPaymentDateTo, string sPaymentDateFrom, string sRemainingAmount);
        DataTable InvoicePayment_AgencyReport_Select(string sPaymentDateTo, string sPaymentDateFrom, string sAgencyID);
        DataTable InvoicePayment_CustomerReport_Select(string sPaymentDateTo, string sPaymentDateFrom, string sCustomer_ID);
        DataTable RemainingAmount_BeforeFromDate_Select(string sPaymentDateFrom, string sCustomer_ID, string sAgency_ID);
        string ReportVersionNumber_Select(string sCustomer_ID);
        string ReportVersion_Insert(string iVersionNumber, string sCustomer_ID, string sPaymentDateFrom, string sPaymentDateTo, string sFilePath);
        string GetLastFileNameByDateAndCustomerID(string sPaymentDateTo, string sPaymentDateFrom, string sCustomer_ID);
    }

    public class ReportProcessor : IReportProcessor
    {
        private readonly ILogger _logger;
        private readonly IReportRepository _reportRepository;
        private readonly IClientProcessor _clientProcessor;
        private readonly IBusinessProcessor _businessProcessor;
        private readonly IAgencyProcessor _agencyProcessor;

        public ReportProcessor()
        {
            _logger = LoggerFactory.Instance;
            _reportRepository = new ReportRepository();

            _clientProcessor = new ClientProcessor();
            _businessProcessor = new BusinessProcessor();
            _agencyProcessor = new AgencyProcessor();
        }

        public ReportModel[] GenrateReport(int businessId, int clientId, string sPaymentDateTo, string sPaymentDateFrom, string sRemainingAmount)
        {
            List<ReportModel> oReport = new List<ReportModel>();
            try
            {
                ReportModel oDataRows = new ReportModel();
                sPaymentDateTo = Convert.ToDateTime(sPaymentDateTo).ToString("yyyy-MM-dd");
                sPaymentDateFrom = Convert.ToDateTime(sPaymentDateFrom).ToString("yyyy-MM-dd");
                string sTodayDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
                string SelectedFileName = GetLastFileNameByDateAndCustomerID(sPaymentDateTo, sPaymentDateFrom, "");
                if (SelectedFileName != "" && sTodayDate != sPaymentDateTo)
                {
                    oDataRows.FilePath = _FolderPath.DOC_DocumentsPath + SelectedFileName;
                    oDataRows.FileName = SelectedFileName;
                    oDataRows.Message = XmlProcessor.ReadXmlFile("INW301");
                    oReport.Add(oDataRows);
                }

                else
                {
                    #region Calculation for Report
                    double dPaymentTotal = 0.00, dRemainingAmountBeforeFromDate = 0.00;
                    double dRemaining = 0.00, dAmount = 0.00, dPaid = 0.00, dAdvance = 0.00;

                    //Get the Invoice,Payment and Advance Amount entries within given date
                    DataTable dt_InvoicePayment = new DataTable();
                    dt_InvoicePayment = InvoicePayment_CustomerReport_Select(sPaymentDateTo, sPaymentDateFrom, "");


                    //Calculate  Payment total,Advance and InvoiceTotal for RemainingAmount                        
                    if (dt_InvoicePayment != null && dt_InvoicePayment.Rows.Count > 0)
                    {
                        dt_InvoicePayment = dt_InvoicePayment.AsEnumerable().OrderBy(x => x[0]).CopyToDataTable();

                        foreach (DataRow drInvoicePayment in dt_InvoicePayment.Rows)
                        {
                            string sType = drInvoicePayment["Type"].ToString().Split('/')[0];
                            if (sType == "Payment")
                            {
                                dPaymentTotal += Convert.ToDouble(drInvoicePayment["Amount"]);
                            }

                            else if (sType == "Invoice")
                                dRemaining += Convert.ToDouble(drInvoicePayment["Amount"]);

                            else if (sType == "Advance")
                                dAdvance += Convert.ToDouble(drInvoicePayment["Amount"]);
                        }
                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Date");
                        dt.Columns.Add("Particular");
                        dt.Columns.Add("VehicleNo");
                        dt.Columns.Add("WorkType");
                        dt.Columns.Add("EntityType");
                        dt.Columns.Add("BasicAmount");
                        dt.Columns.Add("Tax");
                        dt.Columns.Add("TaxAmount");
                        dt.Columns.Add("Amount");
                        dt.Columns.Add("Type");
                        DataRow dr = dt.NewRow();
                        dr["Date"] = DateTime.Now;
                        dr["Particular"] = "-";
                        dr["VehicleNo"] = "-";
                        dr["WorkType"] = "-";
                        dr["EntityType"] = "-";
                        dr["Amount"] = "0.00";
                        dr["Tax"] = "0.00";
                        dr["TaxAmount"] = "0.00";
                        dr["BasicAmount"] = "0.00";
                        dr["Type"] = "-";
                        dt.Rows.Add(dr);

                        dt_InvoicePayment = dt.Copy();
                    }

                    //get Remaining Amount Before FromDate
                    DataTable dt_RemainingAmount_BeforeFromDate = RemainingAmount_BeforeFromDate_Select(sPaymentDateFrom, "", "");
                    if (dt_RemainingAmount_BeforeFromDate != null && dt_RemainingAmount_BeforeFromDate.Rows.Count > 0)
                    {
                        dAmount = Convert.ToDouble(dt_RemainingAmount_BeforeFromDate.Rows[0]["Amount"]) + dAmount;
                        dPaid = Convert.ToDouble(dt_RemainingAmount_BeforeFromDate.Rows[0]["Paid"]) + dPaid;

                        dRemainingAmountBeforeFromDate = dAmount - dPaid;
                    }
                    //

                    //Fill the datatable dtPaymentTotal with TotalPayment,TotalBalance and TotalRemainingBalance and RemainingAmountBeforeFromDate
                    DataTable dt_PaymentTotal = new DataTable();
                    dt_PaymentTotal.Columns.Add("TotalPayment", typeof(Double));
                    dt_PaymentTotal.Columns.Add("RemainingAmountBeforeFromDate", typeof(Double));
                    dt_PaymentTotal.Columns.Add("TotalBalance", typeof(Double));
                    dt_PaymentTotal.Columns.Add("TotalRemainingBalance", typeof(Double));
                    dt_PaymentTotal.Columns.Add("DateFrom");
                    dt_PaymentTotal.Columns.Add("DateTo");

                    DataRow drPaymentTotal = dt_PaymentTotal.NewRow();
                    double dTotalPayment = 0.00;
                    if (dAdvance > dPaymentTotal)
                    {
                        dTotalPayment = dPaymentTotal + (dAdvance - dPaymentTotal);
                        drPaymentTotal["TotalPayment"] = dTotalPayment;
                    }
                    else
                    {
                        dTotalPayment = dPaymentTotal;
                        drPaymentTotal["TotalPayment"] = dTotalPayment;
                    }

                    drPaymentTotal["RemainingAmountBeforeFromDate"] = dRemainingAmountBeforeFromDate;

                    double dTotalBalance = dRemaining + dRemainingAmountBeforeFromDate;
                    drPaymentTotal["TotalBalance"] = dTotalBalance;

                    double dTotalRemainingBalance = dTotalBalance - dTotalPayment;
                    drPaymentTotal["TotalRemainingBalance"] = dTotalRemainingBalance;
                    drPaymentTotal["DateFrom"] = Convert.ToDateTime(sPaymentDateFrom).ToString("dd-MM-yyyy");
                    drPaymentTotal["DateTo"] = Convert.ToDateTime(sPaymentDateTo).ToString("dd-MM-yyyy");
                    dt_PaymentTotal.Rows.Add(drPaymentTotal);
                    #endregion

                    //

                    //get Company and customer Information
                    BusinessModel business = _businessProcessor.GetByUserId(businessId);

                    DataTable dt_CompanyProfile = new DataTable();
                    dt_CompanyProfile.Columns.Add("CustomerName");
                    dt_CompanyProfile.Columns.Add("CustomerAddress");

                    ClientModel customers = _clientProcessor.GetById(businessId, clientId);
                    if (customers != null)
                    {
                        dt_CompanyProfile.Rows[0]["CustomerName"] = customers.Name + "( " + customers.PhoneNumber1 + ")";
                        dt_CompanyProfile.Rows[0]["CustomerAddress"] = customers.AddressComplete;
                    }


                    // Create Invoice/Payment Receipt Report
                    string sbFileWrite = InvoiceHtmlReportGenerate(dt_InvoicePayment, dt_PaymentTotal, dTotalPayment, dt_CompanyProfile, business.AddressComplete);
                    string sPath = "";
                    if (SelectedFileName == "")
                    {
                        //Save html Report
                        // string sDate = System.DateTime.Today.ToString("yyyyMMdd");
                        //string sVersionNumber = ReportVersionNumber_Select(sCustomer_ID);
                        //int iVersionNumber = sVersionNumber == "" ? 1 : Convert.ToInt32(sVersionNumber) + 1;
                        //string sDateFrom = sPaymentDateFrom.Replace("-", "");
                        //string sDateTo = sPaymentDateTo.Replace("-", "");
                        //string sID = sCustomer_ID.Replace("_", "");
                        //string sNewFileName = sID + "_v" + iVersionNumber + "_" + sDateFrom + "_" + sDateTo + ".html";
                        //sPath = Path.Combine(HttpRuntime.AppDomainAppPath, _FolderPath.DOC_DocumentsPath);
                        //File.WriteAllText(sPath + sNewFileName, sbFileWrite.ToString());

                        ////Update Version Number and Insert Version Information
                        //ReportVersion_Insert(iVersionNumber.ToString(), sCustomer_ID, sPaymentDateFrom, sPaymentDateTo, sNewFileName);
                        //oDataRows.FilePath = _FolderPath.DOC_DocumentsPath + sNewFileName;
                        //oDataRows.FileName = sNewFileName;
                        //oReport.Add(oDataRows);
                    }
                    else
                    {
                        //replace html Report
                        sPath = Path.Combine(HttpRuntime.AppDomainAppPath, _FolderPath.DOC_DocumentsPath);
                        File.Delete(sPath + SelectedFileName);
                        File.WriteAllText(sPath + SelectedFileName, sbFileWrite.ToString());
                        oDataRows.FilePath = _FolderPath.DOC_DocumentsPath + SelectedFileName;
                        oDataRows.FileName = SelectedFileName;
                        oReport.Add(oDataRows);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                ReportModel oDataRows = new ReportModel();
                oDataRows.ERROR = ex.Message;
                oReport.Add(oDataRows);
            }
            return oReport.ToArray();
        }

        public ReportModel[] GenrateReportForAgency(int businessId, int agencyId, string sPaymentDateTo, string sPaymentDateFrom, string sRemainingAmount)
        {

            List<ReportModel> reports = new List<ReportModel>();
            try
            {
                ReportModel report = new ReportModel();
                sPaymentDateTo = Convert.ToDateTime(sPaymentDateTo).ToString("yyyy-MM-dd").ToString();
                sPaymentDateFrom = Convert.ToDateTime(sPaymentDateFrom).ToString("yyyy-MM-dd").ToString();
                string sTodayDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
                string SelectedFileName = GetLastFileNameByDateAndCustomerID(sPaymentDateTo, sPaymentDateFrom, "");
                if (SelectedFileName != "" && sTodayDate != sPaymentDateTo)
                {
                    report.FilePath = _FolderPath.DOC_DocumentsPath + SelectedFileName;
                    report.FileName = SelectedFileName;
                    report.Message = XmlProcessor.ReadXmlFile("INW301");
                    reports.Add(report);
                }

                else
                {
                    #region Calculation for Report
                    double dPaymentTotal = 0.00, dRemainingAmountBeforeFromDate = 0.00;
                    double dRemaining = 0.00, dAmount = 0.00, dPaid = 0.00, dAdvance = 0.00;

                    //Get the Invoice,Payment and Advance Amount entries within given date
                    DataTable dt_InvoicePayment = new DataTable();
                    dt_InvoicePayment = InvoicePayment_AgencyReport_Select(sPaymentDateTo, sPaymentDateFrom, "");


                    //Calculate  Payment total,Advance and InvoiceTotal for RemainingAmount               
                    if (dt_InvoicePayment != null && dt_InvoicePayment.Rows.Count > 0)
                    {
                        //
                        dt_InvoicePayment = dt_InvoicePayment.AsEnumerable().OrderBy(x => x[0]).CopyToDataTable();

                        foreach (DataRow drInvoicePayment in dt_InvoicePayment.Rows)
                        {
                            string sType = drInvoicePayment["Type"].ToString().Split('/')[0];
                            if (sType == "Payment")
                                dPaymentTotal += Convert.ToDouble(drInvoicePayment["Amount"]);

                            else if (sType == "Invoice")
                                dRemaining += Convert.ToDouble(drInvoicePayment["Amount"]);

                            else if (sType == "Advance")
                                dAdvance += Convert.ToDouble(drInvoicePayment["Amount"]);
                        }
                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Date");
                        dt.Columns.Add("Particular");
                        dt.Columns.Add("CustomerName");
                        dt.Columns.Add("WorkType");
                        dt.Columns.Add("EntityType");
                        dt.Columns.Add("BasicAmount");
                        dt.Columns.Add("Tax");
                        dt.Columns.Add("TaxAmount");
                        dt.Columns.Add("Amount");
                        dt.Columns.Add("Type");
                        DataRow dr = dt.NewRow();
                        dr["Date"] = DateTime.Now;
                        dr["Particular"] = "-";
                        dr["CustomerName"] = "-";
                        dr["WorkType"] = "-";
                        dr["EntityType"] = "-";
                        dr["Amount"] = "0.00";
                        dr["Tax"] = "0.00";
                        dr["TaxAmount"] = "0.00";
                        dr["BasicAmount"] = "0.00";
                        dr["Type"] = "-";
                        dt.Rows.Add(dr);

                        dt_InvoicePayment = dt.Copy();
                    }

                    //get Remaining Amount Before FromDate
                    DataTable dt_RemainingAmount_BeforeFromDate = RemainingAmount_BeforeFromDate_Select(sPaymentDateFrom, "NONE", "");//NONE for Customerid
                    if (dt_RemainingAmount_BeforeFromDate != null && dt_RemainingAmount_BeforeFromDate.Rows.Count > 0)
                    {
                        dAmount = Convert.ToDouble(dt_RemainingAmount_BeforeFromDate.Rows[0]["Amount"]) + dAmount;
                        dPaid = Convert.ToDouble(dt_RemainingAmount_BeforeFromDate.Rows[0]["Paid"]) + dPaid;
                        dRemainingAmountBeforeFromDate = dAmount - dPaid;
                    }
                    //

                    //Fill the datatable dtPaymentTotal with TotalPayment,TotalBalance and TotalRemainingBalance and RemainingAmountBeforeFromDate
                    DataTable dt_PaymentTotal = new DataTable();
                    dt_PaymentTotal.Columns.Add("TotalPayment", typeof(double));
                    dt_PaymentTotal.Columns.Add("RemainingAmountBeforeFromDate", typeof(double));
                    dt_PaymentTotal.Columns.Add("TotalBalance", typeof(double));
                    dt_PaymentTotal.Columns.Add("TotalRemainingBalance", typeof(double));
                    dt_PaymentTotal.Columns.Add("DateFrom");
                    dt_PaymentTotal.Columns.Add("DateTo");

                    DataRow drPaymentTotal = dt_PaymentTotal.NewRow();
                    double dTotalPayment = 0.00;
                    if (dAdvance > dPaymentTotal)
                    {
                        dTotalPayment = dPaymentTotal + (dAdvance - dPaymentTotal);
                        drPaymentTotal["TotalPayment"] = dTotalPayment;
                    }
                    else
                    {
                        dTotalPayment = dPaymentTotal;
                        drPaymentTotal["TotalPayment"] = dTotalPayment;
                    }

                    drPaymentTotal["RemainingAmountBeforeFromDate"] = dRemainingAmountBeforeFromDate;

                    double dTotalBalance = dRemaining + dRemainingAmountBeforeFromDate;
                    drPaymentTotal["TotalBalance"] = dTotalBalance;

                    double dTotalRemainingBalance = dTotalBalance - dTotalPayment;
                    drPaymentTotal["TotalRemainingBalance"] = dTotalRemainingBalance;
                    drPaymentTotal["DateFrom"] = Convert.ToDateTime(sPaymentDateFrom).ToString("dd-MM-yyyy");
                    drPaymentTotal["DateTo"] = Convert.ToDateTime(sPaymentDateTo).ToString("dd-MM-yyyy");
                    dt_PaymentTotal.Rows.Add(drPaymentTotal);
                    #endregion

                    //

                    //get Company and customer Information
                    BusinessModel business = _businessProcessor.GetByUserId(businessId);

                    DataTable agenctTable = new DataTable();
                    agenctTable.Columns.Add("AgencyName");
                    agenctTable.Columns.Add("AgencyAddress");

                    AgencyModel dtAgency = _agencyProcessor.GetById(businessId, agencyId);
                    agenctTable.Rows[0]["AgencyName"] = "";
                    agenctTable.Rows[0]["AgencyAddress"] = "";
                    if (dtAgency != null)
                    {
                        agenctTable.Rows[0]["AgencyName"] = dtAgency.Name;
                        agenctTable.Rows[0]["AgencyAddress"] = dtAgency.AddressComplete;
                    }

                    // Create Invoice/Payment Receipt
                    string sOriginalFilePath = Path.Combine(HttpRuntime.AppDomainAppPath, _FolderPath.DOC_DocumentsHTMLPath + "report_agency.html");
                    StreamReader srFileRead = new StreamReader(sOriginalFilePath);

                    StringBuilder sbFileWrite = new StringBuilder();
                    string strLine = "";
                    while ((strLine = srFileRead.ReadLine()) != null)
                    {
                        strLine = strLine.Trim();
                        if (strLine == "#COMPANY_NAME#")
                        {
                            sbFileWrite.Append(strLine.Replace("#COMPANY_NAME#", business.Name));
                        }
                        else if (strLine == "#COMPANY_LOGO#")
                        {

                            string sCompanyLogo_Path = "../" + _FolderPath.CompanyLogo_Path + business.Image;
                            sbFileWrite.Append(strLine.Replace("#COMPANY_LOGO#", "<img src=\"" + sCompanyLogo_Path + "\" alt=\"Company Logo\"class=\"CompanyLogo\">"));
                        }
                        else if (strLine == "#ADDRESS#")
                        {
                            sbFileWrite.Append(strLine.Replace("#ADDRESS#", business.AddressComplete));
                        }
                        else if (strLine == "#EMAIL#")
                        {
                            sbFileWrite.Append(strLine.Replace("#EMAIL#", "<b>Email:</b> " + business.Email));
                        }
                        else if (strLine == "#PHONE_NUMBER#")
                        {
                            sbFileWrite.Append(strLine.Replace("#PHONE_NUMBER#", "<b>Ph.No:</b> " + business.PhoneNumber));
                        }
                        //else if (strLine == "#GSTIN#")
                        //{
                        //    sbFileWrite.Append(strLine.Replace("#GSTIN#", "<b>GSTIN#:</b> " + dt_CompanyProfile.Rows[0]["GSTIN"].ToString()));
                        //}
                        //customer information
                        else if (strLine == "#AGENCY_NAME#")
                        {
                            sbFileWrite.Append(strLine.Replace("#AGENCY_NAME#", string.IsNullOrEmpty(dtAgency.Name) ? " " : "<b>Agency:</b> " + dtAgency.Name));
                        }
                        else if (strLine == "#AGENCY_ADDRESS#")
                        {
                            sbFileWrite.Append(strLine.Replace("#AGENCY_ADDRESS#", dtAgency.AddressComplete));
                        }

                        else if (strLine == "#DATE_FROM_TO#")
                        {
                            sbFileWrite.Append(strLine.Replace("#DATE_FROM_TO#", "<b>Date From:</b> " + dt_PaymentTotal.Rows[0]["DateFrom"].ToString() + " To: " + dt_PaymentTotal.Rows[0]["DateTo"].ToString()));
                        }
                        else if (strLine == "#REMAINING_BALANCE#")
                        {
                            sbFileWrite.Append(strLine.Replace("#REMAINING_BALANCE#", "<b>Remaining Balance:</b> " + "&#8377; " + string.Format("{0:0.00}", dt_PaymentTotal.Rows[0]["RemainingAmountBeforeFromDate"])));
                        }
                        else if (strLine == "#idPayment#")
                        {
                            if (dt_InvoicePayment != null && dt_InvoicePayment.Rows.Count > 0)
                            {
                                double dBasicAmount = 0.00, dTaxAmount = 0.00, dTotalAmount = 0.00;
                                string sAmountWithStyle = "", sAmount = "";
                                for (int i = 0; i < dt_InvoicePayment.Rows.Count; i++)
                                {
                                    string sPaymentRow = "<tr id=\"idParticulars\"><td style=\"padding-left:4px;\">#DATE#</td><td style=\"word-break: break-all;padding-left:4px;\">#PARTICULAR#</td><td style=\"padding-left:4px;\">#CUSTOMER#</td><td style=\"padding-left:4px;\">#WORKTYPE#</td><td style=\"padding-left:4px;\">#ENTITY#</td><td style=\"padding-left:4px;\">#TAX#</td><td style=\"text-align: right;padding-right:4px;\">#AMOUNT#</td></tr>";

                                    sPaymentRow = sPaymentRow.Replace("#DATE#", Convert.ToDateTime(dt_InvoicePayment.Rows[i]["Date"]).ToString("dd-MMM HH:MM tt"));
                                    sPaymentRow = sPaymentRow.Replace("#PARTICULAR#", dt_InvoicePayment.Rows[i]["Particular"].ToString());
                                    sPaymentRow = sPaymentRow.Replace("#CUSTOMER#", dt_InvoicePayment.Rows[i]["CustomerName"].ToString());
                                    sPaymentRow = sPaymentRow.Replace("#WORKTYPE#", dt_InvoicePayment.Rows[i]["WorkType"].ToString());
                                    sPaymentRow = sPaymentRow.Replace("#ENTITY#", dt_InvoicePayment.Rows[i]["EntityType"].ToString());
                                    sPaymentRow = sPaymentRow.Replace("#TAX#", dt_InvoicePayment.Rows[i]["Tax"].ToString());

                                    dBasicAmount = Convert.ToDouble(dt_InvoicePayment.Rows[i]["BasicAmount"]);
                                    dTaxAmount = Convert.ToDouble(dt_InvoicePayment.Rows[i]["TaxAmount"]);
                                    dTotalAmount = Convert.ToDouble(dt_InvoicePayment.Rows[i]["Amount"]);
                                    // sPaymentRow = sPaymentRow.Replace("#BASIC_AMOUNT#", sBasicAmount);

                                    if (dt_InvoicePayment.Rows[i]["Type"].ToString() == "Advance")
                                    {
                                        sAmountWithStyle = "<span style=\"color:#2E9AB6; font-weight: bold;\">&#8377; " + string.Format("{0:0.00}", dTotalAmount) + "</span>";
                                        sAmount = sAmountWithStyle;
                                    }
                                    else if (dt_InvoicePayment.Rows[i]["Type"].ToString() == "Payment")
                                    {
                                        sAmountWithStyle = "<span style=\"color:#1C881B; font-weight: bold;\">&#8377; " + string.Format("{0:0.00}", dTotalAmount) + "</span>";
                                        sAmount = sAmountWithStyle;
                                    }
                                    else
                                    {
                                        sAmountWithStyle = "<span style=\"font-weight: bold;\">&#8377; " + string.Format("{0:0.00}", dTotalAmount) + "</span>";

                                        sAmount = "&#8377; " + string.Format("{0:0.00}", dBasicAmount) + "</br>&#8377; " + string.Format("{0:0.00}", dTaxAmount) + "</br>" + sAmountWithStyle;
                                    }
                                    sPaymentRow = sPaymentRow.Replace("#AMOUNT#", sAmount);
                                    sbFileWrite.Append(sPaymentRow);
                                }
                            }

                        }
                        else if (strLine == "#TOTAL_BALANCE#")
                        {
                            sbFileWrite.Append(strLine.Replace("#TOTAL_BALANCE#", "&#8377; " + string.Format("{0:0.00}", dt_PaymentTotal.Rows[0]["TotalBalance"])));
                        }
                        else if (strLine == "#TOTAL_PAYMENT#")
                        {
                            dTotalPayment = Convert.ToDouble(dt_PaymentTotal.Rows[0]["TotalPayment"]);
                            if (dTotalPayment == 0)
                                sbFileWrite.Append(strLine.Replace("#TOTAL_PAYMENT#", "&#8377; " + string.Format("{0:0.00}", dt_PaymentTotal.Rows[0]["TotalPayment"])));
                            else
                                sbFileWrite.Append(strLine.Replace("#TOTAL_PAYMENT#", "<span style=\"color:#1C881B;\">&#8377; " + string.Format("{0:0.00}", dt_PaymentTotal.Rows[0]["TotalPayment"]) + "</span>"));
                        }
                        else if (strLine == "#TOTAL_REMAINING_AMOUNT#")
                        {
                            sbFileWrite.Append(strLine.Replace("#TOTAL_REMAINING_AMOUNT#", "&#8377; " + string.Format("{0:0.00}", dt_PaymentTotal.Rows[0]["TotalRemainingBalance"])));
                        }
                        else
                        {
                            sbFileWrite.Append(strLine);
                        }
                    }

                    srFileRead.Close();
                    string sPath = "";
                    if (SelectedFileName == "")
                    {
                        //Save html Report
                        // string sDate = System.DateTime.Today.ToString("yyyyMMdd");
                        //string sVersionNumber = ReportVersionNumber_Select(agencyId);
                        //int iVersionNumber = sVersionNumber == "" ? 1 : Convert.ToInt32(sVersionNumber) + 1;
                        //string sDateFrom = sPaymentDateFrom.Replace("-", "");
                        //string sDateTo = sPaymentDateTo.Replace("-", "");
                        //string sID = agencyId;
                        //string sNewFileName = sID + "_v" + iVersionNumber + "_" + sDateFrom + "_" + sDateTo + ".html";
                        //sPath = Path.Combine(HttpRuntime.AppDomainAppPath, _FolderPath.DOC_DocumentsPath);
                        //File.WriteAllText(sPath + sNewFileName, sbFileWrite.ToString());

                        //Update Version Number and Insert Version Information
                        //ReportVersion_Insert(iVersionNumber.ToString(), agencyId, sPaymentDateFrom, sPaymentDateTo, sNewFileName);
                        //report.FilePath = _FolderPath.DOC_DocumentsPath + sNewFileName;
                        //report.FileName = sNewFileName;
                        reports.Add(report);
                    }
                    else
                    {
                        //replace html Report
                        sPath = Path.Combine(HttpRuntime.AppDomainAppPath, _FolderPath.DOC_DocumentsPath);
                        File.Delete(sPath + SelectedFileName);
                        File.WriteAllText(sPath + SelectedFileName, sbFileWrite.ToString());
                        report.FilePath = _FolderPath.DOC_DocumentsPath + SelectedFileName;
                        report.FileName = SelectedFileName;
                        reports.Add(report);
                    }
                }

            }
            catch (Exception ex)
            {
                ReportModel oDataRows = new ReportModel();
                oDataRows.ERROR = ex.Message;
                reports.Add(oDataRows);
            }
            return reports.ToArray();
        }

        public DataTable InvoicePayment_AgencyReport_Select(string sPaymentDateTo, string sPaymentDateFrom, string sAgencyID)
        {
            try
            {
                return _reportRepository.InvoicePayment_AgencyReport_Select(sPaymentDateTo, sPaymentDateFrom, sAgencyID);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }

        public DataTable InvoicePayment_CustomerReport_Select(string sPaymentDateTo, string sPaymentDateFrom, string sCustomer_ID)
        {
            try
            {
                return _reportRepository.InvoicePayment_CustomerReport_Select(sPaymentDateTo, sPaymentDateFrom, sCustomer_ID);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }

        public DataTable RemainingAmount_BeforeFromDate_Select(string sPaymentDateFrom, string sCustomer_ID, string sAgency_ID)
        {
            try
            {
                return _reportRepository.RemainingAmount_BeforeFromDate_Select(sPaymentDateFrom, sCustomer_ID, sAgency_ID);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }

        public string ReportVersionNumber_Select(string sCustomer_ID)
        {
            try
            {
                return _reportRepository.ReportVersionNumber_Select(sCustomer_ID);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }

        public string ReportVersion_Insert(string iVersionNumber, string sCustomer_ID, string sPaymentDateFrom, string sPaymentDateTo, string sFilePath)
        {
            try
            {
                bool result = _reportRepository.ReportVersion_Insert(iVersionNumber, sCustomer_ID, sPaymentDateFrom, sPaymentDateTo, sFilePath);
                if (result)
                    return XmlProcessor.ReadXmlFile("");

                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }

        public string GetLastFileNameByDateAndCustomerID(string sPaymentDateTo, string sPaymentDateFrom, string sCustomer_ID)
        {
            try
            {
                return _reportRepository.GetLastFileNameByDateAndCustomerID(sPaymentDateTo, sPaymentDateFrom, sCustomer_ID);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }

        private static string InvoiceHtmlReportGenerate(DataTable dt_InvoicePayment, DataTable dt_PaymentTotal, double dTotalPayment, DataTable dt_CompanyProfile, string sAddress)
        {
            string sOriginalFilePath = Path.Combine(HttpRuntime.AppDomainAppPath, _FolderPath.DOC_DocumentsHTMLPath + "report.html");
            StreamReader srFileRead = new StreamReader(sOriginalFilePath);
            StringBuilder sbFileWrite = new StringBuilder();

            string strLine = "";
            while ((strLine = srFileRead.ReadLine()) != null)
            {
                strLine = strLine.Trim();
                if (strLine == "#COMPANY_NAME#")
                {
                    sbFileWrite.Append(strLine.Replace("#COMPANY_NAME#", dt_CompanyProfile.Rows[0]["CompanyName"].ToString()));
                }
                else if (strLine == "#COMPANY_LOGO#")
                {

                    string sCompanyLogo_Path = "../" + _FolderPath.CompanyLogo_Path + dt_CompanyProfile.Rows[0]["ImageFileName"].ToString();
                    sbFileWrite.Append(strLine.Replace("#COMPANY_LOGO#", "<img src=\"" + sCompanyLogo_Path + "\" alt=\"Company Logo\"class=\"CompanyLogo\">"));
                }
                else if (strLine == "#ADDRESS#")
                {
                    sbFileWrite.Append(strLine.Replace("#ADDRESS#", sAddress));
                }
                else if (strLine == "#EMAIL#")
                {
                    sbFileWrite.Append(strLine.Replace("#EMAIL#", "<b>Email:</b> " + dt_CompanyProfile.Rows[0]["EMail"].ToString()));
                }
                else if (strLine == "#PHONE_NUMBER#")
                {
                    sbFileWrite.Append(strLine.Replace("#PHONE_NUMBER#", "<b>Ph.No:</b> " + dt_CompanyProfile.Rows[0]["PhoneNumber1"].ToString()));
                }
                else if (strLine == "#FAX_NUMBER#")
                {
                    string sFaxNo = dt_CompanyProfile.Rows[0]["FaxNumber"].ToString();
                    sbFileWrite.Append(strLine.Replace("#FAX_NUMBER#", sFaxNo == "" ? " " : "<b>Fax:</b> " + sFaxNo));
                }
                else if (strLine == "#FAX_NUMBER#")
                {
                    sbFileWrite.Append(strLine.Replace("#FAX_NUMBER#", "<b>Fax:</b> " + dt_CompanyProfile.Rows[0]["FaxNumber"].ToString()));
                }
                //else if (strLine == "#GSTIN#")
                //{
                //    sbFileWrite.Append(strLine.Replace("#GSTIN#", "<b>GSTIN#:</b> " + dt_CompanyProfile.Rows[0]["GSTIN"].ToString()));
                //}
                //customer information
                else if (strLine == "#CUSTOMER#")
                {
                    sbFileWrite.Append(strLine.Replace("#CUSTOMER#", dt_CompanyProfile.Rows[0]["CustomerName"].ToString() == "" ? " " : "<b>Customer:</b> " + dt_CompanyProfile.Rows[0]["CustomerName"].ToString()));
                }
                else if (strLine == "#AGENCY_NAME#")
                {
                    sbFileWrite.Append(strLine.Replace("#AGENCY_NAME#", dt_CompanyProfile.Rows[0]["AgencyName"].ToString() == "" ? " " : "<b>Agency:</b> " + dt_CompanyProfile.Rows[0]["AgencyName"].ToString()));
                }
                else if (strLine == "#CUSTOMER_ADDRESS#")
                {
                    sbFileWrite.Append(strLine.Replace("#CUSTOMER_ADDRESS#", dt_CompanyProfile.Rows[0]["CustomerAddress"].ToString()));
                }

                else if (strLine == "#DATE_FROM_TO#")
                {
                    sbFileWrite.Append(strLine.Replace("#DATE_FROM_TO#", "<b>Date From:</b> " + dt_PaymentTotal.Rows[0]["DateFrom"].ToString() + " To: " + dt_PaymentTotal.Rows[0]["DateTo"].ToString()));
                }
                else if (strLine == "#REMAINING_BALANCE#")
                {
                    sbFileWrite.Append(strLine.Replace("#REMAINING_BALANCE#", "<b>Remaining Balance:</b> " + "&#8377; " + dt_PaymentTotal.Rows[0]["RemainingAmountBeforeFromDate"].ToString()));
                }
                else if (strLine == "#idPayment#")
                {
                    if (dt_InvoicePayment != null && dt_InvoicePayment.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt_InvoicePayment.Rows.Count; i++)
                        {
                            double dBasicAmount = 0.00, dTaxAmount = 0.00, dTotalAmount = 0.00;
                            string sAmountWithStyle = "", sAmount = "";
                            string sPaymentRow = "<tr id=\"idParticulars\"><td style=\"padding-left:4px;\">#DATE#</td><td style=\"width:20%;word-break: break-all;padding-left:4px;\">#PARTICULAR#</td><td style=\"padding-left:4px;\">#VEHICLE_NO#</td><td style=\"padding-left:4px;\">#WORKTYPE#</td><td style=\"padding-left:4px;\">#ENTITY#</td><td style=\"padding-left:4px;\">#TAX#</td><td style=\"text-align: right;padding-right:4px;\">#AMOUNT#</td></tr>";
                            sPaymentRow = sPaymentRow.Replace("#DATE#", Convert.ToDateTime(dt_InvoicePayment.Rows[i]["Date"]).ToString("dd-MMM HH:MM tt"));
                            sPaymentRow = sPaymentRow.Replace("#PARTICULAR#", dt_InvoicePayment.Rows[i]["Particular"].ToString());
                            sPaymentRow = sPaymentRow.Replace("#VEHICLE_NO#", dt_InvoicePayment.Rows[i]["VehicleNo"].ToString());
                            sPaymentRow = sPaymentRow.Replace("#WORKTYPE#", dt_InvoicePayment.Rows[i]["WorkType"].ToString());
                            sPaymentRow = sPaymentRow.Replace("#ENTITY#", dt_InvoicePayment.Rows[i]["EntityType"].ToString());
                            sPaymentRow = sPaymentRow.Replace("#TAX#", dt_InvoicePayment.Rows[i]["Tax"].ToString());
                            dBasicAmount = Convert.ToDouble(dt_InvoicePayment.Rows[i]["BasicAmount"]);
                            dTaxAmount = Convert.ToDouble(dt_InvoicePayment.Rows[i]["TaxAmount"]);
                            dTotalAmount = Convert.ToDouble(dt_InvoicePayment.Rows[i]["Amount"]);

                            if (dt_InvoicePayment.Rows[i]["Type"].ToString() == "Advance")
                            {
                                sAmountWithStyle = "<span style=\"color:#2E9AB6; font-weight: bold;\">&#8377; " + string.Format("{0:0.00}", dTotalAmount) + "</span>";
                                sAmount = sAmountWithStyle;
                            }
                            else if (dt_InvoicePayment.Rows[i]["Type"].ToString() == "Payment")
                            {
                                sAmountWithStyle = "<span style=\"color:#1C881B; font-weight: bold;\">&#8377; " + string.Format("{0:0.00}", dTotalAmount) + "</span>";
                                sAmount = sAmountWithStyle;
                            }
                            else
                            {
                                sAmountWithStyle = "<span style=\"font-weight: bold;\">&#8377; " + string.Format("{0:0.00}", dTotalAmount) + "</span>";

                                sAmount = "&#8377; " + string.Format("{0:0.00}", dBasicAmount) + "</br>&#8377; " + string.Format("{0:0.00}", dTaxAmount) + "</br>" + sAmountWithStyle;
                            }
                            sPaymentRow = sPaymentRow.Replace("#AMOUNT#", sAmount);
                            sbFileWrite.Append(sPaymentRow);
                        }
                    }

                }
                // sPaymentRow = sPaymentRow.Replace("#BASIC_AMOUNT#", sBasicAmount); 
                else if (strLine == "#TOTAL_BALANCE#")
                {
                    sbFileWrite.Append(strLine.Replace("#TOTAL_BALANCE#", "&#8377; " + string.Format("{0:0.00}", dt_PaymentTotal.Rows[0]["TotalBalance"])));
                }
                else if (strLine == "#TOTAL_PAYMENT#")
                {
                    dTotalPayment = Convert.ToDouble(dt_PaymentTotal.Rows[0]["TotalPayment"]);
                    if (dTotalPayment == 0)
                        sbFileWrite.Append(strLine.Replace("#TOTAL_PAYMENT#", "&#8377; " + string.Format("{0:0.00}", dt_PaymentTotal.Rows[0]["TotalPayment"])));
                    else
                        sbFileWrite.Append(strLine.Replace("#TOTAL_PAYMENT#", "<span style=\"color:#1C881B;\">&#8377; " + string.Format("{0:0.00}", dt_PaymentTotal.Rows[0]["TotalPayment"]) + "</span>"));
                }
                else if (strLine == "#TOTAL_REMAINING_AMOUNT#")
                {
                    sbFileWrite.Append(strLine.Replace("#TOTAL_REMAINING_AMOUNT#", "&#8377; " + string.Format("{0:0.00}", dt_PaymentTotal.Rows[0]["TotalRemainingBalance"])));
                }
                else
                {
                    sbFileWrite.Append(strLine);
                }
            }

            srFileRead.Close();
            return sbFileWrite.ToString();
        }
    }
}
