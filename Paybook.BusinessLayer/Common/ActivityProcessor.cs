using Paybook.DatabaseLayer;
using Paybook.DatabaseLayer.Common;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Services;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.BusinessLayer.Common
{
    public interface IActivityProcessor
    {
        List<ActivityModel> GetAllByPage(int businessId, int page, string search, string orderBy);
        ActivityModel Create(ActivityBuilderModel model);
    }

    public class ActivityProcessor : IActivityProcessor
    {
        private readonly ILogger _logger;
        private readonly IActivityRepository _activity;

        public ActivityProcessor()
        {
            _logger = LoggerFactory.Instance;
            _activity = new ActivityRepository();
        }

        public List<ActivityModel> GetAllByPage(int businessId, int page, string search, string orderBy)
        {
            try
            {
                return _activity.GetAllByPage(businessId, page, search, orderBy);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public ActivityModel Create(ActivityBuilderModel model)
        {
            try
            {
                var output = new ActivityModel();

                ActivityBuilder activityBuilder = new ActivityBuilder()
                    .AddHeader(model.Title, model.Date, model.TitleCss)
                    .AddBody(model.Type, model.TypeNumber, model.ClientName, model.Title, model.Amount);

                ActivityModel activityModel = new ActivityModel
                {
                    BusinessId = model.BusinessId,
                    CreateBy = model.CreateBy,
                    Status = model.Title,
                    Text = activityBuilder.ToString(),
                    TextHtml = activityBuilder.ToStringHtml()
                };

                int result = _activity.Create(activityModel);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = Messages.Get(MTypes.Activity, MStatus.InsertSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = Messages.Get(MTypes.Activity, MStatus.InsertFailure);
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
