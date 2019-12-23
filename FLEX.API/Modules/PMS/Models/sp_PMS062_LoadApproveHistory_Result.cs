using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.PMS.Models
{
    public class sp_PMS062_LoadApproveHistory_Result
    {
       
        public string APPROVE_USER
        {
            get;
            set;
        }
    	
        public Nullable<int> APPROVE_LEVEL
        {
            get;
            set;
        }
    	
        public string APPROVE_STATUS
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
