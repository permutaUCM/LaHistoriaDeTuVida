
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using LHDTV.Service;
using LHDTV.Models.ViewEntity;
using Microsoft.Extensions.Localization;
using LHDTV.Models.Forms;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace LHDTV.Controllers
{
    [ApiController]
    [Route("api/folder")]

    public FolderController : ControllerBase{

        private readonly IFolderService folderService;

        private readonly IStringLocalizer<FolderController> localizer;

        private readonly ILogger<FolderController> logger;

        private readonly string basePath;

        private const string BASEPATHCONF = "folderRoutes:uploadRoute";

        [HttpPost]
        public ActionResult getFolder ()
        {


        }

        
    }



}