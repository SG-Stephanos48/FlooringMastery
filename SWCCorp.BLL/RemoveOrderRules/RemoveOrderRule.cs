using SWCCorp.Models;
using SWCCorp.Models.Interfaces;
using SWCCorp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.BLL.RemoveOrderRules
{
    class RemoveOrderRule : IRemoveOrderRepo
    {

        public OrderRemoveResponse OrderRemove(Orders orders, string orderDate, int orderNumber)
        {

            OrderRemoveResponse response = new OrderRemoveResponse();

            response.Orders = orders;
            response.orderDate = orderDate;
            response.orderNumber = orderNumber;

            response.Success = true;

            return response;

        }

    }
}
