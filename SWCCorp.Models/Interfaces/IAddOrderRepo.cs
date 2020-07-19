using SWCCorp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.Models.Interfaces
{
    public interface IAddOrderRepo
    {

        OrderAddResponse OrdersAdd(Orders orders, string orderDate);

    }
}
