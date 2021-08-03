using Paybook.DatabaseLayer;
using Paybook.DatabaseLayer.Business;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.BusinessLayer.Business
{
    public interface IBusinessProcessor
    {
        BusinessModel[] CompanyProfile_Insert(BusinessModel businessModel);
        DataTable CompanyProfile_IsExist();
        DataTable CompanyProfile_Select();
        string CompanyProfile_Update(BusinessModel businessModel);
        DataTable Dashboard_SelectCounts();
    }

    public class BusinessProcessor : IBusinessProcessor
    {
        private readonly ILogger _logger;
        private readonly IBusinessRepository _businessRepo;

        public BusinessProcessor()
        {
            _logger = FileLogger.Instance;
            _businessRepo = new BusinessRepository();
        }

        public BusinessModel[] CompanyProfile_Insert(BusinessModel businessModel)
        {
            try
            {
                return _businessRepo.CompanyProfile_Insert(businessModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public DataTable CompanyProfile_IsExist()
        {
            try
            {
                return _businessRepo.CompanyProfile_IsExist();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public DataTable CompanyProfile_Select()
        {
            try
            {
                return _businessRepo.CompanyProfile_Select();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public string CompanyProfile_Update(BusinessModel businessModel)
        {
            try
            {
                return _businessRepo.CompanyProfile_Update(businessModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }

        public DataTable Dashboard_SelectCounts()
        {
            try
            {
                return _businessRepo.Dashboard_SelectCounts();
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
    }
}
