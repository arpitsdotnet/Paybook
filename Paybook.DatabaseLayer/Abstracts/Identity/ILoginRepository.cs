using Paybook.ServiceLayer.Models;

namespace Paybook.DatabaseLayer.Abstracts.Identity
{
    public interface ILoginRepository
    {
        string IsValid(IdentityUserModel loginModel);
    }
}
