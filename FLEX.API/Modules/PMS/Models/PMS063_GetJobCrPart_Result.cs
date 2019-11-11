using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.PMS.Models
{
    public class PMS063_GetJobCrPart_Result
    {
        public string CHECK_REPH_ID
        {
            get;
            set;
        }
        
        public int SEQ
        {
            get;
            set;
        }
        
        public string LOC_CD
        {
            get;
            set;
        }
        
        public string ITEM_CD
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
        
        public int DETAILTYPE
        {
            get;
            set;
        }
        
        public Nullable<decimal> IN_QTY
        {
            get;
            set;
        }
        
        public string IN_CLEAN
        {
            get;
            set;
        }
        
        public Nullable<bool> IN_CLEAN_BOOL
        {
            get;
            set;
        }
        
        public string IN_APPEARANCE
        {
            get;
            set;
        }
        
        public Nullable<bool> IN_APPEARANCE_BOOL
        {
            get;
            set;
        }
        
        public Nullable<decimal> OUT_USEDQTY
        {
            get;
            set;
        }
        
        public Nullable<decimal> OUT_RETURNQTY
        {
            get;
            set;
        }
        
        public string OUT_CLEAN
        {
            get;
            set;
        }
        
        public Nullable<bool> OUT_CLEAN_BOOL
        {
            get;
            set;
        }
        
        public string OUT_APPEARANCE
        {
            get;
            set;
        }
        
        public Nullable<bool> OUT_APPEARANCE_BOOL
        {
            get;
            set;
        }
    }
}
