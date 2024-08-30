using Paybook.ServiceLayer.Models;

namespace Paybook.DatabaseLayer.Identity
{
    public interface ILoginRepository
    {
        string IsValid(IdentityUserModel loginModel);
    }
}
