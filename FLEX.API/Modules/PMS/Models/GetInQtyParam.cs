﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.PMS.Models
{
    public class GetInQtyParam
    {
        public string CHECK_REPH_ID { get; set; }
		public List<InQtyItem> ITEMS { get; set; }
    }

    public class InQtyItem
    {
        public string PARTS_LOC_CD { get; set; }
        public string PARTS_ITEM_CD { get; set; }
        public string UNITCODE { get; set; }
    }
}
