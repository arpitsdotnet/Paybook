using Paybook.ServiceLayer.Models;

namespace Paybook.DatabaseLayer.Common
{
    public interface ILastSavedNumberRepository
    {
        LastSavedNumberModel GetNewNumberByType(int businessId, string type);
        int Update(LastSavedNumberModel model);
    }
}
