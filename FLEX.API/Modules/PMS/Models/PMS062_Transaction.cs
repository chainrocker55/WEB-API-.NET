﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.PMS.Models
{
    public class PMS062_Transaction
    {
       
        public string ITEM_CD { get; set; }

       
        public string LOC_CD { get; set; }

       
        public string LOT_NO { get; set; }

       
        public decimal QTY { get; set; }
    }
}
