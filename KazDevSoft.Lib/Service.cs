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
    public class Service
    {
        public Service(string rootPath)
        {
            if (string.IsNullOrWhiteSpace(rootPath))
                throw new Exception("Укажите корректный путь.");

            rootDir = new DirectoryInfo(rootPath);
            if (!rootDir.Exists)
                rootDir.Create();

            pathToUsersList = Path.Combine(rootDir.FullName, "Users.xml");
        }

        private DirectoryInfo rootDir = null;
        private string pathToUsersList;

        /// <summary>
        /// метод создания пользователя
        /// </summary>
        /// <param name="user"></param>
        public bool UserCreate(User user)
        {
            try
            {
                //1. Создаем директорию Users
                DirectoryInfo userDir = rootDir.CreateSubdirectory("Users");

                //2. Путь к файлу с данными о пользователе
                string path = Path.Combine(userDir.FullName, user.Id + ".xml");

                //3. Производим сериализацию
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(User));
                    xmlSerializer.Serialize(fs, user);
                }

                //4. Добавлем в список пользователей, вновь добавленного пользователя
                AddUserToList(user);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Методж добавления пользователя в список
        /// </summary>
        /// <param name="worker"></param>
        private void AddUserToList(User worker)
        {
            XmlDocument xmlDoc = new XmlDocument();

            if (!File.Exists(pathToUsersList))
                xmlDoc.AppendChild(xmlDoc.CreateElement("Users"));
            else
                xmlDoc.Load(pathToUsersList);

            worker.GetXmlNode(xmlDoc);
            xmlDoc.Save(pathToUsersList);
        }

        public List<User> GetUsers()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(pathToUsersList);

            List<User> users = new List<User>();
            foreach (XmlNode item in xmlDoc.DocumentElement.SelectNodes("User"))
            {
                users.Add(new User()
                {
                    Id = Int32.Parse(item.SelectSingleNode("Id").InnerText),
                    FullName = item.SelectSingleNode("FullName").InnerText
                }) ;
            }

            return users;
        }
    }
}
