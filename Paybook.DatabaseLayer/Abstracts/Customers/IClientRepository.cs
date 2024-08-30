using System;
using Paybook.ServiceLayer.Models.Clients;

namespace Paybook.DatabaseLayer.Abstracts.Customers
{
    public interface IClientRepository : IBaseRepository<ClientModel>
    {
        bool IsExist(string createBy, string name);
        int GetCount(int businessId);
        ClientDetailsCountersModel GetCountersById(int businessId, int Id);
        decimal GetBalanceTotalById(int businessId, int id);

        [Obsolete]
        ClientModel[] GetPaymentByClientID(string sCustomer_ID);
    }
}
