using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.PMS.Models
{
    public class PMS062_GetApproveRoute_Result
    {
       
        public int APPROVE_ID
        {
            get;
            set;
        }
       
        public string CONDITION_NAME
        {
            get;
            set;
        }
       
        public string CONDITION_OPERATOR
        {
            get;
            set;
        }
       
        public Nullable<int> APPROVE_LEVEL
        {
            get;
            set;
        }
       
        public string APPROVE_USER
        {
            get;
            set;
        }
    }
}
