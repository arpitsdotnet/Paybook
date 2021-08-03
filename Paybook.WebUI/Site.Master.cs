using Paybook.BusinessLayer;
using Paybook.BusinessLayer.Business;
using Paybook.ServiceLayer;
using Paybook.ServiceLayer.Logger;
using System;
using System.Data;

namespace Paybook.WebUI
{
    public partial class Site : System.Web.UI.MasterPage
    {
        private readonly ILogger _logger;
        private readonly IBusinessProcessor _business;

        public Site()
        {
            _logger = FileLogger.Instance;
            _business = new BusinessProcessor();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                imgLogo.ImageUrl = Application["Path"] + "_Documents/IMG_CompanyLogo/FWTLogo.png";
                if (Session["LoggedInUser"] != null && Session["LoggedInUser"].ToString() != "")
                {
                    if (!IsPostBack)
                    {

                        //string sLoginUser = Session["LoggedInUser"].ToString();
                        DataTable dtCompanyProfile = _business.CompanyProfile_Select();
                        if (dtCompanyProfile.Rows.Count > 0 && dtCompanyProfile != null)
                        {
                            //if(Convert.ToInt32(hfWindowWidth.Value)<600)

                            lblUsername.Text = dtCompanyProfile.Rows[0]["CompanyName"].ToString();
                            hlLoggedInControlsProfile.ImageUrl = _FolderPath.CompanyLogo_Path + dtCompanyProfile.Rows[0]["ImageFileName"].ToString();
                            // hfCompanyLogo_Image.Value = dtCompanyProfile.Rows[0]["ImageFileName"].ToString();               

                        }
                        //lblUsername.Text = sLoginUser.Split('/')[0];
                        //hfLogin_ID.Value = sLoginUser.Split('_')[1];
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
            }
        }
    }
}