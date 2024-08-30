using Paybook.ServiceLayer.Models;

namespace Paybook.DatabaseLayer.Abstracts.Utilities
{
    public interface ILastSavedNumberRepository
    {
        LastSavedNumberModel GetNewNumberByType(int businessId, string type);
        int Update(LastSavedNumberModel model);
    }
}
