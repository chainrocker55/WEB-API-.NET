using System;
using System.ComponentModel.DataAnnotations;

namespace FLEX.API.Models
{
    public class TBM_DIVISION
    {
        [Key]
        public int DIVISIONID { get; set; }
        public string CODE { get; set; }
        public string NAMEENG { get; set; }
        public string NAMETHA { get; set; }
        public int? DIVISIONLEVEL { get; set; }
        public int? PARENT_DIVISIONID { get; set; }
        public string CREATEUSERID { get; set; }
        public string CREATEMACHINE { get; set; }
        public DateTime? CREATEDATETIME { get; set; }
        public string LASTUPDATEUSERID { get; set; }
        public string LASTUPDATEMACHINE { get; set; }
        public DateTime? LASTUPDATEDATETIME { get; set; }
        public string DELETEFLAG { get; set; }
        public string DELETEUSERID { get; set; }
        public string DELETEMACHINE { get; set; }
        public DateTime? DELETEDATETIME { get; set; }
        public string ORDER_PROCESS_CLS { get; set; }
    }
}
