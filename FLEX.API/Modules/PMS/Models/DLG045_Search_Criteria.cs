using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.PMS.Models
{
    public class DLG045_Search_Criteria
    {
        public bool? ShowDeleted { get; set; }
        public bool? ShowStopItem{get;set;}
        public List<String> FilterItemCls{get;set;}
        public List<int> FilterItemCategory{get;set;}
    }
}
