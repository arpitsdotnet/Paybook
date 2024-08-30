﻿using System.Collections.Generic;
using Paybook.ServiceLayer.Models;

namespace Paybook.BusinessLayer.Abstracts.Customers
{
    public interface IBusinessProcessor
    {
        //bool IsExist(string createBy, string businessName);

        List<BusinessModel> GetAllByUsername(string username);
        BusinessModel GetSelectedByUsername(string username);
        BusinessModel GetById(int id, string username);
        BusinessModel Create(BusinessModel model);
        BusinessModel Update(BusinessModel model);
        BusinessModel UpdateSelected(int id, string username);
        BusinessModel Activate(int id, string username, bool active);
        BusinessModel Delete(int id);
    }
}