using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.PMS.Models
{
    public class PMS061_GetCheckJobH_OH_Result
    {
        [Key]
        public string CHECK_REPH_ID
        {
            get;
            set;
        }
        public Nullable<int> VENDORID
        {
            get;
            set;
        }
        public Nullable<int> POHID
        {
            get;
            set;
        }
        public string PONUMBER
        {
            get;
            set;
        }
        public Nullable<decimal> TOTAL_TIME_OPERATION
        {
            get;
            set;
        }
        public string PROBLEM_DESC
        {
            get;
            set;
        }
        public string REPAIR_METHOD
        {
            get;
            set;
        }
        public string CAUSE_DELAY
        {
            get;
            set;
        }
        public string CREATEUSERID
        {
            get;
            set;
        }
        public string CREATEMACHINE
        {
            get;
            set;
        }
        public Nullable<System.DateTime> CREATEDATETIME
        {
            get;
            set;
        }
        public string LASTUPDATEUSERID
        {
            get;
            set;
        }
        public string LASTUPDATEMACHINE
        {
            get;
            set;
        }
        public Nullable<System.DateTime> LASTUPDATEDATETIME
        {
            get;
            set;
        }
        public string DELETEFLAG
        {
            get;
            set;
        }
        public Nullable<System.DateTime> DELETEDATETIME
        {
            get;
            set;
        }
        public string DELETEMACHINE
        {
            get;
            set;
        }
        public string DELETEUSERID
        {
            get;
            set;
        }
    }
}
