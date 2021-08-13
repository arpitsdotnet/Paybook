using Paybook.DatabaseLayer.Common;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.BusinessLayer.Common
{
    public interface ILastSavedNumberProcessor
    {
        string GetNewNumber(int businessId, string type);
        void Update(LastSavedNumberModel model);
    }

    public class LastSavedNumberProcessor : ILastSavedNumberProcessor
    {
        private readonly ILogger _logger;
        private readonly ILastSavedNumberRepository _lastSavedNumberRepo;

        public LastSavedNumberProcessor()
        {
            _logger = LoggerFactory.Instance;
            _lastSavedNumberRepo = new LastSavedNumberRepository();
        }

        public string GetNewNumber(int businessId, string type)
        {
            string newNumber = "";
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;
            var months = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" };
            try
            {
                var result = _lastSavedNumberRepo.GetByType(businessId, type);
                if (result != null)
                {
                    int lastNumber = Int32.Parse(result.LastNumber);
                    int lastYear = Int32.Parse(result.Year);

                    if (lastYear < currentYear)
                        newNumber = "1";
                    else
                        newNumber = (lastNumber + 1).ToString();
                }

                if (newNumber.Length == 1)
                    newNumber = "000" + newNumber;
                if (newNumber.Length == 2)
                    newNumber = "00" + newNumber;
                if (newNumber.Length == 3)
                    newNumber = "0" + newNumber;

                var numberWithMonth = months[currentMonth] + newNumber;

                newNumber = result.Prefix + result.Seperator + currentYear.ToString() + result.Seperator + numberWithMonth;

                return newNumber;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }

        public void Update(LastSavedNumberModel model)
        {
            try
            {
                _lastSavedNumberRepo.Update(model);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
    }
}
