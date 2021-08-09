using Paybook.DatabaseLayer;
using Paybook.DatabaseLayer.Note;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paybook.BusinessLayer.Note
{
    public interface INoteProcessor
    {
        List<NoteModel> GetAllByPage(int businessId, int page, string search, string orderBy);
        NoteModel GetById(int businessId, int id);
        NoteModel Create(NoteModel model);
        NoteModel Update(NoteModel model);
        NoteModel Activate(int businessId, int id, bool active);
        NoteModel Delete(int businessId, int id);
    }
    public class NoteProcessor : INoteProcessor
    {
        private readonly ILogger _logger;
        private readonly NoteRepository _noteRepo;

        public NoteProcessor()
        {
            _logger = LoggerFactory.Instance;
            _noteRepo = new NoteRepository();
        }

        public List<NoteModel> GetAllByPage(int businessId, int page, string search, string orderBy)
        {
            try
            {
                return _noteRepo.GetAllByPage(businessId, page, search, orderBy);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        public NoteModel GetById(int businessId, int id)
        {
            try
            {
                return _noteRepo.GetById(businessId, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        public NoteModel Create(NoteModel model)
        {
            try
            {
                NoteModel output = new NoteModel { IsSucceeded = false };
                int result = _noteRepo.Create(model);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = XmlProcessor.ReadXmlFile("NDS902");
                }
                output.ReturnMessage = XmlProcessor.ReadXmlFile("NoteUpdateFail");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        public NoteModel Update(NoteModel model)
        {
            try
            {
                NoteModel output = new NoteModel { IsSucceeded = false };
                int result = _noteRepo.Update(model);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = XmlProcessor.ReadXmlFile("NDS902");
                }
                output.ReturnMessage = XmlProcessor.ReadXmlFile("NoteUpdateFail");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        public NoteModel Activate(int businessId, int id, bool active)
        {
            try
            {
                NoteModel output = new NoteModel { IsSucceeded = false };
                int result = _noteRepo.Activate(businessId, id, active);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = XmlProcessor.ReadXmlFile("NDS902");
                }
                output.ReturnMessage = XmlProcessor.ReadXmlFile("NoteActivateFail");
                return output;

            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        public NoteModel Delete(int businessId, int id)
        {
            try
            {
                NoteModel output = new NoteModel { IsSucceeded = false };
                int result = _noteRepo.Delete(businessId, id);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = XmlProcessor.ReadXmlFile("NDS902");
                }
                output.ReturnMessage = XmlProcessor.ReadXmlFile("NoteDeleteFail");
                return output;

            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
    }
}
