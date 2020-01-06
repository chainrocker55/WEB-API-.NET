using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.PMS.Models
{
    public class sp_RPMS001_PartsWithdrawalSlipCorrective_Result
    {
        public string CHECK_REPH_ID
        {
            get;
            set;
        }
        
        public string CHECK_REP_NO
        {
            get;
            set;
        }
        
        public string LOC_DESC
        {
            get;
            set;
        }
        
        public Nullable<System.DateTime> REQUEST_DATE
        {
            get;
            set;
        }
        
        public string PARTS_ITEM_CD
        {
            get;
            set;
        }
        
        public string ITEM_DESC
        {
            get;
            set;
        }
        
        public string UNITCODE
        {
            get;
            set;
        }
        
        public Nullable<decimal> REQUEST_QTY
        {
            get;
            set;
        }
        
        public string SHELF_NAME
        {
            get;
            set;
        }
    }
}
