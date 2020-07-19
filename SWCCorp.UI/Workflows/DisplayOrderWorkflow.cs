using SWCCorp.BLL;
using SWCCorp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.UI.Workflows
{
    public class DisplayOrderWorkflow
    {

        public void Execute()
        {
            OrderManager manager = OrderManagerFactory.Create();

            Console.Clear();
            Console.WriteLine("Lookup an order");
            Console.WriteLine("-------------------------");
            Console.Write("Enter the date you are interested in (use MMDDYYYY format):");
            string orderDate = Console.ReadLine();
            //validation date but make sure to get rid of forward slashes

            OrderLookupResponse response = manager.LookupOrder(orderDate);

            if (response.Success)
            {
                ConsoleIO.DisplayOrderDetails(response.Orders, orderDate);
            }
            else
            {
                Console.WriteLine("An error occurred: ");
                Console.WriteLine(response.Message);

            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

    }
}
