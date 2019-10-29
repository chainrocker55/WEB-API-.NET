using System;
using System.ComponentModel.DataAnnotations;

namespace FLEX.API.Models
{
    public class TZ_USER_GROUP_MS
    {
        [Key]
        public string GROUP_CD { get; set; }
        public string GROUP_NAME { get; set; }
        public string CRT_BY { get; set; }
        public DateTime? CRT_DATE { get; set; }
        public string CRT_MACHINE { get; set; }
        public string UPD_BY { get; set; }
        public DateTime? UPD_DATE { get; set; }
        public string UPD_MACHINE { get; set; }
    }
}