using SWCCorp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.BLL.RemoveOrderRules
{
    public class RemoveOrderRulesFactory
    {
        public static IRemoveOrderRepo Create()
        {
            return new RemoveOrderRule();
        }
    }
}
