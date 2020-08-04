
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
        private readonly ITokenRecoveryService tokenRecovery;



        public FolderController(IFolderService _folderService, IStringLocalizer<FolderController> _localizer,
                          ILogger<FolderController> _logger, IConfiguration _configuration,
                          IPhotoService _photoService, ITokenRecoveryService tokenRecovery)
        {
            folderService = _folderService;
            localizer = _localizer;
            logger = _logger;
            basePath = _configuration.GetValue<string>(BASEPATHCONF);
            photoService = _photoService;
            this.tokenRecovery = tokenRecovery;
        }

        [HttpGet("{id}")]
        public ActionResult getFolder(int id)
        {

            logger.LogInformation("getFolder: id: ", id);

            var userId = this.tokenRecovery.RecoveryId(this.tokenRecovery.RecoveryToken(HttpContext));
            var folder = folderService.GetFolder(id, userId);

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

        [HttpPost("filtered/{folderId}")]
        public ActionResult getFolder([FromBody] Pagination pag, int folderId)
        {

            logger.LogInformation("getFolder: id: ", folderId);

            var userId = this.tokenRecovery.RecoveryId(this.tokenRecovery.RecoveryToken(HttpContext));
            var folder = folderService.GetFolder(folderId, pag, userId);

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
        public ActionResult UpdateFolder([FromBody] UpdateFolderForm form)
        {

            var userId = this.tokenRecovery.RecoveryId(this.tokenRecovery.RecoveryToken(HttpContext));
            return Ok(folderService.Update(form, userId));

        }


        [HttpPost("add")]
        public ActionResult addFolder([FromBody] AddFolderForm form)
        {
            var userId = this.tokenRecovery.RecoveryId(this.tokenRecovery.RecoveryToken(HttpContext));
            var ret = folderService.Create(form, userId);
            return Ok(new
            {
                Metadata = new { },
                Data = ret
            });

        }


        [HttpGet("delete/{folderId}")]
        public ActionResult delete(int folderId)
        {
            var userId = this.tokenRecovery.RecoveryId(this.tokenRecovery.RecoveryToken(HttpContext));
            return Ok(folderService.Delete(folderId, userId));
        }

        //addPhotoToFolder
        [HttpPost("addPhoto")]
        public ActionResult addPhotoToFolder([FromBody] AddPhotoToFolderForm form)
        {

            var userId = this.tokenRecovery.RecoveryId(this.tokenRecovery.RecoveryToken(HttpContext));

            return Ok(new
            {
                Metadata = new { },
                Data = folderService.addPhotoToFolder(form.FolderId, form.PhotosIds, userId)
            });

        }

        //deletePhotoToFolder
        [HttpPost("deletePhoto")]
        public ActionResult deletePhotoToFolder([FromBody] RemoveFromFolder form)
        {
            var userId = this.tokenRecovery.RecoveryId(this.tokenRecovery.RecoveryToken(HttpContext));

            return Ok(new
            {
                Metadata = new { },
                Data = folderService.deletePhotoToFolder(form.FolderId, form.PhotosIds, userId)
            });
        }

        //updateDefaultPhotoToFolder
        [HttpPost("updateDefaultPhoto")]
        public ActionResult updateDefaultPhotoToFolder([FromBody] UpdateFolderPhotoForm form)
        {

            var userId = this.tokenRecovery.RecoveryId(this.tokenRecovery.RecoveryToken(HttpContext));
            var newFolder = folderService.updateDefaultPhotoToFolder(form.FolderId, form.PhotoId, userId);
            return Ok(new
            {
                metadata = new { },
                data = newFolder
            });
        }

        [HttpPost("all")]
        public ActionResult getAllFolders([FromBody] Pagination pag)
        {
            try
            {
                var userId = this.tokenRecovery.RecoveryId(this.tokenRecovery.RecoveryToken(HttpContext));
                var folders = folderService.GetAll(pag, userId);

                return Ok(new
                {
                    Metadata = new
                    {
                        Pag = pag,
                        PagCount = 150,
                        FolderMetadata = folderService.GetMetadata(),

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

        [HttpGet("folderMetadata")]
        public ActionResult getMetadata([FromQuery] Pagination pag)
        {
            try
            {
                var userId = this.tokenRecovery.RecoveryId(this.tokenRecovery.RecoveryToken(HttpContext));
                var folders = folderService.GetAll(pag, userId);

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