using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FLEX.API.Modules.PMS.Models;
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

        [HttpPost]
        public ActionResult<List<PMS060_CheckListAndRepairOrder_Result>> sp_PMS060_GetMachineRepairOrderList(PMS060_Search_Criteria criteria)
        {
            var result = svc.sp_PMS060_GetMachineRepairOrderList(criteria);
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<PMS061_DTO> sp_PMS061_GetCheckJob(PMS060_CheckListAndRepairOrder_Result row)
        {
            if (row == null) return BadRequest();

            PMS061_DTO data = new PMS061_DTO();
            data.Header = svc.sp_PMS061_GetCheckJobH(row.CHECK_REPH_ID).SingleOrDefault();
            data.HeaderOverHaul = svc.sp_PMS061_GetCheckJobH_OH(row.CHECK_REPH_ID).SingleOrDefault();
            return Ok(data);
        }

    }
}
