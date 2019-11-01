using System;
using System.ComponentModel.DataAnnotations;

namespace FLEX.API.Modules.PMS.Models
{
    public class PMS061_GetCheckJobH_Result
    {
        [Key]
        public string CHECK_REPH_ID
        {
            get;
            set;
        }
        public string CHECK_REP_NO
        {
            get;
            set;
        }
        public Nullable<int> MACHINE_SCHEDULEID
        {
            get;
            set;
        }
        public string MACHINE_NO
        {
            get;
            set;
        }
        public string MACHINE_NAME
        {
            get;
            set;
        }
        public Nullable<System.DateTime> PLAN_DATE
        {
            get;
            set;
        }
        public Nullable<System.DateTime> REQUEST_DATE
        {
            get;
            set;
        }
        public Nullable<System.DateTime> COMPLETE_DATE
        {
            get;
            set;
        }
        public Nullable<System.TimeSpan> COMPLETE_TIME
        {
            get;
            set;
        }
        public Nullable<System.DateTime> TEST_DATE
        {
            get;
            set;
        }
        public string MACHINE_LOC_CD
        {
            get;
            set;
        }
        public string MACHINE_LOC
        {
            get;
            set;
        }
        public Nullable<int> APPROVE_ID
        {
            get;
            set;
        }
        public Nullable<int> CUR_LEVEL
        {
            get;
            set;
        }
        public Nullable<int> NEXT_LEVEL
        {
            get;
            set;
        }
        public Nullable<decimal> PERIOD
        {
            get;
            set;
        }
        public Nullable<int> PERIOD_ID
        {
            get;
            set;
        }
        public string COMPLETE_MTN
        {
            get;
            set;
        }
        public string COMPLETE_RQ
        {
            get;
            set;
        }
        public Nullable<int> SCHEDULE_TYPEID
        {
            get;
            set;
        }
        public string AUTO_CREATEFLAG
        {
            get;
            set;
        }
        public string CHECK_REP_NO_REF
        {
            get;
            set;
        }
        public string REVISE_REMARK
        {
            get;
            set;
        }
        public string CANCEL_REMARK
        {
            get;
            set;
        }
        public string PRINT_FLAG
        {
            get;
            set;
        }
        public string STATUSID
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
