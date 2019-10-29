using FLEX.API.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FLEX.API.Modules.Services.PMS
{
    public interface IPMSDataSvc
    {
    }

    public class PMSDataSvc : IPMSDataSvc
    {
        private readonly FLEXContext ct;

        public PMSDataSvc(FLEXContext context)
        {
            ct = context;
        }
    }
}
