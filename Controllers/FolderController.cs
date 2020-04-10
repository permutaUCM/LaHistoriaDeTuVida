
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using LHDTV.Service;
using LHDTV.Models.ViewEntity;
using Microsoft.Extensions.Localization;
using LHDTV.Models.Forms;
using LHDTV.Models.DbEntity;
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

    public class FolderController : ControllerBase
    {

        private readonly IFolderService folderService;

        private readonly IStringLocalizer<FolderController> localizer;

        private readonly ILogger<FolderController> logger;

        private readonly string basePath;

        private const string BASEPATHCONF = "folderRoutes:uploadRoute";

        
        public FolderController(IFolderService _folderService, IStringLocalizer<FolderController> _localizer,
                          ILogger<FolderController> _logger, IConfiguration _configuration)
        {
            folderService = _folderService;
            localizer = _localizer;
            logger = _logger;
            basePath = _configuration.GetValue<string>(BASEPATHCONF);

        }

        [HttpPost]
        public ActionResult getFolder([FromBody]FolderForm form)
        {

            logger.LogInformation("getFolder {@form}" , form);

            var id = form.id;

            var folder = folderService.GetFolder(id);

            if(folder == null)
            {
                return BadRequest(localizer["folderNotFound"].Value);

            }

            return Ok(folder);

        }

        [HttpPost("updateFolder")]
        public ActionResult UpdateFolder ([FromBody]UpdateFolderForm form)
        {

            return Ok(folderService.Update(form));

        }

      
        [HttpPost("addFolder")]
        public ActionResult addFolder ([FromForm]AddFolderForm form)
        {
            
            var ret = folderService.Create(form);
            return Ok(ret);

        }
     

        [HttpGet("delete/{folderId}")]
        public ActionResult delete (int folderId){
            return Ok(folderService.Delete(folderId));
        }

        //addPhotoToFolder
        [HttpPost("addPhotoToFolder")]
        public ActionResult addPhotoToFolder(int folderId, PhotoDb photo){

                return Ok(folderService.addPhotoToFolder(folderId,photo));

        }

        //deletePhotoToFolder
        [HttpPost("deletePhotoToFolder")]
        public ActionResult deletePhotoToFolder(int folderId, PhotoDb photo){

            return Ok(folderService.deletePhotoToFolder(folderId,photo));
        }

        //updateDefaultPhotoToFolder
        [HttpPost("updateDefaultPhotoToFolder")]
        public ActionResult updateDefaultPhotoToFolder(int folderId, PhotoDb photo){

            return Ok(folderService.updateDefaultPhotoToFolder(folderId,photo));
        }
     
        
    }



}