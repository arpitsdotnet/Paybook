using Paybook.ServiceLayer.Models;
using Paybook.ServiceLayer.Xml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.DatabaseLayer.Business
{
    public interface IBusinessRepository
    {
        BusinessModel[] CompanyProfile_Insert(BusinessModel businessModel);
        DataTable CompanyProfile_IsExist();
        DataTable CompanyProfile_Select();
        string CompanyProfile_Update(BusinessModel businessModel);
        DataTable Dashboard_GetAllCounters();
    }

    public class BusinessRepository : IBusinessRepository
    {
        private readonly IDbContext _dbContext;

        public BusinessRepository()
        {
            _dbContext = DbContextFactory.Instance;
        }
        public DataTable Dashboard_GetAllCounters()
        {
            DataTable dt;
            try
            {
                dt = _dbContext.LoadDataByProcedure("sps_Dashboard_SelectCounts", null);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;
        }

        public BusinessModel[] CompanyProfile_Insert(BusinessModel businessModel)
        {
            List<BusinessModel> oCompany = new List<BusinessModel>();
            try
            {
                List<Parameter> oParams = new List<Parameter>();
                oParams.Add(new Parameter("sCreatedBY", businessModel.CreatedBY));
                oParams.Add(new Parameter("sCompanyName", businessModel.CompanyName));
                oParams.Add(new Parameter("sFounded_Date", businessModel.Founded_Date));
                oParams.Add(new Parameter("sCompanyAddress1", businessModel.Address1));
                oParams.Add(new Parameter("sCompanyAddress2", businessModel.Address2));
                oParams.Add(new Parameter("sCompanyCity", businessModel.City));
                oParams.Add(new Parameter("sCompanyState_Core", businessModel.State_Core));
                oParams.Add(new Parameter("sCompanyCountry", businessModel.Country_Core));
                oParams.Add(new Parameter("sCompanyPhoneNumber1", businessModel.PhoneNumber1));
                oParams.Add(new Parameter("sCompanyPhoneNumber2", businessModel.PhoneNumber2));
                oParams.Add(new Parameter("sCompanyFaxNumber", businessModel.FaxNumber));
                oParams.Add(new Parameter("sCompanyEmail", businessModel.EMail));
                oParams.Add(new Parameter("sImageFileName", businessModel.ImageFileName));
                oParams.Add(new Parameter("sGSTIN", businessModel.GSTIN));

                _dbContext.LoadDataByProcedure("sps_CompanyProfile_Insert", oParams);
                BusinessModel oDataRows = new BusinessModel();
                oDataRows.Message = XmlProcessor.ReadXmlFile("CPS401");
                oCompany.Add(oDataRows);
            }
            catch (Exception ex)
            {
                BusinessModel oDataRows = new BusinessModel();
                oDataRows.ERROR = ex.Message;
                oCompany.Add(oDataRows);
            }
            return oCompany.ToArray();
        }

        public string CompanyProfile_Update(BusinessModel businessModel)
        {
            string sMessage = "";
            List<BusinessModel> oCompany = new List<BusinessModel>();
            try
            {
                List<Parameter> oParams = new List<Parameter>();
                oParams.Add(new Parameter("sModifiedBY", businessModel.ModifiedBY));
                oParams.Add(new Parameter("sCompanyName", businessModel.CompanyName));
                oParams.Add(new Parameter("sFounded_Date", businessModel.Founded_Date));
                oParams.Add(new Parameter("sCompanyAddress1", businessModel.Address1));
                oParams.Add(new Parameter("sCompanyAddress2", businessModel.Address2));
                oParams.Add(new Parameter("sCompanyCity", businessModel.City));
                oParams.Add(new Parameter("sCompanyState_Core", businessModel.State_Core));
                oParams.Add(new Parameter("sCompanyCountry", businessModel.Country_Core));
                oParams.Add(new Parameter("sCompanyPhoneNumber1", businessModel.PhoneNumber1));
                oParams.Add(new Parameter("sCompanyPhoneNumber2", businessModel.PhoneNumber2));
                oParams.Add(new Parameter("sCompanyFaxNumber", businessModel.FaxNumber));
                oParams.Add(new Parameter("sCompanyEmail", businessModel.EMail));
                oParams.Add(new Parameter("sImageFileName", businessModel.ImageFileName));
                oParams.Add(new Parameter("sGSTIN", businessModel.GSTIN));
                oParams.Add(new Parameter("sUserName", businessModel.UserName));
                oParams.Add(new Parameter("sPassword", businessModel.Password));

                DataTable dt = _dbContext.LoadDataByProcedure("sps_CompanyProfile_Update", oParams);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0]["id"]) == 1)
                        sMessage = XmlProcessor.ReadXmlFile("CPS402");
                    else
                        sMessage = XmlProcessor.ReadXmlFile("BSW006");
                }


            }
            catch (Exception ex)
            {
                sMessage = ex.Message;
            }
            return sMessage;
        }
        public DataTable CompanyProfile_IsExist()
        {
            try
            {
                return _dbContext.LoadDataByProcedure("sps_CompanyProfile_IsExist", null) ;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable CompanyProfile_Select()
        {
            try
            {

                return _dbContext.LoadDataByProcedure("sps_CompanyProfile_Select", null);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
