using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWCCorp.BLL;
using SWCCorp.Models;
using SWCCorp.Models.Interfaces;
using SWCCorp.Models.Responses;

namespace SWCCorp.UI.Workflows
{
    public class AddOrderWorkflow
    {

        public void Execute()
        {

            Console.Clear();
            OrderManager orderManager = OrderManagerFactory.Create();

            Console.Clear();
            Console.WriteLine("Add an Order");
            Console.WriteLine("-------------------------");

            bool goodDate = false;
            string orderDate;
            DateTime orderDateCompare;
            do
            {
                //must be in the future
                Console.Write("Enter Date (MMDDYYYY): ");
                orderDate = Console.ReadLine();
                orderDateCompare = DateTime.ParseExact(orderDate, "MMddyyyy", CultureInfo.InvariantCulture);
                DateTime todayDate = DateTime.Today;
                if (orderDateCompare >= todayDate)
                {
                    goodDate = true;
                    continue;
                }
                else
                {
                    Console.WriteLine("Please enter a date in the future.");
                    goodDate = false;
                }
            } while (goodDate == false);


            //This field may not be blank; it is allowed to contain [a-z][0-9] as well as periods and comma characters. “Acme, Inc.” is a valid name.
            bool goodName = false;
            string customerName;
            do
            {
                Console.Write("\nEnter Customer Name: ");
                customerName = Console.ReadLine();
                if (customerName != null)
                {
                    goodName = true;
                    continue;
                }
                else
                {
                    Console.WriteLine("CustomerName cannot be empty.");
                    goodName = false;
                }
            } while (goodName == false);


            //Entered states must be checked against the tax file. If the state does not exist in the tax file, we cannot sell there. If a state is added to the tax file later, it should be included without changing the application code.
            string state;
            decimal taxRate;
            bool goodTax = false;
            do
            {
                Console.Write("\nEnter State (Enter full name of state): ");
                state = Console.ReadLine();
                //var taxdownload = new List<Tax>();
                var taxdownload = orderManager._orderRepository.LookUpTax(state);
                //state = "Texas";
                //create list
                //open file with tax info and put data in list (how to get data from list?)
                //foreach loop to figure out if stateName is in list
                //
                if (state == taxdownload.StateName)
                {
                    taxRate = taxdownload.TaxRate;
                    Console.Write($"{taxdownload.StateName} TaxRate is: {taxdownload.TaxRate}\n");
                    goodTax = true;
                }
                else
                {
                    Console.Write("Sorry, we cannot sell to this state at this time.");
                    taxRate = 0;
                    goodTax = false;
                }
             
            } while (goodTax == false);
            

            //Show a list of available products and pricing information to choose from. Again, if a product is added to the file, it should show up in the application without a code change.
            Console.Write("\nChoose Product Type: \n");
            var productDown = orderManager._orderRepository.LookUpProducts();

            foreach (var product in productDown)
            {
                Console.WriteLine("{0}, {1}, {2}",
                    product.ProductType, product.LaborCostPerSquareFoot, product.CostPerSquareFoot);
                Console.Write("");
            }

            bool goodProduct = false;
            string productType;
            Products confirmedChoice = null;
            do
            {
                //Products confirmedChoice;
                Console.WriteLine("");
                Console.Write("\nEnter your product selection (Enter full name productType): ");
                productType = Console.ReadLine();
                var productChoice = orderManager._orderRepository.ChooseProduct(productType);
                if (productChoice != null)
                {
                    confirmedChoice = productChoice;
                    goodProduct = true;
                }
                else
                {
                    Console.Write("\n You have made an incorrect choice, please choose again.");
                    goodProduct = false;
                };

            } while (goodProduct == false);

            //string productType = Console.ReadLine();
            //string productType = "Hardwood";

            //The area must be a positive decimal.  Minimum order size is 100 square feet.
            bool goodArea = false;
            decimal area;
            do
            {
                Console.Write("Area: ");
                area = Convert.ToDecimal(Console.ReadLine());
                if (area >= 100M)
                {
                    goodArea = true;
                    continue;
                }
                else
                {
                    Console.WriteLine("\nArea entered must be positive and greater than 100 sq feet.");
                    goodArea = false;
                }
            } while (goodArea == false);
            //Show input summary


            int lastOrderNumber = 0;
            decimal costPerSquareFoot;
            decimal laborCostPerSquareFoot;
            decimal materialCost;
            decimal laborCost;
            decimal tax;
            decimal total;
            //int orderNumber;
            costPerSquareFoot = confirmedChoice.CostPerSquareFoot;
            laborCostPerSquareFoot = confirmedChoice.LaborCostPerSquareFoot;
            materialCost = area * confirmedChoice.CostPerSquareFoot;
            laborCost = area * confirmedChoice.LaborCostPerSquareFoot;
            tax = ((area * confirmedChoice.CostPerSquareFoot) + (area * confirmedChoice.LaborCostPerSquareFoot)) * (taxRate / 100);
            total = (area * confirmedChoice.CostPerSquareFoot) + (area * confirmedChoice.LaborCostPerSquareFoot) + (((area * confirmedChoice.CostPerSquareFoot) + (area * confirmedChoice.LaborCostPerSquareFoot)) * (taxRate / 100));
            //orderNumber = orderManager._orderRepository.LookUpOrderNumber(orderDate);

            Orders passOrderAdd = new Orders();
            List<Orders> lastOrders = new List<Orders>();

            lastOrders = orderManager._orderRepository.FindLastOrder(orderDate);
            if(lastOrders == null)
            {
                lastOrderNumber = 0;
            }
            else
            {
                lastOrderNumber = lastOrders.Count();  //Check Max()
            }
            passOrderAdd.OrderNumber = lastOrderNumber + 1;
            passOrderAdd.CustomerName = customerName;
            passOrderAdd.State = state;
            passOrderAdd.TaxRate = taxRate;
            passOrderAdd.ProductType = productType;
            passOrderAdd.Area = area;
            passOrderAdd.CostPerSquareFoot = costPerSquareFoot;
            passOrderAdd.LaborCostPerSquareFoot = laborCostPerSquareFoot;
            passOrderAdd.MaterialCost = materialCost;
            passOrderAdd.LaborCost = laborCost;
            passOrderAdd.Tax = tax;
            passOrderAdd.Total = total;

            Console.Write($"\nOrderDate is {orderDate}");
            Console.Write($"\nCustomerName is {customerName}");
            Console.Write($"\nStateName is {state}");
            Console.Write($"\nTaxRate is {taxRate}");
            Console.Write($"\nProductType is {productType}");
            Console.Write($"\nCostPerSquareFoot is {confirmedChoice.CostPerSquareFoot}");
            Console.Write($"\nLaborCostPerSquareFoot is {confirmedChoice.LaborCostPerSquareFoot}");
            Console.Write($"\nMaterialCost: {area * confirmedChoice.CostPerSquareFoot}");
            Console.Write($"\nLaborCost: {area * confirmedChoice.LaborCostPerSquareFoot}");
            Console.Write($"\nTax: {((area * confirmedChoice.CostPerSquareFoot) + (area * confirmedChoice.LaborCostPerSquareFoot)) * (taxRate / 100)}");
            Console.Write($"\nTotal: {(area * confirmedChoice.CostPerSquareFoot) + (area * confirmedChoice.LaborCostPerSquareFoot) + ((area * confirmedChoice.CostPerSquareFoot) + (area * confirmedChoice.LaborCostPerSquareFoot)) * (taxRate / 100)}");

            Console.Write("\nPlease review your order details. \nIf order seems right, please type Add to continue or anything else to go back to main menu.\n");
            string resp = Console.ReadLine();

            if (resp == "Add")
            {
                OrderAddResponse response = orderManager.Add(passOrderAdd, orderDate);

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
