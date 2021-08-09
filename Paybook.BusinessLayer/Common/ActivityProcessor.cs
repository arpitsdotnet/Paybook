using Paybook.DatabaseLayer;
using Paybook.DatabaseLayer.Common;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
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
        string Create(ActivityModel activityModel);
        List<ActivityModel> GetAllByPage(int businessId, int page, string search, string orderBy);
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
        public string Create(ActivityModel activityModel)
        {
            try
            {
                int result = _activity.Create(activityModel);
                if (result > 0)
                {
                    return XmlProcessor.ReadXmlFile("AGES104");
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public List<ActivityModel> GetAllByPage(int businessId, int page, string search, string orderBy)
        {
            try
            {
                return _activity.GetAllByPage(businessId, page, search, orderBy);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
    }
}
