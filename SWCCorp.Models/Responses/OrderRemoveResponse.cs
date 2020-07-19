﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.Models.Responses
{
    public class OrderRemoveResponse : Response
    {

        public Orders Orders { get; set; }

        public string orderDate { get; set; }

        public int orderNumber { get; set; }

    }
}
