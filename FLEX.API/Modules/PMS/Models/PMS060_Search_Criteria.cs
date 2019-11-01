using System;

namespace FLEX.API.Modules.PMS.Models
{
    public class PMS060_Search_Criteria
    {
        public DateTime? REQUEST_DATE_FROM { get; set; }
        public DateTime? REQUEST_DATE_TO { get; set; }
        public DateTime? TEST_DATE_FROM { get; set; }
        public DateTime? TEST_DATE_TO { get; set; }
        public DateTime? COMPLETE_DATE_FROM { get; set; }
        public DateTime? COMPLETE_DATE_TO { get; set; }
        public string CHECK_REP_NO_FROM { get; set; }
        public string CHECK_REP_NO_TO { get; set; }
        public string PONUMBER_FROM { get; set; }
        public string PONUMBER_TO { get; set; }
        public string PERSONINCHARGE { get; set; }
        public string REQUESTER { get; set; }
        public string MACHINE_LOC_CD { get; set; }
        public string CUR_PERSON { get; set; }
        public string MACHINE_NO_FROM { get; set; }
        public string MACHINE_NO_TO { get; set; }
        public string MACHINE_NAME { get; set; }
        public int? VENDORID { get; set; }
        public int? SCHEDULE_TYPEID { get; set; }
        public string STATUSID { get; set; }
    }
}