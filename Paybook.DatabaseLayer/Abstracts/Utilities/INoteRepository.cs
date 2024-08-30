using Paybook.ServiceLayer.Models;

namespace Paybook.DatabaseLayer.Abstracts.Utilities
{
    public interface INoteRepository : IBaseRepository<NoteModel>
    {
        int GetTotalCount(int businessId);
    }
}
