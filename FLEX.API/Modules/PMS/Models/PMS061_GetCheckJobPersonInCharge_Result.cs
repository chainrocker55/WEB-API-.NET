using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.PMS.Models
{
    public class PMS061_GetCheckJobPersonInCharge_Result
    {
        [Key]
        public string PERSONINCHARGE
        {
            get;
            set;
        }
        
        public Nullable<int> POSITIONID
        {
            get;
            set;
        }
        
        public string POSITIONCODE
        {
            get;
            set;
        }
        
        public string POSITIONNAME
        {
            get;
            set;
        }
        
        public int SEQ
        {
            get;
            set;
        }
        
        public string DISPLAY
        {
            get;
            set;
        }
        
        public string CHECK_REPH_ID
        {
            get;
            set;
        }   
        
        public string DELETEFLAG
        {
            get;
            set;
        }
    }
}
