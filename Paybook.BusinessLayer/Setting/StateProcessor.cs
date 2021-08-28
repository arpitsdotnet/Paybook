using Paybook.DatabaseLayer;
using Paybook.DatabaseLayer.Setting;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.BusinessLayer.Setting
{
    public interface IStateProcessor
    {
        List<StateMasterModel> GetAllByPage(int countryId, int page, string search, string orderBy);
        StateMasterModel GetById(int id);
        StateMasterModel Create(StateMasterModel model);
        StateMasterModel Update(StateMasterModel model);
        StateMasterModel Activate(int id, bool active);
        StateMasterModel Delete(int id);
    }

    public class StateProcessor : IStateProcessor
    {
        private readonly ILogger _logger;
        private readonly IStateRepository _state;

        public StateProcessor()
        {
            _logger = LoggerFactory.Instance;
            _state = new StateRepository();
        }

        public List<StateMasterModel> GetAllByPage(int countryId, int page, string search, string orderBy)
        {
            try
            {
                return _state.GetAllByPage(countryId, page, search, orderBy);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public StateMasterModel GetById(int id)
        {
            try
            {
                return _state.GetById(id);
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public StateMasterModel Create(StateMasterModel model)
        {
            try
            {
                var output = new StateMasterModel { IsSucceeded = false };
                int result = _state.Create(model);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = Messages.Get(MTypes.StateMaster, MStatus.InsertSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = Messages.Get(MTypes.StateMaster, MStatus.InsertFailure);
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public StateMasterModel Update(StateMasterModel model)
        {
            try
            {
                var output = new StateMasterModel { IsSucceeded = false };
                int result = _state.Update(model);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = Messages.Get(MTypes.StateMaster, MStatus.UpdateSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = Messages.Get(MTypes.StateMaster, MStatus.UpdateFailure);
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public StateMasterModel Activate(int id, bool active)
        {
            try
            {
                var output = new StateMasterModel { IsSucceeded = false };
                int result = _state.Activate(id, active);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    if (active)
                        output.ReturnMessage = Messages.Get(MTypes.StateMaster, MStatus.ActivateSuccess);
                    else
                        output.ReturnMessage = Messages.Get(MTypes.StateMaster, MStatus.DeactivateSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                if (active)
                    output.ReturnMessage = Messages.Get(MTypes.StateMaster, MStatus.ActivateFailure);
                else
                    output.ReturnMessage = Messages.Get(MTypes.StateMaster, MStatus.DeactivateFailure);
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public StateMasterModel Delete(int id)
        {
            try
            {
                var output = new StateMasterModel { IsSucceeded = false };
                int result = _state.Delete(id);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = Messages.Get(MTypes.StateMaster, MStatus.DeleteSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = Messages.Get(MTypes.StateMaster, MStatus.DeleteFailure);
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
