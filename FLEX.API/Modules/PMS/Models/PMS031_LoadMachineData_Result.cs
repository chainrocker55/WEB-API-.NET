using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.PMS.Models
{
    public class PMS031_LoadMachineData_Result
    {
        
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
        
        public string FIXEDASSET_NO
        {
            get;
            set;
        }
        
        public string MCBOM_CD
        {
            get;
            set;
        }
        
        public Nullable<System.DateTime> ACQUIRED_DATE
        {
            get;
            set;
        }
        
        public Nullable<System.DateTime> CANCEL_DATE
        {
            get;
            set;
        }
        
        public Nullable<int> LINEID
        {
            get;
            set;
        }
        
        public string MACHINE_LOC
        {
            get;
            set;
        }
        
        public string LOC_CD
        {
            get;
            set;
        }
        
        public string MODEL
        {
            get;
            set;
        }
        
        public string STOP_FLAG
        {
            get;
            set;
        }
        
        public Nullable<decimal> STD_COST
        {
            get;
            set;
        }
        
        public string STD_COST_CURR
        {
            get;
            set;
        }
        
        public Nullable<decimal> OH_TIME_OPER_HR
        {
            get;
            set;
        }
        
        public Nullable<decimal> PM_PERIOD
        {
            get;
            set;
        }
        
        public Nullable<int> PM_PERIOD_ID
        {
            get;
            set;
        }
        
        public Nullable<decimal> CR_PERIOD1
        {
            get;
            set;
        }
        
        public Nullable<int> CR_PERIOD1_ID
        {
            get;
            set;
        }
        
        public Nullable<decimal> CR_PERIOD2
        {
            get;
            set;
        }
        
        public Nullable<int> CR_PERIOD2_ID
        {
            get;
            set;
        }
        
        public byte[] IMAGE_NAME
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
