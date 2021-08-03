using Paybook.DatabaseLayer.Setting;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paybook.BusinessLayer.Setting
{
    public interface IParticularProcessor
    {
        ParticularModel[] Particular_IsExist(string sParticular, string sCategory_Core);
    }
    public class ParticularProcessor : IParticularProcessor
    {
        private readonly ILogger _logger;
        private readonly IParticularRepository _particularRepo;

        public ParticularProcessor()
        {
            _logger = FileLogger.Instance;
            _particularRepo = new ParticularRepository();
        }
        public ParticularModel[] Particular_IsExist(string sParticular, string sCategory_Core)
        {
            try
            {
                return _particularRepo.Particular_IsExist(sParticular, sCategory_Core);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
    }
}
