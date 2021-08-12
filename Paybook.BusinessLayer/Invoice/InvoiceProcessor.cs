using Paybook.BusinessLayer.Agency;
using Paybook.BusinessLayer.Client;
using Paybook.BusinessLayer.Common;
using Paybook.BusinessLayer.Setting;
using Paybook.DatabaseLayer;
using Paybook.DatabaseLayer.Invoice;
using Paybook.ServiceLayer;
using Paybook.ServiceLayer.Constants;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Services;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.BusinessLayer.Invoice
{

    public interface IInvoiceProcessor
    {
        string Invoices_Update_CloseStatus(string sParticular, string sCreatedBY, string sCategory_Core, string sStatus_Core, string sReason, string sCustomer_ID);
        string Activity_Insert_Overdue(string sCreatedBY, string sStatus_Core);
        List<InvoiceModel> GetAllByPage(int businessId, int page, string search, string orderBy);
        InvoiceModel GetById(int businessId, int id);
        InvoiceModel Create(InvoiceModel model);
        InvoiceModel Update(InvoiceModel model);
        InvoiceModel Activate(int businessId, int id, bool active);
        InvoiceModel Delete(int businessId, int id);
    }

    public partial class InvoiceProcessor : IInvoiceProcessor
    {
        private readonly ILogger _logger;
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IInvoiceServiceRepository _serviceRepo;
        private readonly ILastSavedNumberProcessor _lastSavedNumberProcessor;
        private readonly ICategoryTypeProcessor _categoryTypeProcessor;
        private readonly ICategoryProcessor _categoryProcessor;
        private readonly IClientProcessor _clientProcessor;
        private readonly IAgencyProcessor _agencyProcessor;

        public InvoiceProcessor()
        {
            _logger = LoggerFactory.Instance;
            _invoiceRepo = new InvoiceRepository();
            _serviceRepo = new InvoiceServiceRepository();
            _lastSavedNumberProcessor = new LastSavedNumberProcessor();
            _categoryTypeProcessor = new CategoryTypeProcessor();
            _categoryProcessor = new CategoryProcessor();
            _clientProcessor = new ClientProcessor();
            _agencyProcessor = new AgencyProcessor();
        }

        public string Invoices_Update_CloseStatus(string sParticular, string sCreatedBY, string sCategory_Core, string sStatus_Core, string sReason, string sCustomer_ID)
        {
            try
            {
                bool result = _invoiceRepo.Invoices_Update_CloseStatus(sParticular, sCreatedBY, sCategory_Core, sStatus_Core, sReason, sCustomer_ID);
                if (result)
                {
                    return XmlProcessor.ReadXmlFile("INS303");
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }

        public string Activity_Insert_Overdue(string sCreatedBY, string sStatus_Core)
        {
            try
            {
                DataTable dt = _invoiceRepo.GetByStatusOverdue();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        InvoiceModel model = new InvoiceModel
                        {

                        };


                        string statusClass = "";

                        switch (model.StatusCategoryMaster.Core)
                        {
                            case InvoiceStatusConst.Overdue:
                                statusClass = ActivityStatusCss.DANGER;
                                break;
                            case InvoiceStatusConst.Paid:
                            case InvoiceStatusConst.PaidPartial:
                                statusClass = ActivityStatusCss.SUCCESS;
                                break;
                            case InvoiceStatusConst.Open:
                                statusClass = ActivityStatusCss.INFO;
                                break;
                            case InvoiceStatusConst.Close:
                                statusClass = ActivityStatusCss.DEFAULT;
                                break;
                            default:
                                statusClass = ActivityStatusCss.DEFAULT;
                                break;
                        }

                        ActivityBuilder activity = new ActivityBuilder()
                            .AddHeader(model.StatusCategoryMaster.Name, model.InvoiceDate.Value.ToShortDateString(), statusClass)
                            .AddBody("Invoice", model.InvoiceNumber, model.Clients.Name, model.StatusCategoryMaster.Name, model.Total.ToString());

                        var activityModel = new ActivityModel
                        {
                            CreateBy = model.CreateBy,
                            //BusinessID = invoiceModel.BusinessID,
                            Status = model.StatusCategoryMaster.Name,
                            Text = activity.ToString(),
                            HtmlText = activity.ToStringHtml()
                        };

                        //_activityRepo.Create(activityModel);
                        _invoiceRepo.Invoices_Update_OverdueStatus(model.InvoiceNumber, "", "");
                    }
                }

                return XmlProcessor.ReadXmlFile("");
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
            //try
            //{
            //    bool result = _invoiceRepo.CreateInvoiceActivity(sCreatedBY, sStatus_Core);
            //    if (result)
            //    {
            //        return XmlProcessor.ReadXmlFile("CUW110");
            //    }
            //    return string.Empty;
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(_logger.MethodName, ex);

            //    throw;
            //}
        }


        public List<InvoiceModel> GetAllByPage(int businessId, int page, string search, string orderBy)
        {
            try
            {
                return _invoiceRepo.GetAllByPage(businessId, page, search, orderBy);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public InvoiceModel GetById(int businessId, int id)
        {
            try
            {
                return _invoiceRepo.GetById(businessId, id);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public InvoiceModel Create(InvoiceModel model)
        {
            try
            {
                var output = new InvoiceModel { IsSucceeded = false };
                int result = _invoiceRepo.Create(model);
                if (result == 0)
                {
                    output.ReturnMessage = "Current request failed due to technical issue, please try again.";// XmlProcessor.ReadXmlFile("OTW901");
                    return output;
                }

                output.IsSucceeded = true;
                output.ReturnMessage = "Invoice has been created successfully.";// XmlProcessor.ReadXmlFile("INS302");

                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public InvoiceModel Update(InvoiceModel model)
        {
            try
            {
                var output = new InvoiceModel { IsSucceeded = false };
                int result = _invoiceRepo.Update(model);
                if (result == 0)
                {
                    output.ReturnMessage = "Current request failed due to technical issue, please try again.";// XmlProcessor.ReadXmlFile("OTW901");
                    return output;
                }

                output.IsSucceeded = true;
                output.ReturnMessage = "Invoice has been updated successfully.";// XmlProcessor.ReadXmlFile("INS302");

                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public InvoiceModel Activate(int businessId, int id, bool active)
        {
            try
            {
                var output = new InvoiceModel { IsSucceeded = false };
                int result = _invoiceRepo.Activate(businessId, id, active);
                if (result == 0)
                {
                    output.ReturnMessage = "Current request failed due to technical issue, please try again.";// XmlProcessor.ReadXmlFile("OTW901");
                    return output;
                }

                output.IsSucceeded = true;
                output.ReturnMessage = "Invoice has been activated/deactivated successfully.";// XmlProcessor.ReadXmlFile("INS302");

                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public InvoiceModel Delete(int businessId, int id)
        {
            try
            {
                var output = new InvoiceModel { IsSucceeded = false };
                int result = _invoiceRepo.Delete(businessId, id);
                if (result == 0)
                {
                    output.ReturnMessage = "Current request failed due to technical issue, please try again.";// XmlProcessor.ReadXmlFile("OTW901");
                    return output;
                }

                output.IsSucceeded = true;
                output.ReturnMessage = "Invoice has been deleted successfully.";// XmlProcessor.ReadXmlFile("INS302");

                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
    }
}
