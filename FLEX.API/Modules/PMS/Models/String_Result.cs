using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.PMS.Models
{
    public class String_Result
    {
        [Key]
        public string VALUE
        {
            get;
            set;
        }
    }
}
