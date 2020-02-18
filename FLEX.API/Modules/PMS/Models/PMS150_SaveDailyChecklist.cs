using FLEX.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.PMS.Models
{
    public class PMS150_SaveDailyChecklist
    {
        public sp_PMS150_GetDailyChecklist header
        {
            get;
            set;
        }
        public List<sp_PMS151_GetDailyChecklist_Detail> machine
        {
            get;
            set;
        }
        public List<sp_PMS151_GetDailyChecklist_Detail_Item> items
        {
            get;
            set;
        }
        public string userID
        {
            get;
            set;
        }
    }
}
