namespace Paybook.ServiceLayer.Abstracts
{
    public interface IMessageProvider
    {
        string Get(string messageType, string messageStatus);
        string GetUserMessage(string messageStatus);
        string GetBusinessMessage(string messageStatus);
        string GetActivityMessage(string messageStatus);
        string GetClientMessage(string messageStatus);
        string GetInvoiceMessage(string messageStatus);
    }
}
