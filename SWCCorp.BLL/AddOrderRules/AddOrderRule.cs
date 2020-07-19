using SWCCorp.Models;
using SWCCorp.Models.Interfaces;
using SWCCorp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.BLL.AddOrderRules
{
    public class AddOrderRule : IAddOrderRepo
    {
        public OrderAddResponse OrdersAdd(Orders orders, string orderDate)
        {

            OrderAddResponse response = new OrderAddResponse();

            /*  //decide to do verification here or in workflow??
            if (account.Type != AccountType.Basic && account.Type != AccountType.Premium)
            {
                response.Success = false;
                response.Message = "Error: Only basic and premium accounts can deposit with no limit. Contact IT";
                return response;
            }

            if (amount <= 0)
            {
                response.Success = false;
                response.Message = "Deposit amounts must be positive.";
                return response;
            }
            */

            response.Orders = orders;
            response.orderDate = orderDate;

            /*
            response.Orders.OrderNumber = orders.OrderNumber;
            response.Orders.CustomerName = orders.CustomerName;
            response.Orders.State = orders.State;
            response.Orders.TaxRate = orders.TaxRate;
            response.Orders.ProductType = orders.ProductType;
            response.Orders.Area = orders.Area;
            response.Orders.CostPerSquareFoot = orders.CostPerSquareFoot;
            response.Orders.LaborCostPerSquareFoot = orders.LaborCostPerSquareFoot;
            response.Orders.MaterialCost = orders.MaterialCost;
            response.Orders.LaborCost = orders.LaborCost;
            response.Orders.Tax = orders.Tax;
            response.Orders.Total = orders.Total;
            */

            response.Success = true;

            return response;
        }
    }
}
