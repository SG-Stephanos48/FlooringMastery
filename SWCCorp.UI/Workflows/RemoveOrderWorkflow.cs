using SWCCorp.BLL;
using SWCCorp.Models;
using SWCCorp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.UI.Workflows
{
    public class RemoveOrderWorkflow
    {

        public void Execute()
        {

            Console.Clear();
            OrderManager orderManager = OrderManagerFactory.Create();

            Console.Clear();
            Console.WriteLine("Remove an Order");
            Console.WriteLine("-------------------------");

            bool goodDate = false;
            string orderDate;
            do
            {
                //must be in the future
                Console.Write("Enter the date you are interested in (use MMDDYYYY format):\n");

                orderDate = Console.ReadLine();
                if (orderDate != null && orderDate.Count() == 8)
                {
                    goodDate = true;
                    continue;
                }
                else
                {
                    Console.WriteLine("Please enter a date in correct format.");
                    goodDate = false;
                }
            } while (goodDate == false);

            bool goodNumber = false;
            int orderNumber;
            do
            {
                //must be in the future
                Console.Write("Enter Order Number you wish to edit:\n");

                orderNumber = int.Parse(Console.ReadLine());
                if (orderNumber <= 0 || orderNumber > 0)
                {
                    goodNumber = true;
                    continue;
                }
                else
                {
                    Console.WriteLine("Please enter a good order number.");
                    goodNumber = false;
                }
            } while (goodNumber == false);

            Orders passOrderRemove = new Orders();
            List<Orders> lastOrders = new List<Orders>();

            lastOrders = orderManager._orderRepository.FindOrder(orderDate);

            if (lastOrders == null)
            {
                Console.WriteLine("There are no orders for order number provided. ");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Menu.Start();
            }
            else
            {
                foreach (var order in lastOrders)
                {
                    if (order.OrderNumber == orderNumber)
                    {
                        passOrderRemove.OrderNumber = order.OrderNumber;
                        passOrderRemove.CustomerName = order.CustomerName;
                        passOrderRemove.State = order.State;
                        passOrderRemove.TaxRate = order.TaxRate;
                        passOrderRemove.ProductType = order.ProductType;
                        passOrderRemove.Area = order.Area;
                        passOrderRemove.CostPerSquareFoot = order.CostPerSquareFoot;
                        passOrderRemove.LaborCostPerSquareFoot = order.LaborCostPerSquareFoot;
                        passOrderRemove.MaterialCost = order.MaterialCost;
                        passOrderRemove.LaborCost = order.LaborCost;
                        passOrderRemove.Tax = order.Tax;
                        passOrderRemove.Total = order.Total;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            Console.Write($"\nOrderNumber is {passOrderRemove.OrderNumber}");
            Console.Write($"\nCustomerName is {passOrderRemove.CustomerName}");
            Console.Write($"\nStateName is {passOrderRemove.State}");
            Console.Write($"\nTaxRate is {passOrderRemove.TaxRate}");
            Console.Write($"\nProductType is {passOrderRemove.ProductType}");
            Console.Write($"\nArea is {passOrderRemove.Area }");
            Console.Write($"\nCostPerSquareFoot is {passOrderRemove.CostPerSquareFoot }");
            Console.Write($"\nLaborCostPerSquareFoot is {passOrderRemove.LaborCostPerSquareFoot}");
            Console.Write($"\nMaterialCost: {passOrderRemove.MaterialCost}");
            Console.Write($"\nLaborCost: {passOrderRemove.LaborCost}");
            Console.Write($"\nTax: {passOrderRemove.Tax}");
            Console.Write($"\nTotal: {passOrderRemove.Total}");

            Console.Write("\n Please review changes above. \n If you are sure you want to delete, please type: Delete \n");

            string resp = Console.ReadLine();

            if (resp == "Delete")
            {
                OrderRemoveResponse response = orderManager.Remove(passOrderRemove, orderDate, orderNumber);

                if (response.Success)
                {
                    Console.WriteLine("Your order has been successfully removed!");
                }
                else
                {
                    Console.WriteLine("An error occurred: ");
                    Console.WriteLine(response.Message);
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            else
            {
                Menu.Start();
            }


        }

    }
}
