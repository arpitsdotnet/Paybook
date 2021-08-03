using Paybook.DatabaseLayer.Invoice;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.DatabaseLayer.Common
{
    public interface IActivityRepository
    {
        bool Activity_Insert(ActivityModel activityModel);
        DataTable Activity_Select();
    }

    public class ActivityRepository : IActivityRepository
    {
        private readonly ILogger _logger;
        private readonly IDbContext _dbContext;

        public ActivityRepository()
        {
            _logger = FileLogger.Instance;
            _dbContext = DbContextFactory.Instance;
        }

        public bool Activity_Insert(ActivityModel activityModel)
        {
            try
            {
                var parameters = new List<Parameter>
                {
                   new Parameter("sCreatedBY", activityModel.CreatedBY),
                   new Parameter("sActivity_Date", activityModel.Activity_Date),
                   new Parameter("sAgency_ID", activityModel.Agency_ID),
                   new Parameter("sCustomer_ID", activityModel.Customer_ID),
                   new Parameter("sPaymentAmount", activityModel.PaymentAmount),
                   new Parameter("sCategory_Core", activityModel.Category_Core),
                   new Parameter("sInvoice_ID", activityModel.Invoice_ID),
                   new Parameter("sInvoiceStatus_Core", activityModel.InvoiceStatus_Core)

                };

                _dbContext.LoadDataByProcedure("sps_Activity_Insert", parameters);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }

        }

        public DataTable Activity_Select()
        {
            try
            {
                //select overdue and overpaid from activity              
                return _dbContext.LoadDataByProcedure("sps_Activity_SelectAll", null);

            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
    }
}
