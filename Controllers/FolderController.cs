
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
        private readonly IPhotoService photoService;
        private readonly IStringLocalizer<FolderController> localizer;

        private readonly ILogger<FolderController> logger;

        private readonly string basePath;

        private const string BASEPATHCONF = "folderRoutes:uploadRoute";


        public FolderController(IFolderService _folderService, IStringLocalizer<FolderController> _localizer,
                          ILogger<FolderController> _logger, IConfiguration _configuration,
                          IPhotoService _photoService)
        {
            folderService = _folderService;
            localizer = _localizer;
            logger = _logger;
            basePath = _configuration.GetValue<string>(BASEPATHCONF);
            photoService = _photoService;
        }

        [HttpGet("{id}")]
        public ActionResult getFolder(int id)
        {

            logger.LogInformation("getFolder: id: ", id);

            var folder = folderService.GetFolder(id);

            var types = photoService.GetTagTypes();
            if (folder == null)
            {
                return BadRequest(localizer["folderNotFound"].Value);
            }
            return Ok(new
            {
                Metadata = new
                {
                    types = types
                },
                Data = folder
            });

        }

        [HttpPost("updateFolder")]
        public ActionResult UpdateFolder([FromBody]UpdateFolderForm form)
        {

            return Ok(folderService.Update(form));

        }


        [HttpPost("addFolder")]
        public ActionResult addFolder([FromForm]AddFolderForm form)
        {

            var ret = folderService.Create(form);
            return Ok(ret);

        }


        [HttpGet("delete/{folderId}")]
        public ActionResult delete(int folderId)
        {
            return Ok(folderService.Delete(folderId));
        }

        //addPhotoToFolder
        [HttpPost("addPhotoToFolder")]
        public ActionResult addPhotoToFolder(int folderId, int photoId){

                return Ok(folderService.addPhotoToFolder(folderId,photoId));

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