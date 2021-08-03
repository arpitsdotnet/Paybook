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
        NoteModel[] GetAllByPage(string sGridPageNumber);
        NoteModel GetByNoteID(string sDataID);
        string Create(NoteModel noteModel);
        string Update(NoteModel noteModel);
        string Delete(string sDataID);
    }
    public class NoteProcessor : INoteProcessor
    {
        private readonly ILogger _logger;
        private readonly NoteRepository _noteRepo;

        public NoteProcessor()
        {
            _logger = FileLogger.Instance;
            _noteRepo = new NoteRepository();
        }

        public NoteModel[] GetAllByPage(string pageNumber)
        {
            try
            {
                return _noteRepo.GetAllByPage(pageNumber);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        public NoteModel GetByNoteID(string noteId)
        {
            try
            {
                return _noteRepo.GetByNoteID(noteId);
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        public string Create(NoteModel noteModel)
        {
            try
            {
                bool result = _noteRepo.Create(noteModel);
                if (result)
                    return XmlProcessor.ReadXmlFile("NDS901");

                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        public string Update(NoteModel noteModel)
        {
            try
            {
                bool result = _noteRepo.Update(noteModel);
                if (result)
                    return XmlProcessor.ReadXmlFile("NDS903");

                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
        public string Delete(string sDataID)
        {
            try
            {
                bool result = _noteRepo.Delete(sDataID);
                if (result)
                    return XmlProcessor.ReadXmlFile("NDS902");

                return string.Empty;

            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);

                throw;
            }
        }
    }
}
