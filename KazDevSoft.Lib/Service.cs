using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace KazDevSoft.Lib
{
    class Service
    {
        DirectoryInfo di = null;
        public Service(string route)
        {
            if (string.IsNullOrWhiteSpace(route))
                throw new Exception("Укажите корректный путь.");
            di = new DirectoryInfo(route);
            if (!di.Exists)
                di.Create();
            this.route = route;
        }
        public string route { get; private set; }
        public void UserCreate(User worker)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(User));
            DirectoryInfo users = di.CreateSubdirectory("user");
            string path = users.FullName + @"\" + worker.Id + ".xml";

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate)) 
            {
                xmlSerializer.Serialize(fs, worker);
            }
        }

        public void AddUserToList(User worker)
        {
            FileInfo fi = new FileInfo(Path.Combine(di.FullName, "users.xml"));
            XmlDocument xmlDocument = new XmlDocument();
            if (!fi.Exists)
            {
                XmlElement xmlElement = xmlDocument.CreateElement("users");
                xmlDocument.AppendChild(xmlElement);
                xmlDocument.Save(fi.FullName);
            }

            XmlElement xmlTag = xmlDocument.CreateElement("user");
            xmlTag.InnerText = worker.FullName;
        }

        public void ChangeUser(User worker)
        {

        }
    }
}
