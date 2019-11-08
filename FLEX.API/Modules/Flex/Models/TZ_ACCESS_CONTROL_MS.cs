using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.Flex.Models
{
    public class TZ_ACCESS_CONTROL_MS
    {
        public string GROUP_CD { get; set; }
        public string SCREEN_CD { get; set; }
        public string METHOD { get; set; }
        public string METHOD_NAME { get; set; }
        public bool CAN_EXECUTE { get; set; }
        public string CRT_BY { get; set; }
        public DateTime CRT_DATE { get; set; }
        public string CRT_MACHINE { get; set; }
        public string UPD_BY { get; set; }
        public DateTime? UPD_DATE { get; set; }
        public string UPD_MACHINE { get; set; }
    }
}
