using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.Models.Responses
{
    public class OrderAddResponse : Response
    {

        public Orders Orders { get; set; }

        public string orderDate { get; set; }


    }
}
