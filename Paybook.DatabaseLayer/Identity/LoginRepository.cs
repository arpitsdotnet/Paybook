using Paybook.ServiceLayer.Logger;
using Paybook.ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Paybook.DatabaseLayer.Identity
{
    public interface ILoginRepository
    {
        string IsValid(IdentityUserModel loginModel);
    }

    public class LoginRepository : ILoginRepository
    {
        private readonly ILogger _logger;
        private readonly IDbContext _dbContext;

        public LoginRepository()
        {
            _logger = LoggerFactory.Instance;
            _dbContext = DbContextFactory.Instance;
        }

        /// <summary>
        /// Check if username valid
        /// </summary>
        /// <param name="model"></param>
        /// <returns>string: UserExist | UserNotExist | UserMatch | UserNotMatch</returns>
        public string IsValid(IdentityUserModel model)
        {
            try
            {
                var p = new { Username = model.Username, PasswordHash = model.PasswordHash };

                var result = _dbContext.SaveDataOutParam("sps_IdentityUser_IsValid", p, out string message, DbType.String, 20, "Message");
                //DataTable dt = _dbContext.LoadDataByProcedure("sps_IdentityUser_IsValid", parameters);
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    if (!(dt.Rows[0]["Message"] is null))
                //    {
                //        return dt.Rows[0]["Message"].ToString();
                //    }
                //}

                return message;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }
        public bool IsForgetPassword(string email)
        {
            try
            {
                var p = new { Email = email };

                // Is Email Exists

                // If exists, send password link to that emailid

                // Check if valid link is clicked to update password
                
                // Update the new password

                var result = _dbContext.SaveDataOutParam("sps_IdentityUser_IsValid", p, out bool isExist, DbType.String, 20, "IsExist");
                //DataTable dt = _dbContext.LoadDataByProcedure("sps_IdentityUser_IsValid", parameters);
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    if (!(dt.Rows[0]["Message"] is null))
                //    {
                //        return dt.Rows[0]["Message"].ToString();
                //    }
                //}

                return isExist;
            }
            catch (Exception ex)
            {
                _logger.LogError(_logger.MethodName, ex);
                throw;
            }
        }

    }
}
