using Paybook.ServiceLayer.Models;

namespace Paybook.BusinessLayer.Abstracts.Identity
{
    public interface ILoginProcessor
    {
        /// <summary>
        /// Check if username valid
        /// </summary>
        /// <param name="model"></param>
        /// <returns>string: UserMatch | UserNotMatch | UserNotExist</returns>
        string IsValid(IdentityUserModel loginModel);

    }
}
