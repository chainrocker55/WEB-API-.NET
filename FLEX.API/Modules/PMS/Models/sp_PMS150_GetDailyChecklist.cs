using System;
using System.ComponentModel.DataAnnotations;

namespace FLEX.API.Models
{
    public class sp_PMS150_GetDailyChecklist
    {
        [Key]
        public int DAILY_CHECKLIST_HID
        {
            get;
            set;
        }
        public string DAILY_CHECKLIST_NO
        {
            get;
            set;
        }
        public Nullable<int> LINEID
        {
            get;
            set;
        }
        public Nullable<System.DateTime> CHECK_DATE
        {
            get;
            set;
        }
        public Nullable<int> SHIFTID
        {
            get;
            set;
        }
        public string SHIFT_DESC
        {
            get;
            set;
        }
        public string CHECKER
        {
            get;
            set;
        }
        public string STATUSID
        {
            get;
            set;
        }
        public string STATUS_DESC
        {
            get;
            set;
        }
        public string CREATEUSERID
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
        public Nullable<System.DateTime> LASTUPDATEDATETIME
        {
            get;
            set;
        }
    }
}
