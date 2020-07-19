using SWCCorp.Models.Interfaces;
using SWCCorp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.Data
{
    public class TestRepository : IOrderRepository
    {

        private static Orders _orders = new Orders
        {

            OrderNumber = 123,
            CustomerName = "Rhaz Al Ghul",
            State = "TX",
            TaxRate = 825M,
            ProductType = "Hardwood",
            Area = 100M,
            CostPerSquareFoot = 5.0M,
            LaborCostPerSquareFoot = 2.5M

        };

        private static Tax _tax = new Tax
        {

            StateAbbreviation = "TX",
            StateName = "Texas",
            TaxRate = 825M

        };

        private static Products _products = new Products
        {

            ProductType = "Hardwwod",
            CostPerSquareFoot = 5M,
            LaborCostPerSquareFoot = 2.5M

        };

        public Products ChooseProduct(string productType)
        {
            throw new NotImplementedException();
        }

        public Orders LoadOrder(string OrderDate)
        {
            //Get date from user, if date exists load orders for date (need to figure out date for test)
            //if date does not exist, display an error message and return the user to the main menu
            //if file does exist print all of the order information
            if (OrderDate != null)
            {
                return _orders;
            }
            else
            {
                return null;
            }
        }

        public Tax LookUpTax(string state)
        {
            throw new NotImplementedException();
        }

        public Orders OrdersAdd(Orders passOrderAdd, string orderDate)
        {
            throw new NotImplementedException();
        }

        public void SaveOrder(Orders orders, string orderDate)
        {
            _orders = orders;
        }

        public List<Products> LookUpProducts()
        {
            throw new NotImplementedException();
        }

        public List<Orders> FindLastOrder(string orderDate)
        {
            throw new NotImplementedException();
        }

        public Orders OrdersEdit(Orders userEdit, string orderDate, int orderNumber)
        {
            throw new NotImplementedException();
        }

        public List<Orders> FindOrder(string orderDate)
        {
            throw new NotImplementedException();
        }

        public Orders OrdersRemove(Orders passOrderRemove, string orderDate, int orderNumber)
        {
            throw new NotImplementedException();
        }
    }
}
