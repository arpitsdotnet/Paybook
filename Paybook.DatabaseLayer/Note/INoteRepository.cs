using Paybook.ServiceLayer.Models;

namespace Paybook.DatabaseLayer.Note
{
    public interface INoteRepository : IBaseRepository<NoteModel>
    {
        int GetTotalCount(int businessId);
    }
}
