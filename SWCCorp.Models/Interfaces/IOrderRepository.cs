using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.Models.Interfaces
{
    public interface IOrderRepository
    {

        Orders LoadOrder(string OrderDate);
        
        Orders OrdersAdd(Orders passOrderAdd, string orderDate);

        Orders OrdersEdit(Orders userEdit, string orderDate, int orderNumber);

        Tax LookUpTax(string stateName);

        List<Products> LookUpProducts();

        Products ChooseProduct(string productType);

        List<Orders> FindLastOrder(string orderDate);

        List<Orders> FindOrder(string orderDate);

        Orders OrdersRemove(Orders passOrderRemove, string orderDate, int orderNumber);

        void SaveOrder(Orders orders, string orderDate);
        
    }
}
