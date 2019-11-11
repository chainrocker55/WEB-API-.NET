using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.PMS.Models
{
    public class PMS063_GetPersonalChecklist_Result
    {
        
        public string CHECK_REPH_ID
        {
            get;
            set;
        }
        
        public Nullable<int> SEQ
        {
            get;
            set;
        }
        
        public Nullable<int> PERSONAL_CK_ID
        {
            get;
            set;
        }
        
        public string PERSONAL_DESC
        {
            get;
            set;
        }
        
        public string PASS_CHECK
        {
            get;
            set;
        }

        public bool PASS { 
            get
            {
                return PASS_CHECK == "Y";
            }
        }
        //public bool NOT_PASS { get; set; }
    }
}
