using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paybook.BusinessLayer
{
    public interface IBaseProcessor<T> where T : class
    {
        List<T> GetAllByPage(string username, int page, string search, string orderBy);
        T GetById(string username, int id);
        T Create(T model);
        T Update(T model);
        T Activate(string username, int id, bool active);
        T Delete(string username, int id);
    }
}
