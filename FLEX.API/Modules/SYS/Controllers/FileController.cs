using System;
using System.Collections.Generic;
using System.Linq;
using FLEX.API.Models;
using FLEX.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json.Linq;

namespace FLEX.API.Modules.SYS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFlexDataSvc svc;
        public FileController(IFlexDataSvc service)
        {
            svc = service;
        }


        [HttpGet("{MACHINE_NO}/{FILE_ID}")]
        public ActionResult DownloadAttachment(string MACHINE_NO, string FILE_ID)
        {
            var attachment = svc.LoadMachineAttachment(MACHINE_NO, FILE_ID);
            string contentType;
            if (!new FileExtensionContentTypeProvider().TryGetContentType(attachment.DisplayName, out contentType))
                contentType = "application/unknown";

            return PhysicalFile(attachment.PhysicalName, contentType, attachment.DisplayName);
        }
    }
}
