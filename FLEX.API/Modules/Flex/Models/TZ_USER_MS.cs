using System;
using System.ComponentModel.DataAnnotations;

namespace FLEX.API.Models
{
    public class TZ_USER_MS
    {
        [Key]
        public string USER_ACCOUNT { get; set; }
        public string MENU_SET_CD { get; set; }
        public string GROUP_CD { get; set; }
        public string DEPARTMENT_CD { get; set; }
        public string LANG_CD { get; set; }
        public decimal DATE_FORMAT { get; set; }
        public string PASS { get; set; }
        public string FULL_NAME { get; set; }
        public DateTime? APPLY_DATE { get; set; }
        public decimal FLG_RESIGN { get; set; }
        public decimal FLG_ACTIVE { get; set; }
        public decimal FLG_ABSENCE { get; set; }
        public string EMAILADDR { get; set; }
        public string CRT_BY { get; set; }
        public DateTime CRT_DATE { get; set; }
        public string CRT_MACHINE { get; set; }
        public string UPD_BY { get; set; }
        public DateTime? UPD_DATE { get; set; }
        public string UPD_MACHINE { get; set; }
        public string UPPER_USER_ACCOUNT { get; set; }
        public string LOWER_USER_ACCOUNT { get; set; }
        public byte[] SIGNATURE { get; set; }
        public int? DIVISIONID { get; set; }
        public int? BRANCHID { get; set; }
        public int? DEPARTMENTID { get; set; }
        public int DISPLAY_DATE_FORMAT { get; set; }
        public int DISPLAY_DATE_FORMAT_LONG { get; set; }
    }
}
