using System;
using System.ComponentModel.DataAnnotations;

namespace FLEX.API.Models
{
    public class TZ_LANG_MS
    {
        [Key]
        public string LANG_CD { get; set; }
        public string LANG_NAME { get; set; }
        public string IS_DEFAULT { get; set; }
        public string CRT_BY { get; set; }
        public DateTime CRT_DATE { get; set; }
        public string CRT_MACHINE { get; set; }
        public string UPD_BY { get; set; }
        public DateTime? UPD_DATE { get; set; }
        public string UPD_MACHINE { get; set; }
    }
}
