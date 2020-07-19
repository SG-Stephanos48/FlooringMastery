using SWCCorp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.BLL.EditOrderRules
{
    public class EditOrderRulesFactory
    {
        public static IEditOrderRepo Create()
        {
            return new EditOrderRule();
        }
    }
}
