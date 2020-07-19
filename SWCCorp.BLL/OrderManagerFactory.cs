using SWCCorp.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.BLL
{
    public static class OrderManagerFactory
    {
        public static OrderManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "Test":
                    return new OrderManager(new TestRepository());
                case "Prod":
                    return new OrderManager(new ProdRepository(@"C:\Users\steph\Desktop\SWCCorp\"));
                default:
                    throw new Exception("Mode value in app config is not valid");
            }

        }

    }
}
