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
    public class XmlProcessor
    {
        public static string ReadXmlFile(string MsgID, string path = null)
        {
            string sMsgText = "";
            string sOriginalFilePath = Path.Combine(HttpRuntime.AppDomainAppPath, _FolderPath.Messages + "messages.xml");
            XElement xelement = XElement.Load(sOriginalFilePath);
            var MsgText = from s in xelement.Elements("Message")
                          where (string)s.Element("MsgText").Attribute("id") == MsgID
                          select s;

            foreach (XElement xEle in MsgText)
            {
                sMsgText = xEle.Element("MsgText").Value;
            }
            return sMsgText;
        }
    }
}
