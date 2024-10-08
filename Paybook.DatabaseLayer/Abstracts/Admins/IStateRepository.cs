﻿using Paybook.ServiceLayer.Models;

namespace Paybook.DatabaseLayer.Abstracts.Admins
{
    public interface IStateRepository : IBaseRepository<StateMasterModel>
    {
        StateMasterModel GetById(int id);
        int Activate(int id, bool active);
        int Delete(int id);
    }
}
