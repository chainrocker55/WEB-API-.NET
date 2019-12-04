using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Models
{
    public class TBM_STATUS
    {
        [Key]
        public string STATUSID { get; set; }
        public int STATUS_SEQ { get; set; }
        public string STATUSNAME { get; set; }
        public string STATUS_ICON { get; set; }

    }
}
