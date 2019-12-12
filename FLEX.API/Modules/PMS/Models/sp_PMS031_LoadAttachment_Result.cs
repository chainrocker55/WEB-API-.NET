using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.PMS.Models
{
    public class sp_PMS031_LoadAttachment_Result
    {
        
        public string MACHINE_NO
        {
            get;
            set;
        }
        
        public Nullable<int> FILE_ID
        {
            get;
            set;
        }
        
        public string FILE_NAME
        {
            get;
            set;
        }
        
        public string FILE_NAME_ORG
        {
            get;
            set;
        }
        
        public string FILE_PATH
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
