using System;
using System.ComponentModel.DataAnnotations;

namespace FLEX.API.Models
{
    public class TZ_SCREEN_DETAIL_LANG_MS
    {
        public string CONTROL_CD { get; set; }
        public string LANG_CD { get; set; }
        public string SCREEN_CD { get; set; }
        public string CONTROL_CAPTION { get; set; }
        public string CRT_BY { get; set; }
        public DateTime CRT_DATE { get; set; }
        public string CRT_MACHINE { get; set; }
        public string UPD_BY { get; set; }
        public DateTime UPD_DATE { get; set; }
        public string UPD_MACHINE { get; set; }
        public int? IS_USED { get; set; }
    }
}
