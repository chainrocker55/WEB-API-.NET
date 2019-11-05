using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
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
            if (data.Header == null)
            {
                data.Header = new PMS061_GetCheckJobH_Result();
                data.Header.STATUSID = row.STATUSID;
                data.Header.SCHEDULE_TYPEID = row.SCHEDULE_TYPEID;
                data.Header.CHECK_REPH_ID = row.CHECK_REPH_ID;
                data.Header.CHECK_REP_NO = row.CHECK_REP_NO;
                data.Header.MACHINE_SCHEDULEID = row.MACHINE_SCHEDULEID;
                data.Header.PLAN_DATE = row.PLAN_DATE;
                data.Header.REQUEST_DATE = row.REQUEST_DATE;
                data.Header.COMPLETE_DATE = row.COMPLETE_DATE;
                data.Header.MACHINE_NO = row.MACHINE_NO;
                data.Header.MACHINE_NAME = row.MACHINE_NAME;
                data.Header.MACHINE_LOC = row.MACHINE_LOC;
            }

            data.HeaderOverHaul = svc.sp_PMS061_GetCheckJobH_OH(row.CHECK_REPH_ID).SingleOrDefault();
            if (data.HeaderOverHaul == null)
                data.HeaderOverHaul = new PMS061_GetCheckJobH_OH_Result();

            data.PersonInCharge = svc.sp_PMS061_GetCheckJobPersonInCharge(row.CHECK_REPH_ID, row.MACHINE_NO).ToList();
            if (data.PersonInCharge == null)
                data.PersonInCharge = new List<PMS061_GetCheckJobPersonInCharge_Result>();

            return Ok(data);
        }

        [HttpPost]
        public ActionResult<List<PMS062_GetJobPmChecklist_Result>> sp_PMS062_GetJobPmChecklist(PMS060_CheckListAndRepairOrder_Result row)
        {
            var result = svc.sp_PMS062_GetJobPmChecklist(row.CHECK_REPH_ID, row.MACHINE_NO);
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<string> PMS061_SaveData(PMS061_DTO data, string user)
        {
            try
            {
                var result = svc.PMS061_SaveData(data, user);
                //return Ok(new List<string>() { result }); ;
                return Ok(result); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException());
            }
        }
    }
}
