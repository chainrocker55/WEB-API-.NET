using System;
using FLEX.API.Common;
using FLEX.API.Models;
using FLEX.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FLEX.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IFlexDataSvc systemSvc;
        public AuthController(IFlexDataSvc service)
        {
            systemSvc = service;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("Service Started");
        }

        [HttpPost]
        public ActionResult<UserInfo> Post(AuthModel data)
        {
            try
            {
                //var data = new AuthModel()
                //{
                //    UserCd = UserCd,
                //    Password = Password,
                //};
                if (data == null || string.IsNullOrEmpty(data.UserCd) || string.IsNullOrEmpty(data.Password)) return BadRequest();

                data.Password = Encryption.MD5EncryptString(data.UserCd, data.Password);

                var result = systemSvc.UserLogin(data.UserCd, data.Password);
                if (result == null)
                {
                    return BadRequest("Invalid Username or Password");
                }
                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }
    }
}