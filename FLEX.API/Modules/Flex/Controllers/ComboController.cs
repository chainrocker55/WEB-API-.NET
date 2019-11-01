using System.Collections.Generic;
using FLEX.API.Models;
using FLEX.API.Modules.Flex.Models.Combo;
using FLEX.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}
