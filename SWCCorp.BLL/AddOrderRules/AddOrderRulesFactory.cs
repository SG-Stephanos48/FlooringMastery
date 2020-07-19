using SWCCorp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.BLL.AddOrderRules
{
    public class AddOrderRulesFactory
    {

        public static IAddOrderRepo Create()
        {
            return new AddOrderRule();
        }

    }
}
