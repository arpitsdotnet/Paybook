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
        string Activity_Insert(ActivityModel activityModel);
        DataTable Activity_Select();
    }

    public class ActivityProcessor : IActivityProcessor
    {
        private readonly ILogger _logger;
        private readonly IActivityRepository _activity;

        public ActivityProcessor()
        {
            _logger = FileLogger.Instance;
            _activity = new ActivityRepository();
        }
        public string Activity_Insert(ActivityModel activityModel)
        {
            try
            {
                bool result = _activity.Activity_Insert(activityModel);
                if (result)
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

        public DataTable Activity_Select()
        {
            try
            {
                return _activity.Activity_Select();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
    }
}
