using SWCCorp.BLL;
using SWCCorp.Models;
using SWCCorp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.UI.Workflows
{
    public class EditOrderWorkflow
    {

        public void Execute()
        {

            Console.Clear();
            OrderManager orderManager = OrderManagerFactory.Create();

            Console.Clear();
            Console.WriteLine("Edit an Order");
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

            Orders passOrderEdit = new Orders();
            List<Orders> lastOrders = new List<Orders>();

            lastOrders = orderManager._orderRepository.FindOrder(orderDate);

            if(lastOrders == null)
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
                    if(order.OrderNumber == orderNumber)
                    {
                        passOrderEdit.OrderNumber = order.OrderNumber;
                        passOrderEdit.CustomerName = order.CustomerName;
                        passOrderEdit.State = order.State;
                        passOrderEdit.TaxRate = order.TaxRate;
                        passOrderEdit.ProductType = order.ProductType;
                        passOrderEdit.Area = order.Area;
                        passOrderEdit.CostPerSquareFoot = order.CostPerSquareFoot;
                        passOrderEdit.LaborCostPerSquareFoot = order.LaborCostPerSquareFoot;
                        passOrderEdit.MaterialCost = order.MaterialCost;
                        passOrderEdit.LaborCost = order.LaborCost;
                        passOrderEdit.Tax = order.Tax;
                        passOrderEdit.Total = order.Total;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            Orders userEdit = new Orders();

            Console.Write($"Edit CustomerName ({passOrderEdit.CustomerName}):\n");
            string editCustomerName = Console.ReadLine();

            if (editCustomerName == null)
            {
                userEdit.CustomerName = passOrderEdit.CustomerName;
            }
            else
            {
                userEdit.CustomerName = editCustomerName;
            }

            string editState;
            //decimal editTaxRate;
            Tax taxChoice = null;
            bool goodTax = false;
            do
            {
                Console.Write($"Edit State ({passOrderEdit.State}):\n");
                editState = Console.ReadLine();

                var taxdownload = orderManager._orderRepository.LookUpTax(editState);

                if (editState == taxdownload.StateName)
                {
                    taxChoice = taxdownload;
                    userEdit.TaxRate = taxdownload.TaxRate;
                    userEdit.State = taxdownload.StateName;
                    Console.Write($"{taxdownload.StateName} TaxRate is: {taxdownload.TaxRate}\n");
                    Console.Write($"\n");
                    goodTax = true;
                }
                else if(editState == null)
                {
                    userEdit.TaxRate = passOrderEdit.TaxRate;
                    userEdit.State = passOrderEdit.State;
                    goodTax = true;
                }
                else
                {
                    Console.Write("Sorry, we cannot sell to this state at this time.");
                    //editTaxRate = 0;
                    goodTax = false;
                }

            } while (goodTax == false);


            string editProductType;
            Console.Write($"Edit Product Type ({passOrderEdit.ProductType}):\n");
            var productDown = orderManager._orderRepository.LookUpProducts();
            foreach (var product in productDown)
            {
                Console.WriteLine("{0}, {1}, {2}",
                    product.ProductType, product.LaborCostPerSquareFoot, product.CostPerSquareFoot);
                Console.Write("");
            }
            Console.Write($"\n");
            editProductType = Console.ReadLine();
            
            bool goodProduct = false;
            Products editConfirmedChoice = null;
            do
            {
                //Products confirmedChoice;
                Console.WriteLine("");
                if (editProductType == null)
                {
                    userEdit.ProductType = passOrderEdit.ProductType;
                }
                else
                {
                    var productChoice = orderManager._orderRepository.ChooseProduct(editProductType);
                    if (productChoice != null)
                    {
                        editConfirmedChoice = productChoice;
                        userEdit.ProductType = productChoice.ProductType;
                        userEdit.CostPerSquareFoot = productChoice.CostPerSquareFoot;
                        userEdit.LaborCostPerSquareFoot = productChoice.LaborCostPerSquareFoot;
                        goodProduct = true;
                    }
                    else
                    {
                        Console.Write("\n You have made an incorrect choice, please choose again.");
                        goodProduct = false;
                    };
                }
            } while (goodProduct == false) ;


            bool goodArea = false;
            decimal editArea;
            do
            {
                Console.Write($"Edit Area ({passOrderEdit.Area}):\n");
                editArea = Convert.ToDecimal(Console.ReadLine());
                if (editArea >= 100M)
                {
                    if (editArea.ToString().Count() == 0)
                    {
                        userEdit.Area = passOrderEdit.Area;
                    }
                    else
                    {
                        userEdit.Area = editArea;
                    }
                    goodArea = true;
                    continue;
                }
                else
                {
                    Console.WriteLine("\nArea entered must be positive and greater than 100 sq feet.");
                    goodArea = false;
                }
            } while (goodArea == false);


            decimal costPerSquareFoot;
            decimal laborCostPerSquareFoot;
            decimal materialCost;
            decimal laborCost;
            decimal tax;
            decimal total;

            costPerSquareFoot = editConfirmedChoice.CostPerSquareFoot;
            laborCostPerSquareFoot = editConfirmedChoice.LaborCostPerSquareFoot;
            materialCost = userEdit.Area * editConfirmedChoice.CostPerSquareFoot;
            laborCost = userEdit.Area * editConfirmedChoice.LaborCostPerSquareFoot;
            tax = ((userEdit.Area * editConfirmedChoice.CostPerSquareFoot) + (userEdit.Area * editConfirmedChoice.LaborCostPerSquareFoot)) * (taxChoice.TaxRate / 100);
            total = (userEdit.Area * editConfirmedChoice.CostPerSquareFoot) + (userEdit.Area * editConfirmedChoice.LaborCostPerSquareFoot) + (((userEdit.Area * editConfirmedChoice.CostPerSquareFoot) + (userEdit.Area * editConfirmedChoice.LaborCostPerSquareFoot)) * (taxChoice.TaxRate / 100));

            userEdit.OrderNumber = orderNumber;
            userEdit.LaborCost = laborCost;
            userEdit.MaterialCost = materialCost;
            userEdit.Tax = tax;
            userEdit.Total = total;

            Console.Write($"\nOrderDate is {orderDate}");
            Console.Write($"\nOrderNumber is {orderNumber}");
            Console.Write($"\nCustomerName is {userEdit.CustomerName}");
            Console.Write($"\nStateName is {taxChoice.StateAbbreviation}");
            Console.Write($"\nTaxRate is {taxChoice.TaxRate}");
            Console.Write($"\nProductType is {userEdit.ProductType}");
            Console.Write($"\nCostPerSquareFoot is {costPerSquareFoot}");
            Console.Write($"\nLaborCostPerSquareFoot is {laborCostPerSquareFoot}");
            Console.Write($"\nMaterialCost: {materialCost}");
            Console.Write($"\nLaborCost: {laborCost}");
            Console.Write($"\nTax: {tax}");
            Console.Write($"\nTotal: {total}");

            Console.Write("\n Please review changes above. \n If info seems right, please type Edit to continue or anything else to go back to main menu. \n");

            string resp = Console.ReadLine();

            if (resp == "Edit")
            {
                OrderEditResponse response = orderManager.Edit(userEdit, orderDate, orderNumber);

                if (response.Success)
                {
                    ConsoleIO.DisplayOrderDetails(response.Orders, orderDate);
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
