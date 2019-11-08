using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.PMS.Models
{
    public class ApproveHistory
    {
        public int? APPROVE_ID { get; set; }
        public int? DOCUMENT_HID { get; set; }
        public int? DOCUMENT_DID { get; set; }
        public string SourceType { get; set; }
        public string DocumentNo { get; set; }
        public int? APPROVE_LEVEL { get; set; }
        public string APPROVE_STATUS { get; set; }
        public string APPROVE_USER { get; set; }
        public string REMARK { get; set; }
    }
}
