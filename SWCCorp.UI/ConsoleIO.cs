using SWCCorp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.UI
{
    public class ConsoleIO
    {

        public static void DisplayOrderDetails(Orders orders, string orderDate)
        {
            Console.Write($"\nOrderDate is {orderDate}");
            Console.Write($"\nOrderNumber is {orders.OrderNumber}");
            Console.Write($"\nCustomerName is {orders.CustomerName}");
            Console.Write($"\nStateName is {orders.State}");
            Console.Write($"\nTaxRate is {orders.TaxRate}");
            Console.Write($"\nProductType is {orders.ProductType}");
            Console.Write($"\nCostPerSquareFoot is {orders.CostPerSquareFoot}");
            Console.Write($"\nLaborCostPerSquareFoot is {orders.LaborCostPerSquareFoot}");
            Console.Write($"\nMaterialCost: {orders.Area * orders.CostPerSquareFoot}");
            Console.Write($"\nLaborCost: {orders.Area * orders.LaborCostPerSquareFoot}");
            Console.Write($"\nTax: {((orders.Area * orders.CostPerSquareFoot) + (orders.Area * orders.LaborCostPerSquareFoot)) * (orders.TaxRate / 100)}");
            Console.Write($"\nTotal: {(orders.Area * orders.CostPerSquareFoot) + (orders.Area * orders.LaborCostPerSquareFoot) + ((orders.Area * orders.CostPerSquareFoot) + (orders.Area * orders.LaborCostPerSquareFoot)) * (orders.TaxRate / 100)}");
        }

    }
}
