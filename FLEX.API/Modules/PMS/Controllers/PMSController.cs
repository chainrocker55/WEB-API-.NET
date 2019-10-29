using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FLEX.API.Modules.Services.PMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FLEX.API.Modules.PMS.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMSController : ControllerBase
    {
        private readonly IPMSDataSvc svc;
        public PMSController(IPMSDataSvc service)
        {
            svc = service;
        }

    }
}
