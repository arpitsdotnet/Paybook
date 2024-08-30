using Paybook.ServiceLayer.Abstracts;

namespace Paybook.ServiceLayer.Xml
{
    public class XmlMessageHelperWrapper : IMessageProvider
    {
        public string Get(string messageType, string messageStatus)
        {
            return XmlMessageHelper.Get(messageType, messageStatus);
        }

        public string GetUserMessage(string messageStatus)
        {
            return Get(MTypes.User, messageStatus);
        }
        public string GetBusinessMessage(string messageStatus)
        {
            return Get(MTypes.Business, messageStatus);
        }
        public string GetActivityMessage(string messageStatus)
        {
            return Get(MTypes.Activity, messageStatus);
        }
        public string GetClientMessage(string messageStatus)
        {
            return Get(MTypes.Client, messageStatus);
        }
        public string GetInvoiceMessage(string messageStatus)
        {
            return Get(MTypes.Invoice, messageStatus);
        }
    }
}
