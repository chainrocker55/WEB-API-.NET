using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FLEX.API.Modules.Services.PMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FLEX.API.Modules.PMS.Models;
using System.Transactions;
using FLEX.API.Common.Utils;
using FLEX.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json.Linq;


namespace FLEX.API.Modules.PMS.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChecklistController : ControllerBase
    {
        const string STATUS_NEW = "L01"; // New Checklist
        const string STATUS_DURING = "L02"; // During Checking
        const string STATUS_ISSUED = "L03"; // Issued Repair Order
        const string STATUS_COMPLETED = "L04"; // Completed Repair
        const string STATUS_CANCEL = "L05"; // Cancel Checklist
        const string STATUS_ISSUED_APPROVED = "L06"; // Issued Repair Order/Approved
        const string STATUS_COMPLETED_APPROVED = "L07"; // Completed Repair/Approved
        const string STATUS_IN_PROGRESS = "L99"; // In-Progress

        private readonly IPMSDataSvc svc;
        public ChecklistController (IPMSDataSvc service)
        {
            svc = service;
        }
        [HttpPost]
        public ActionResult GetDailyChecklist(PMS150_Search_Criteria data)
        {
            try
            {
                var result = svc.sp_PMS150_GetDailyChecklist(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException());
            }
        }

    }
}