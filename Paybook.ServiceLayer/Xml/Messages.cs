using Paybook.ServiceLayer.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace Paybook.ServiceLayer.Xml
{
    public class Messages
    {
        public static string Get(string messageType, string messageStatus)
        {
            messageType = messageType == "" ? "messages.xml" : messageType + ".xml";
            string filePath = Path.Combine(HttpRuntime.AppDomainAppPath, _FolderPath.Messages + messageType);
            XElement xelement = XElement.Load(filePath);
            var text = from s in xelement.Elements("Message")
                          where (string)s.Element("MsgText").Attribute("id") == messageStatus
                          select s.Value;

            //string sMsgText = "";
            //foreach (XElement element in text)
            //{
            //    sMsgText = element.Element("MsgText").Value;
            //}
            return text.FirstOrDefault();
        }
    }
}
