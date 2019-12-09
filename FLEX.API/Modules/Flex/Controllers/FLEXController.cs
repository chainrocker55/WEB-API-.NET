using System;
using System.Collections.Generic;
using FLEX.API.Models;
using FLEX.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FLEX.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FLEXController : ControllerBase
    {
        private readonly IFlexDataSvc svc;
        public FLEXController(IFlexDataSvc service)
        {
            svc = service;
        }

        [HttpPost]
        public ActionResult<List<NavData>> GetMenu(AuthModel data)
        {
            var menus = svc.GetMenu(data.UserCd);
            return Ok(menus);
        }

        [HttpPost]
        public ActionResult<List<TZ_MESSAGE_MS>> GetMessage(LanguageModel data)
        {
            var msg = svc.GetMessage(data?.LangCd);
            return Ok(msg);
        }

        [HttpPost]
        public ActionResult<List<TZ_SCREEN_DETAIL_LANG_MS>> GetScreenDetail(LanguageModel data)
        {
            var labels = svc.GetScreenDetail(data?.LangCd);
            return Ok(labels);
        }

        [HttpPost]
        public ActionResult<string> InsertScreenDetail(List<TZ_SCREEN_DETAIL_LANG_MS> dataList)
        {
            svc.InsertScreenDetail(dataList);
            return Ok("OK");
        }

        [HttpPost]
        public ActionResult<TZ_USER_MS> GetUserProfile(AuthModel data)
        {
            var user = svc.GetUserProfile(data?.UserCd);
            if (user != null) { user.PASS = null; }
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<List<Notify>> GetNotify(AuthModel data)
        {
            var result = svc.GetNotify(data?.UserCd);
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<bool> ResponseNotify(Notify data)
        {
            try
            {
                var result = svc.ResponseNotify(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult<string> GetToken(UserInfo user)
        {
            var t = this.svc.GetToken(user);
            return Ok(t);
        }

        [HttpGet("{userGroup}")]
        public ActionResult<List<ActivePermissionValue>> GetActivePermission(string userGroup)
        {
            var result = svc.GetActivePermission(userGroup);
            return Ok(result);
        }

        [HttpGet("{userGroup}")]
        public ActionResult<List<ActivePermissionValue>> GetSpecialPermission(string userGroup)
        {
            var result = svc.GetSpecialPermission(userGroup, null);
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<TBM_STATUS>> GetStatusList()
        {
            var result = svc.GetStatusList();
            return Ok(result);
        }

        [HttpPost]
        public ActionResult<TZ_SYS_CONFIG> GetSysConfig(JObject data)
        {
            var SYS_GROUP_ID = data.GetValue("SYS_GROUP_ID").ToObject<string>();
            var SYS_KEY = data.GetValue("SYS_KEY").ToObject<string>();

            var result = svc.GetSysConfig(SYS_GROUP_ID, SYS_KEY);
            return Ok(result);
        }

        

    }
}
