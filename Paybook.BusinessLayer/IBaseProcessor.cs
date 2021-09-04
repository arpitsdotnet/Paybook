using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paybook.BusinessLayer
{
    public interface IBaseProcessor<T> where T : class
    {
        List<T> GetAllByPage(int businessId, int page, string search, string orderBy);
        T GetById(int businessId, int id);
        T Create(T model);
        T Update(T model);
        T Activate(int businessId, string username, int id, bool active);
        T Delete(int businessId, string username, int id);
    }
}
