using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWCCorp.BLL.AddOrderRules;
using SWCCorp.BLL.EditOrderRules;
using SWCCorp.BLL.RemoveOrderRules;
using SWCCorp.Models;
using SWCCorp.Models.Interfaces;
using SWCCorp.Models.Responses;

namespace SWCCorp.BLL
{
    public class OrderManager
    {

        public IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public OrderLookupResponse LookupOrder(string orderDate)
        {
            OrderLookupResponse response = new OrderLookupResponse();

            response.Orders = _orderRepository.LoadOrder(orderDate);

            if (response.Orders == null)
            {
                response.Success = false;
                response.Message = $"{orderDate} is not a valid order.";
            }
            else
            {
                response.Success = true;
            }

            return response;

        }
        
        public OrderAddResponse Add(Orders passOrderAdd, string orderDate)
        {
            OrderAddResponse response = new OrderAddResponse();

            //response.Orders = _orderRepository.OrdersAdd(passOrderAdd, orderDate);

            if (passOrderAdd == null)
            {
                response.Success = false;
                response.Message = $"{orderDate} is not a valid date.";
                return response;
            }
            else
            {
                response.Success = true;
            }
            IAddOrderRepo addOrderRule = AddOrderRulesFactory.Create();
            response = addOrderRule.OrdersAdd(passOrderAdd, orderDate);

            if (response.Success)
            {
                _orderRepository.SaveOrder(response.Orders, response.orderDate);
                //_orderRepository.SaveOrder(response.Orders, orderDate);
            }

            return response;
        }

        public OrderEditResponse Edit(Orders userEdit, string orderDate, int orderNumber)
        {
            OrderEditResponse response = new OrderEditResponse();

            //response.Orders = _orderRepository.FindOrder(orderDate);

            if (userEdit == null)
            {
                response.Success = false;
                response.Message = $"{orderDate} or {orderDate} is not a valid date.";
                return response;
            }
            else
            {
                response.Success = true;
            }
            IEditOrderRepo editRule = EditOrderRulesFactory.Create();
            response = editRule.OrderEdit(userEdit, orderDate, orderNumber);

            if (response.Success)
            {
                _orderRepository.OrdersEdit(userEdit, orderDate, orderNumber);
                //_orderRepository.SaveOrder(response.Orders);
            }

            return response;
        }

        public OrderRemoveResponse Remove(Orders passOrderRemove, string orderDate, int orderNumber)
        {
            OrderRemoveResponse response = new OrderRemoveResponse();

            //response.Orders = _orderRepository.LoadOrder(orderDate);

            if (passOrderRemove == null)
            {
                response.Success = false;
                response.Message = $"{orderDate} or {orderDate} is not a valid date.";
                return response;
            }
            else
            {
                response.Success = true;
            }
            IRemoveOrderRepo removeRule = RemoveOrderRulesFactory.Create();
            response = removeRule.OrderRemove(response.Orders, orderDate, orderNumber);

            if (response.Success)
            {
                _orderRepository.OrdersRemove(passOrderRemove, orderDate, orderNumber);
            }

            return response;
        }

    }
}
