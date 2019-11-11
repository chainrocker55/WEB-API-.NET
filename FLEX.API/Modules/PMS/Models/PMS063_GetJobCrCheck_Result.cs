using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.PMS.Models
{
    public class PMS063_GetJobCrCheck_Result
    {
       
        public string CHECK_REPH_ID
        {
            get;
            set;
        }
       
        public Nullable<int> CHECK_MC_ID
        {
            get;
            set;
        }
       
        public Nullable<System.DateTime> CHECK_MC_DATE_FR
        {
            get;
            set;
        }
       
        public Nullable<System.DateTime> CHECK_MC_DATE_TO
        {
            get;
            set;
        }
       
        public string CHECK_MC_PERSON
        {
            get;
            set;
        }
       
        public Nullable<int> CHECK_MC_POSITIONID
        {
            get;
            set;
        }
       
        public Nullable<System.DateTime> CHECK_MC_DATE
        {
            get;
            set;
        }
       
        public string CLEAN_FLAG
        {
            get;
            set;
        }
       
        public string CLEAN_PERSON
        {
            get;
            set;
        }
       
        public Nullable<int> CLEAN_POSITIONID
        {
            get;
            set;
        }
       
        public Nullable<System.DateTime> CLEAN_DATE
        {
            get;
            set;
        }
       
        public string QC_CHECK
        {
            get;
            set;
        }
       
        public string QC_CHECK_DESC
        {
            get;
            set;
        }
       
        public string QC_PERSON
        {
            get;
            set;
        }
       
        public Nullable<int> QC_POSITIONID
        {
            get;
            set;
        }
       
        public Nullable<System.DateTime> QC_DATE
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
