using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.PMS.Models
{
    public class PMS062_GetItemOnhandAtDate_Result
    {
        
        public string ITEM_CD
        {
            get;
            set;
        }
        
        public Nullable<decimal> AVAILABLE_AT_DATE
        {
            get;
            set;
        }
        
        public Nullable<decimal> ONHAND_AT_DATE
        {
            get;
            set;
        }
        
        public string LOC_CD
        {
            get;
            set;
        }
        
        public string LOT_NO
        {
            get;
            set;
        }
    }
}
