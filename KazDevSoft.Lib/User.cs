using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KazDevSoft.Lib
{
    [Serializable]
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Iin { get; set; }
        public DateTime CreateDate { get; set; }
        public string Position { get; set; }
        public int Salary { get; set; }
        
        public static explicit operator XmlElement(User user)
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlElement el_User = xmlDoc.CreateElement("User");

            XmlElement el_Id = xmlDoc.CreateElement("Id");
            el_Id.InnerText = user.Id.ToString();

            XmlElement el_FullName = xmlDoc.CreateElement("FullName");
            el_FullName.InnerText = user.FullName;

            el_User.AppendChild(el_Id);
            el_User.AppendChild(el_FullName);

            return el_User;
        }

        public void GetXmlNode(XmlDocument xmlDoc)
        {
            XmlElement el_User = xmlDoc.CreateElement("User");

            XmlElement el_Id = xmlDoc.CreateElement("Id");
            el_Id.InnerText = Id.ToString();

            XmlElement el_FullName = xmlDoc.CreateElement("FullName");
            el_FullName.InnerText = FullName;

            el_User.AppendChild(el_Id);
            el_User.AppendChild(el_FullName);

            xmlDoc.DocumentElement.AppendChild(el_User);
        }
    }
}
