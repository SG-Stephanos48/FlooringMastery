using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.Models.Responses
{
    public class OrderEditResponse : Response
    {

        public Orders Orders { get; set; }

        public int orderNumber { get; set; }

        public string orderDate { get; set; }

    }
}
