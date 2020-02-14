using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.PMS.Models
{
    public class sp_PMS151_GetDailyChecklist_Detail_Item
    {
        #region Primitive Properties
        
        public int DAILY_CHECKLIST_HID
        {
            get;
            set;
        }
        
        public string MACHINE_NO
        {
            get;
            set;
        }
        
        public int CHECKLISTITEMID
        {
            get;
            set;
        }
        
        public string CHECKLISTITEM_DESC
        {
            get;
            set;
        }
        
        public string CHECK_FLAG
        {
            get;
            set;
        }
        
        public Nullable<bool> OK
        {
            get;
            set;
        }
        
        public Nullable<bool> NG
        {
            get;
            set;
        }
        
        public string NG_REASON
        {
            get;
            set;
        }
        
        public string REMARK
        {
            get;
            set;
        }
        
        public string CHECK_REPH_ID
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

        #endregion
    }
}
