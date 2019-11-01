using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.Models.PMS
{
    public class PMS060_CheckListAndRepairOrder_Result
    {
        [Key]
        public int? MACHINE_SCHEDULEID { get; set; }
        public string CHECK_REPH_ID { get; set; }
        public string CHECK_REP_NO { get; set; }
        public string MACHINE_NO { get; set; }
        public string MACHINE_NAME { get; set; }
        public DateTime? REQUEST_DATE { get; set; }
        public DateTime? TEST_DATE { get; set; }
        public DateTime? COMPLETE_DATE { get; set; }
        public string MACHINE_LOC_CD { get; set; }
        public string MACHINE_LOC { get; set; }
        public decimal? PERIOD { get; set; }
        public int? PERIOD_ID { get; set; }
        public decimal? TOTAL_TIME_OPERATION { get; set; }
        public int? SCHEDULE_TYPEID { get; set; }
        public string COMPLETE_MTN { get; set; }
        public string COMPLETE_RQ { get; set; }
        public string STATUSID { get; set; }
        public string PRINT_FLAG { get; set; }
        public string REVISE_REMARK { get; set; }
        public string CANCEL_REMARK { get; set; }
        public string STATUS_DISPLAY { get; set; }
        public string SCHEDULE_NAME { get; set; }
        public string SUPPLIER_DISPLAY { get; set; }
        public int? POHID { get; set; }
        public string PONUMBER { get; set; }
        public string PERSONINCHARGE { get; set; }
        public string REQUESTER { get; set; }
        public string APPROVER { get; set; }
        public int? VENDORID { get; set; }
        public DateTime? CREATEDATETIME { get; set; }
        public string CREATEUSERID { get; set; }
        public DateTime? LASTUPDATEDATETIME { get; set; }
        public string LASTUPDATEUSERID { get; set; }
        public DateTime? DELETEDATETIME { get; set; }
        public string DELETEUSERID { get; set; }
        public string PROBLEM_DESC { get; set; }
        public string REPAIR_METHOD { get; set; }
        public string CAUSE_DELAY { get; set; }
        public DateTime? START_DATE { get; set; }
        public DateTime? END_DATE { get; set; }
        public int DAYS { get; set; }
    }
}
