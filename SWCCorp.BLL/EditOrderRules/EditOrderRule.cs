using SWCCorp.Models;
using SWCCorp.Models.Interfaces;
using SWCCorp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.BLL.EditOrderRules
{
    public class EditOrderRule : IEditOrderRepo
    {

        public OrderEditResponse OrderEdit(Orders userEdit, string orderDate, int orderNumber)
        {

            OrderEditResponse response = new OrderEditResponse();

            response.Orders = userEdit;
            response.orderDate = orderDate;
            response.orderNumber = orderNumber;

            response.Success = true;

            return response;

        }
    }
}
