using Paybook.ServiceLayer.Models.Clients;

namespace Paybook.BusinessLayer.Abstracts.Customers
{
    public interface IClientProcessor : IBaseProcessor<ClientModel>
    {
        bool IsExist(string createBy, string clientName);
        int GetCount(int businessId);
        ClientDetailsCountersModel GetCountersById(int businessId, int Id);
        ClientModel[] GetAllByText(string SearchText);
        ClientModel[] GetAllNamesByAgencyID(string sAgency_ID);
        ClientModel[] Customer_SelectRemainingAmount(string sCustomer_ID);
        decimal GetBalanceTotalById(int businessId, int id);
    }
}
