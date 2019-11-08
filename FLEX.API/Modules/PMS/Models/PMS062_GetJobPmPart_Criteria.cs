using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.PMS.Models
{
    public class PMS062_GetJobPmPart_Criteria
    {
        public string CHECK_REPH_ID { get; set; }
        public string MCBOM_CD { get; set; }
        public string PARTS_LOC_CD { get; set; }
    }
}
