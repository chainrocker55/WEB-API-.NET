using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.PMS.Models
{
    public class PMS062_GetJobPmPart_Result
    {

        public string CHECK_REPH_ID
        {
            get;
            set;
        }

        public int? SEQ
        {
            get;
            set;
        }

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

        public string PARTS_ITEM_DESC
        {
            get;
            set;
        }

        public Nullable<decimal> REQUEST_QTY
        {
            get;
            set;
        }

        public Nullable<decimal> IN_QTY
        {
            get;
            set;
        }

        public Nullable<decimal> USED_QTY
        {
            get;
            set;
        }

        public string UNITCODE
        {
            get;
            set;
        }

        public string REMARK
        {
            get;
            set;
        }
    }
}
