using System.Collections.Generic;
using FLEX.API.Models;
using FLEX.API.Modules.Flex.Models.Combo;
using FLEX.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FLEX.API.Modules.Flex.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ComboController : ControllerBase
    {
        private readonly IComboDataSvc svc;
        public ComboController(IComboDataSvc service)
        {
            svc = service;
        }

        [HttpGet]
        public ActionResult<List<TZ_LANG_MS>> GetLanguage()
        {
            var result = svc.GetLanguage();
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<TZ_USER_GROUP_MS>> GetUserGroup()
        {
            var result = svc.GetUserGroup();
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<TZ_MENU_SET_MS>> GetMenuSet()
        {
            var result = svc.GetMenuSet();
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<TBM_DIVISION>> GetDivision()
        {
            var result = svc.GetDivision();
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<TBM_POSITION>> GetPosition()
        {
            var result = svc.GetPosition();
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<ComboPersonInCharge_KIBUN_Result>> GetComboPersonInCharge_KIBUN()
        {
            var result = svc.GetComboPersonInCharge_KIBUN();
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<ComboStringValue>> GetComboUserWithPosition()
        {
            var result = svc.GetComboUserWithPosition();
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<ComboStringValue>> GetComboLocation()
        {
            var result = svc.GetComboLocation();
            return Ok(result);
        }

        [HttpGet("{userCd}")]
        public ActionResult<List<ComboStringValue>> GetComboUserApproveLocation(string userCd)
        {
            var result = svc.GetComboUserApproveLocation(userCd);
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<ComboIntValue>> GetComboSupplier(bool IsIncludeDelete = false)
        {
            var result = svc.GetComboSupplier(IsIncludeDelete);
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<ComboIntValue>> GetComboMachineScheduleType()
        {
            var result = svc.GetComboMachineScheduleType();
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<ComboStringValue>> GetComboMachineStatus()
        {
            var result = svc.GetComboMachineStatus();
            return Ok(result);
        }

        //[HttpGet("{IsInculdeAll}")]
        //public ActionResult<List<ComboStringValue>> GetComboMachine(bool IsInculdeAll)
        [HttpGet]
        public ActionResult<List<ComboStringValue>> GetComboMachine(bool? excludeDelete)
        {
            var result = svc.GetComboMachine(excludeDelete);
            return Ok(result);
        }
        [HttpGet]
        public ActionResult<List<ComboStringValue>> GetComboPoNumber()
        {
            var result = svc.GetComboPoNumber();
            return Ok(result);
        }
        [HttpGet]
        public ActionResult<List<ComboIntValue>> GetComboMachinePeriod()
        {
            var result = svc.GetComboMachinePeriod();
            return Ok(result);
        }
        [HttpGet]
        public ActionResult<List<ComboIntValue>> GetComboMachineComponent(string MACHINE_NO)
        {
            var result = svc.GetComboMachineComponent(MACHINE_NO);
            return Ok(result);
        }
        [HttpGet]
        [HttpPost]
        public ActionResult<List<ComboStringValue>> GetComboItemUnit(JObject param)
        {
            var ITEM_CD = param.GetValue("ITEM_CD")?.ToObject<string>();
            var UNIT_SETTING  = param.GetValue("UNIT_SETTING")?.ToObject<string>();
            var SHOW_CODE = param.GetValue("SHOW_CODE")?.ToObject<bool?>();

            var result = svc.GetComboItemUnit(ITEM_CD, UNIT_SETTING, SHOW_CODE??false);
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<ComboStringValue>> GetComboUnit(bool? SHOW_CODE)
        {
            var result = svc.GetComboUnit(SHOW_CODE);
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<ComboStringValue>> GetComboShiftTypeDayNight(string DAY_SHIFT, string NIGHT_SHIFT, string OVERTIME_SHIFT)
        {
            var result = svc.GetComboShiftTypeDayNight(DAY_SHIFT,NIGHT_SHIFT,OVERTIME_SHIFT);
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<ComboStringValue>> GetComboLineCode()
        {
            var result = svc.GetComboLineCode();
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<ComboStringValue>> GetCombotDailyChecklistStatus()
        {
            var result = svc.GetCombotDailyChecklistStatus();
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<ComboStringValue>> GetComboByClsInfoCD(string cls_info)
        {
            var result = svc.GetComboByClsInfoCD(cls_info);
            return Ok(result);

            
        }


    }
}
