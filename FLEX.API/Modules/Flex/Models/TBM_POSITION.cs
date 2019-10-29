using System;
using System.ComponentModel.DataAnnotations;

namespace FLEX.API.Models
{
    public class TBM_POSITION
    {
        [Key]
        public int POSITIONID { get; set; }
        public string POSITIONCODE { get; set; }
        public string POSITIONNAME { get; set; }
        public string REMARK { get; set; }
        public string CREATEUSERID { get; set; }
        public DateTime? CREATEDATETIME { get; set; }
        public string CREATEMACHINE { get; set; }
        public string LASTUPDATEUSERID { get; set; }
        public DateTime? LASTUPDATEDATETIME { get; set; }
        public string LASTUPDATEMACHINE { get; set; }
        public string DELETEFLAG { get; set; }
        public string DELETEUSERID { get; set; }
        public DateTime? DELETEDATETIME { get; set; }
        public string DELETEMACHINE { get; set; }
    }
}
