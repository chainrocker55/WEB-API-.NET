using System;
using System.ComponentModel.DataAnnotations;

namespace FLEX.API.Models
{
    public class TZ_SCREEN_DETAIL_LANG_MS
    {
        [Required, MaxLength(240)]
        public string CONTROL_CD { get; set; }
        [Required, MaxLength(40)]
        public string LANG_CD { get; set; }
        [Required, MaxLength(120)]
        public string SCREEN_CD { get; set; }
        [MaxLength(400)]
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
