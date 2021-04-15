using KazDevSoft.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KazDevSoft.App
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User();
            user.Id = 6;
            user.FullName = "Джозеф Кони";
            user.Iin = "880111300392";
            user.Salary = 200000;

            Service service = new Service(@"C:\Users\ГерценЕ\source\repos\KazDevSoft.App\KazDevSoft.App\App");
            service.UserCreate(user);

            foreach (var item in service.GetUsers())
            {
                Console.WriteLine("{0}  -  {1}", item.Id, item.FullName);
            }
        }
    }
}
