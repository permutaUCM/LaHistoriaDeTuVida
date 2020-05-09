
using System;
using LHDTV.Service;
using LHDTV.Models.Forms;
using LHDTV.Models.DbEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;


namespace LHDTV.Controllers
{
    [ApiController]
    [Route("api/folder")]
    [Authorize(Roles = "USER,ADMIN")]
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

            var folder = folderService.GetFolder(id, 1);

            var types = photoService.GetTagTypes();
            if (folder == null)
            {
                return BadRequest(localizer["folderNotFound"].Value);
            }
            return Ok(new
            {
                Metadata = folderService.GetMetadata(),
                //  new
                // {
                //     types = types
                // },
                Data = folder
            });

        }

        [HttpPost("update")]
        public ActionResult UpdateFolder([FromBody]UpdateFolderForm form)
        {

            return Ok(folderService.Update(form, 1));

        }


        [HttpPost("add")]
        public ActionResult addFolder([FromForm]AddFolderForm form)
        {

            var ret = folderService.Create(form, 1);
            return Ok(ret);

        }


        [HttpGet("delete/{folderId}")]
        public ActionResult delete(int folderId)
        {
            return Ok(folderService.Delete(folderId, 1));
        }

        //addPhotoToFolder
        [HttpPost("addPhoto")]
        public ActionResult addPhotoToFolder(int folderId,  int photoId)
        {

            return Ok(folderService.addPhotoToFolder(folderId, photoId, 1));

        }

        //deletePhotoToFolder
        [HttpPost("deletePhoto")]
        public ActionResult deletePhotoToFolder(int folderId, int photo)
        {

            return Ok(folderService.deletePhotoToFolder(folderId, photo, 1));
        }

        //updateDefaultPhotoToFolder
        [HttpPost("updateDefaultPhoto")]
        public ActionResult updateDefaultPhotoToFolder(int folderId, int photo)
        {

            return Ok(folderService.updateDefaultPhotoToFolder(folderId, photo, 1));
        }

        [HttpGet("all")]
        public ActionResult getAllFolders([FromQuery]Pagination pag)
        {

            try
            {
                var folders = folderService.GetAll(pag, 1);

                return Ok(new
                {
                    Metadata = new
                    {
                        Pag = pag,
                        PagCount = 150,
                    },
                    Data = folders
                });

            }
            catch (Exception e)
            {
                logger.LogError("Unespected error: " + e.StackTrace);
                return BadRequest(localizer["ERROR_DEFAULT"]);
            }
        }


    }



}