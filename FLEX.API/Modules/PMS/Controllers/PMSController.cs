using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using FLEX.API.Common.Utils;
using FLEX.API.Models;
using FLEX.API.Modules.PMS.Models;
using FLEX.API.Modules.Services.PMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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
                data.Header.MACHINE_NO = row.MACHINE_NO;
                data.Header.MACHINE_NAME = row.MACHINE_NAME;
                data.Header.MACHINE_LOC = row.MACHINE_LOC;

                if (row.SCHEDULE_TYPEID == 1)
                {
                    data.Header.COMPLETE_DATE = row.COMPLETE_DATE;
                }else if (row.SCHEDULE_TYPEID == 2)
                {
                    data.Header.TEST_DATE = row.TEST_DATE;
                    data.Header.PERIOD = row.PERIOD;
                    data.Header.PERIOD_ID = row.PERIOD_ID;
                    data.Header.MACHINE_LOC = row.MACHINE_LOC;
                    data.Header.MACHINE_LOC_CD = row.MACHINE_LOC_CD;

                    
                }
            }

            if (data.Header.SCHEDULE_TYPEID == 1) // over haul
            {
                data.HeaderOverHaul = svc.sp_PMS061_GetCheckJobH_OH(row.CHECK_REPH_ID).SingleOrDefault();
                if (data.HeaderOverHaul == null)
                    data.HeaderOverHaul = new PMS061_GetCheckJobH_OH_Result();
            }

            if (data.Header.SCHEDULE_TYPEID == 2) // pm
            {
                data.DefaultComponent = svc.sp_PMS062_GetMachineDefaultComponent(data.Header.MACHINE_NO);
                data.PmChecklist = svc.sp_PMS062_GetJobPmChecklist(row.CHECK_REPH_ID, row.MACHINE_NO).ToList();
                if (data.PmChecklist == null)
                    data.PmChecklist = new List<PMS062_GetJobPmChecklist_Result>();

                data.PmParts = svc.sp_PMS062_GetJobPmPart(data.Header.CHECK_REPH_ID, null, null).ToList();
                if (data.PmParts == null)
                    data.PmParts = new List<PMS062_GetJobPmPart_Result>();

            }


            //data.PersonInCharge = svc.sp_PMS061_GetCheckJobPersonInCharge(row.CHECK_REPH_ID, row.MACHINE_NO).ToList();
            data.PersonInCharge = svc.sp_PMS061_GetCheckJobPersonInCharge(row.CHECK_REPH_ID, null).ToList();
            if (data.PersonInCharge == null)
                data.PersonInCharge = new List<PMS061_GetCheckJobPersonInCharge_Result>();

            return Ok(data);
        }

        [HttpPost]
        public ActionResult<PMS063_DTO> sp_PMS063_LoadData(PMS060_CheckListAndRepairOrder_Result row)
        {
            if (row == null) return BadRequest();

            PMS063_DTO data = new PMS063_DTO();
            data.Header = svc.sp_PMS063_GetCrHeader(row.CHECK_REPH_ID).SingleOrDefault();
            if (data.Header == null)
            {
                data.Header = new PMS063_GetCrHeader_Result();
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

                data.Header.TEST_DATE = row.TEST_DATE;
                data.Header.PERIOD = row.PERIOD;
                data.Header.PERIOD_ID = row.PERIOD_ID;
                data.Header.MACHINE_LOC_CD = row.MACHINE_LOC_CD;

            }

            data.DefaultComponent = svc.sp_PMS062_GetMachineDefaultComponent(data.Header.MACHINE_NO);

            data.Check=svc.sp_PMS063_GetJobCrCheck(row.CHECK_REPH_ID).SingleOrDefault();
            if (data.Check == null)
                data.Check = new PMS063_GetJobCrCheck_Result();

            data.AfterService = svc.sp_PMS063_GetJobCrAfterService(row.CHECK_REPH_ID).SingleOrDefault();
            if (data.AfterService == null)
                data.AfterService = new PMS063_GetJobCrAfterService_Result();

            data.Tools = svc.sp_PMS063_GetJobCrPart(row.CHECK_REPH_ID, 1).ToList();
            if (data.Tools == null)
                data.Tools = new List<PMS063_GetJobCrPart_Result>();

            data.Parts = svc.sp_PMS063_GetJobCrPart(row.CHECK_REPH_ID, 2).ToList();
            if (data.Parts == null)
                data.Parts = new List<PMS063_GetJobCrPart_Result>();

            data.PersonalChecklist= svc.sp_PMS063_GetPersonalChecklist(row.CHECK_REPH_ID).ToList();
            if (data.PersonalChecklist == null)
                data.PersonalChecklist = new List<PMS063_GetPersonalChecklist_Result>();

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
        public ActionResult<List<PMS062_GetJobPmPart_Result>> sp_PMS062_GetJobPmPart(PMS062_GetJobPmPart_Criteria c)
        {
            var result = svc.sp_PMS062_GetJobPmPart(c.CHECK_REPH_ID, c.MCBOM_CD, c.PARTS_LOC_CD);

            // get in qty
            if (result != null)
            {
                var xml = XmlUtil.ConvertToXml_Store(result);
                var InQty = svc.sp_PMS062_GetInQty(c.CHECK_REPH_ID, xml);
                foreach (var item in result)
                {
                    // set in qty
                    var qty = InQty.Where(q =>
                        q.PARTS_ITEM_CD == item.PARTS_ITEM_CD
                        && q.PARTS_LOC_CD == item.PARTS_LOC_CD
                        && q.UNITCODE == item.UNITCODE
                    ).Select(q => q.ISSUE_INVQTY).FirstOrDefault();
                    item.IN_QTY = qty;
                }
            }

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<List<PMS062_GetInQty_Result>> sp_PMS062_GetInQty(GetInQtyParam param)
        {
            try
            {
                if (param == null)
                {
                    return Ok(new List<PMS062_GetInQty_Result>());
                }

                var xml = XmlUtil.ConvertToXml_Store(param.ITEMS);
                var result = svc.sp_PMS062_GetInQty(param.CHECK_REPH_ID, xml);
                //return Ok(new List<string>() { result }); ;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException());
            }
        }



        [HttpPost]
        public ActionResult<string> PMS061_SaveData(PMS061_DTO data)
        {
            try
            {
                var result = svc.PMS061_SaveData(data, data.CurrentUser);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException());
            }
        }

       

        [HttpPost]
        public ActionResult<string> PMS062_SaveData(PMS061_DTO data)
        {
            try
            {
                var result = svc.PMS062_SaveData(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException());
            }
        }


        [HttpPost]
        public ActionResult<string> PMS063_SaveData(PMS063_DTO data)
        {
            try
            {
                var result = svc.PMS063_SaveData(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException());
            }
        }

        [HttpPost]
        public ActionResult<string> PMS063_Confirm(PMS063_DTO data)
        {
            try
            {
                var result = svc.PMS063_Confirm(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException());
            }
        }
        [HttpPost]
        public ActionResult<string> PMS063_SendToApprove(PMS063_DTO data)
        {
            try
            {
                var result = svc.PMS063_SendToApprove(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException());
            }
        }
        
        [HttpPost]
        public ActionResult<string> PMS063_Approve(PMS063_DTO data)
        {
            try
            {
                var result = svc.PMS063_Approve(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException());
            }
        }

        [HttpPost]
        public ActionResult<string> PMS063_Revise(PMS063_DTO data)
        {
            try
            {
                var result = svc.PMS063_Revise(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException());
            }
        }

        [HttpPost]
        public ActionResult PMS063_Cancel(PMS063_DTO data)
        {
            try
            {
                svc.PMS063_Cancel(data);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException());
            }
        }

        [HttpPost]
        public ActionResult<string> PMS062_SendToApprove(PMS061_DTO data)
        {
            try
            {
                var result = svc.PMS062_SendToApprove(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException());
            }
        }


        [HttpPost]
        public ActionResult<string> PMS062_Revise(PMS061_DTO data)
        {
            try
            {
                var result = svc.PMS062_Revise(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException());
            }
        }

        [HttpPost]
        public ActionResult PMS062_Cancel(PMS061_DTO data)
        {
            try
            {
                svc.PMS062_Cancel(data);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException());
            }
        }

        [HttpPost]
        public ActionResult<List<DLG045_ItemFindDialogWithParam_Result>> GetItemFindDialogWithParam(DLG045_Search_Criteria criteria)
        {
            try
            {
                var result = svc.sp_DLG045_ItemFindDialogWithParam(criteria);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException());
            }
        }

        [HttpPost]
        public ActionResult<PMS031_LoadMachineData_Result> LoadMachineData(SingleParam data)
        {
            try
            {
                var result = svc.sp_PMS031_LoadMachineData(data.StringValue);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException());
            }
        }

        [HttpPost]
        public ActionResult<List<PMS061_GetCheckJobPersonInCharge_Result>> GetCheckJobPersonInCharge(JObject data)
        {
            try
            {
                var CHECK_REPH_ID = data.GetValue("CHECK_REPH_ID").ToObject<string>();
                var MACHINE_NO = data.GetValue("MACHINE_NO").ToObject<string>();

                var result = svc.sp_PMS061_GetCheckJobPersonInCharge(CHECK_REPH_ID, MACHINE_NO).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException());
            }
        }

        [HttpPost]
        public ActionResult PMS061_Cancel(PMS061_DTO data)
        {
            try
            {
                svc.PMS061_Cancel(data);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException());
            }
        }

        [HttpPost]
        public ActionResult<string> GetMachineDefaultComponent(SingleParam param)
        {
            try
            {
                var result = svc.sp_PMS062_GetMachineDefaultComponent(param.StringValue);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException());
            }
        }

       





    }
}
