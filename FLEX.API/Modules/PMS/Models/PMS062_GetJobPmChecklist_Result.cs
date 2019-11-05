using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.PMS.Models
{
    public class PMS062_GetJobPmChecklist_Result
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
        
        public Nullable<int> PM_CHECKLISTID
        {
            get;
            set;
        }
        
        public string PM_CHECKLIST_DESC
        {
            get;
            set;
        }
        
        public string NORMAL_CHECK
        {
            get;
            set;
        }
        
        public Nullable<bool> NORMAL_CHECK_BOOL
        {
            get;
            set;
        }
        
        public string PROBLEM_DESC
        {
            get;
            set;
        }
        
        public string REPAIR_METHOD
        {
            get;
            set;
        }
    }
}
