using System.Data;
using Paybook.DatabaseLayer.Abstracts.Identity;
using Paybook.ServiceLayer.Models;

namespace Paybook.DatabaseLayer.Identity
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IDbContext _dbContext;

        public LoginRepository()
        {
            _dbContext = DbContextFactory.Instance;
        }

        /// <summary>
        /// Check if username valid
        /// </summary>
        /// <param name="model"></param>
        /// <returns>string: UserExist | UserNotExist | UserMatch | UserNotMatch</returns>
        public string IsValid(IdentityUserModel model)
        {
            var p = new { Username = model.Username, PasswordHash = model.PasswordHash };
            _ = _dbContext.SaveDataOutParam("sps_IdentityUsers_IsValid", p, out string message, DbType.String, 20, "Message");
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
    }
}
