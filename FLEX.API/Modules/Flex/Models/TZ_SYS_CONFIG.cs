using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Models
{
    public class TZ_SYS_CONFIG
    {
        
        public virtual string SYS_GROUP_ID
        {
            get;
            set;
        }
        
        public virtual string SYS_KEY
        {
            get;
            set;
        }
        
        public virtual string CHAR_DATA
        {
            get;
            set;
        }
        
        public virtual Nullable<int> EDIT_FLAG
        {
            get;
            set;
        }
    }
}
