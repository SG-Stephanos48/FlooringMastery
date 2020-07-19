using SWCCorp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.Models.Interfaces
{
    public interface IRemoveOrderRepo
    {

        OrderRemoveResponse OrderRemove(Orders orders, string orderDate, int orderNumber);

    }
}
