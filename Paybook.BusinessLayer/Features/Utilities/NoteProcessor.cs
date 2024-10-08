﻿using System;
using System.Collections.Generic;
using Paybook.BusinessLayer.Abstracts.Utilities;
using Paybook.DatabaseLayer.Features.Utilities;
using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;

namespace Paybook.BusinessLayer.Features.Utilities
{
    public class NoteProcessor : INoteProcessor
    {
        private readonly ILogger _logger;
        private readonly NoteRepository _noteRepo;

        public NoteProcessor(ILogger logger)
        {
            _logger = logger;
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
                _logger.Error(_logger.GetMethodName(), ex);

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
                _logger.Error(_logger.GetMethodName(), ex);

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
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.Note, MStatus.InsertSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = XmlMessageHelper.Get(MTypes.Note, MStatus.InsertFailure);
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

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
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.Note, MStatus.UpdateSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = XmlMessageHelper.Get(MTypes.Note, MStatus.UpdateFailure);
                return output;
            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);

                throw;
            }
        }
        public NoteModel Activate(int businessId, string username, int id, bool active)
        {
            try
            {
                NoteModel output = new NoteModel { IsSucceeded = false };
                int result = _noteRepo.Activate(businessId, username, id, active);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    if (active)
                        output.ReturnMessage = XmlMessageHelper.Get(MTypes.Note, MStatus.ActivateSuccess);
                    else
                        output.ReturnMessage = XmlMessageHelper.Get(MTypes.Note, MStatus.DeactivateSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                if (active)
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.Note, MStatus.ActivateFailure);
                else
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.Note, MStatus.DeactivateFailure);
                return output;

            }
            catch (Exception ex)
            {
                _logger.Error(_logger.GetMethodName(), ex);
                throw;
            }
        }
        public NoteModel Delete(int businessId, string username, int id)
        {
            try
            {
                NoteModel output = new NoteModel { IsSucceeded = false };
                int result = _noteRepo.Delete(businessId, username, id);
                if (result > 0)
                {
                    output.IsSucceeded = true;
                    output.ReturnMessage = XmlMessageHelper.Get(MTypes.Note, MStatus.DeleteSuccess);
                    return output;
                }

                output.IsSucceeded = false;
                output.ReturnMessage = XmlMessageHelper.Get(MTypes.Note, MStatus.DeleteFailure);
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
