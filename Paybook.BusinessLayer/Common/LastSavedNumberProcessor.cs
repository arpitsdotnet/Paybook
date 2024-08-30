using System;
using Paybook.BusinessLayer.Abstracts.Customers;
using Paybook.BusinessLayer.Abstracts.Utilities;
using Paybook.DatabaseLayer.Common;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;

namespace Paybook.BusinessLayer.Common
{
    public class LastSavedNumberProcessor : ILastSavedNumberProcessor
    {
        private readonly ILogger _logger;
        private readonly IBusinessProcessor _business;
        private readonly ILastSavedNumberRepository _lastSavedNumberRepo;

        public LastSavedNumberProcessor(
            ILogger logger,
            IBusinessProcessor business)
        {
            _logger = logger;
            _business = business;
            _lastSavedNumberRepo = new LastSavedNumberRepository();
        }

        public string GetNewNumberByType(string username, string type)
        {
            //string newNumber = "";
            //int currentYear = DateTime.Now.Year;
            //int currentMonth = DateTime.Now.Month;
            //var months = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L" };
            try
            {
                var business = _business.GetSelectedByUsername(username);

                var result = _lastSavedNumberRepo.GetNewNumberByType(business.Id, type);
                return result.NewNumber;

                //if (result != null)
                //{
                //    int lastNumber = Int32.Parse(result.LastNumber);
                //    int lastYear = Int32.Parse(result.Year);

                //    if (lastYear < currentYear)
                //        newNumber = "1";
                //    else
                //        newNumber = (lastNumber + 1).ToString();
                //}

                //if (newNumber.Length == 0)
                //    newNumber = "0001";
                //else if (newNumber.Length == 1)
                //    newNumber = "000" + newNumber;
                //else if (newNumber.Length == 2)
                //    newNumber = "00" + newNumber;
                //else if (newNumber.Length == 3)
                //    newNumber = "0" + newNumber;

                //var numberWithMonth = months[currentMonth] + newNumber;

                //newNumber = result.Prefix + result.Seperator + currentYear.ToString() + result.Seperator + numberWithMonth;

                //return result;
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
