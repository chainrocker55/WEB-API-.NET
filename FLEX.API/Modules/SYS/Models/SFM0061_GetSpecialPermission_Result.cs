using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.SYS.Models
{
    public class SFM0061_GetSpecialPermission_Result
    {
        public string SCREEN_CD { get; set; }
        public string SCREEN_NAME { get; set; }
        public bool? CAN_EXECUTE { get; set; }
        public string METHOD { get; set; }
        public string METHOD_NAME { get; set; }
    }
}
