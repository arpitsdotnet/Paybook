using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paybook.DatabaseLayer
{
  public  interface IBaseRepository<T>
    {
        List<T> GetAllByPage(int businessId, int page, string search, string orderBy);
        T GetById(int businessId, int id);
        int Create(T model);
        int Update(T model);
        int Activate(int businessId, int id, bool active);
        int Delete(int businessId, int id);        
    }
}
