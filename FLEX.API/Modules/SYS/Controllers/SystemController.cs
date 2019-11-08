using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FLEX.API.Common;
using FLEX.API.Models;
using FLEX.API.Modules.SYS.Models;
using FLEX.API.Modules.SYS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FLEX.API.Modules.SYS.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly ISystemDataSvc svc;
        public SystemController(ISystemDataSvc service)
        {
            svc = service;
        }
        private string UserCd
        {
            get
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var userid = identity?.Claims.SingleOrDefault(x=>x.Type == "USER_CD")?.ToString();
                return userid?.Split(':')[1];
            }
        }

        [HttpGet]
        public ActionResult<List<TB_CLASS_LIST_MS>> GetClassList()
        {
            var result = svc.GetClassList();
            return Ok(result);
        }
        [HttpPost]
        public ActionResult SaveClassList(TB_CLASS_LIST_MS data)
        {
            svc.SaveClassList(data);
            return Ok();
        }

        #region SFM030 - User List
        [HttpGet]
        public ActionResult<List<TZ_USER_MS>> GetUserList()
        {
            var result = svc.GetUserList();
            return Ok(result);
        }
        [HttpPost]
        public ActionResult<sp_SFM031_LoadUser_Result> GetUser(TZ_USER_MS data)
        {
            try
            {
                var result = svc.GetUser(data?.USER_ACCOUNT);
                result.PASS = result.OLD_PASS = null;
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex.GetBaseException();
            }
        }

        [HttpPost]
        public ActionResult SaveUser(sp_SFM031_LoadUser_Result data)
        {
            try
            {
                if (!string.IsNullOrEmpty(data.PASS))
                {
                    data.PASS = Encryption.MD5EncryptString(data.USER_ACCOUNT, data.PASS);
                }
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                svc.SaveUser(data, UserCd);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.GetBaseException());
            }
        }
        #endregion

        #region SFM0060 - Authorized Maintenance
        [HttpGet]
        public ActionResult GetUserGroupList()
        {
            var result = svc.GetUserGroupList();
            return Ok(result);
        }
        [HttpGet("{userGroup}")]
        public ActionResult sp_SFM0061_GetPermission(string userGroup)
        {
            var result = svc.sp_SFM0061_GetStandardPermission(userGroup);
            var sp = svc.sp_SFM0061_GetSpecialPermission(userGroup);
            var g = result.GroupBy(g => new
            {
                SCREEN_CD = g.SCREEN_CD,
                SCREEN_NAME = g.SCREEN_NAME,
            }).Select(x => new SFM0061_GetStandardPermission()
            {
                SCREEN_CD = x.Key.SCREEN_CD,
                SCREEN_NAME = x.Key.SCREEN_NAME,
                Standard = x.ToList(),
                Special = sp.Where(y => y.SCREEN_CD == x.Key.SCREEN_CD).ToList()
            }).ToList();
            return Ok(g);
        }
        #endregion
    }
}
