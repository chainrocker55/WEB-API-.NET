using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.PMS.Models
{
    public class PMS150_Search_Criteria
    {
        public int? SHIFTID { get; set; }
        public int? LINEID { get; set; }
        public string MACHINE_NO { get; set; }
        public string MACHINE_NAME { get; set; }
        public DateTime? CHECK_DATE_FROM { get; set; }
        public DateTime? CHECK_DATE_TO { get; set; }
        public string STATUSID { get; set; }
    }
}
