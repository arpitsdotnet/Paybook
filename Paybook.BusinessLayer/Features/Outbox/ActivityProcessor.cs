using System;
using System.Collections.Generic;
using Paybook.BusinessLayer.Abstracts.Outbox;
using Paybook.DatabaseLayer.Abstracts.Outbox;
using Paybook.DatabaseLayer.Features.Outbox;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models.Activities;
using Paybook.ServiceLayer.Services;
using Paybook.ServiceLayer.Xml;

namespace Paybook.BusinessLayer.Features.Outbox
{
    public class ActivityProcessor : IActivityProcessor
    {
        private readonly ILogger _logger;
        private readonly IActivityRepository _activity;

        public ActivityProcessor(ILogger logger)
        {
            _logger = logger;
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

                if (result <= 0)
                {
                    return new ActivityModel
                    {
                        IsSucceeded = false,
                        ReturnMessage = XmlMessageHelper.Get(MTypes.Activity, MStatus.InsertFailure)
                    };
                }

                return new ActivityModel
                {
                    IsSucceeded = true,
                    ReturnMessage = XmlMessageHelper.Get(MTypes.Activity, MStatus.InsertSuccess)
                };
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }

    }
}
