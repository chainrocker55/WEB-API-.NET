﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.Flex.Models
{
    public class SpecialPermissionResult
    {
        public string SCREEN_CD { get; set; }
        public bool CAN_EXECUTE { get; set; }
        public string METHOD { get; set; }
        public string METHOD_NAME { get; set; }
    }
}
