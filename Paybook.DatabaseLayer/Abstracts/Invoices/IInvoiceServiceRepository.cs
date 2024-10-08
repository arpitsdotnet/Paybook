﻿using Paybook.ServiceLayer.Models.Invoices;
using System.Collections.Generic;

namespace Paybook.DatabaseLayer.Abstracts.Invoices
{
    public interface IInvoiceServiceRepository : IBaseRepository<InvoiceServiceModel>
    {
        List<InvoiceServiceModel> GetAllByInvoiceId(int businessId, int invoiceId);
        bool IsExist(string businessId, int id);
    }
}
