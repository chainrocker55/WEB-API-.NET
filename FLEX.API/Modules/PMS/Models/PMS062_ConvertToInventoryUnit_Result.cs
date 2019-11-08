using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.PMS.Models
{
    public class PMS062_ConvertToInventoryUnit_Result
    {
        
        public string PARTS_LOC_CD
        {
            get;
            set;
        }
        
        public string PARTS_ITEM_CD
        {
            get;
            set;
        }
        
        public string UNITCODE
        {
            get;
            set;
        }
        
        public decimal INVQTY
        {
            get;
            set;
        }
    }
}
