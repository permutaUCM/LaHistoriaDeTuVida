
using System;
using System.Collections.Generic;
using LHDTV.Service;
using LHDTV.Models.Forms;
using LHDTV.Models.DbEntity;
using LHDTV.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using MetadataExtractor.IO;
using MetadataExtractor.Util;
using System.Linq;
namespace LHDTV.Controllers
{
    [ApiController]
    [Route("api/photo")]
    [Authorize(Roles = "USER,ADMIN")]
    public class PhotoController : ControllerBase
    {

        private readonly LHDTVContext context;
        private readonly IPhotoService photoService;
        private readonly IStringLocalizer<PhotoController> localizer;

        private readonly ILogger<PhotoController> logger;

        private readonly string basePath;

        private const string BASEPATHCONF = "photoRoutes:uploadRoute";
        private readonly AppSettings appSettings;
        private readonly ITokenRecoveryService tokenRecovery;

        public PhotoController(IPhotoService _photoService, IStringLocalizer<PhotoController> _localizer,
                          ILogger<PhotoController> _logger, IConfiguration _configuration,
                          IOptions<AppSettings> _appSettings, ITokenRecoveryService tokenRecovery
            )
        {
            photoService = _photoService;
            localizer = _localizer;
            logger = _logger;
            basePath = _configuration.GetValue<string>(BASEPATHCONF);

            appSettings = _appSettings.Value;
            this.tokenRecovery = tokenRecovery;
        }

        [HttpPost]
        public ActionResult getPhoto([FromBody] PhotoForm form)
        {

            logger.LogInformation("getphoto {@form}", form);

            var id = form.id;

            var userId = this.tokenRecovery.RecoveryId(this.tokenRecovery.RecoveryToken(HttpContext));
            var photo = photoService.GetPhoto(id, userId);

            if (photo == null)
            {
                return BadRequest(localizer["photoNotFound"].Value);
            }
            return Ok(photo);
        }

        /*[HttpPost("FileUpload")]

        public async Task<IActionResult> OnPostUploadAsync(IFormFile file)
        {

            long size = file.Length;

            if (size > 0)
            {
                                    var userId = this.tokenRecovery.RecoveryId(this.tokenRecovery.RecoveryToken(HttpContext));
                double dobletemp = new Random().Next(1form, userId000000);
                var extension = file.FileName.Split(".");
                var filePath = Path.Combine(basePath , DateTime.UtcNow.Ticks +"_"+ Math.Round(dobletemp) +"."+ extension[extension.Length - 1]);

                if(!Directory.Exists(basePath)){

                    return BadRequest();
                }
                        using (var stream = new FileStream (filePath,FileMode.Create))
                        {
                             file.CopyTo(stream);
                        }
            }

            return Ok();
        }*/

        [HttpPost("updatePhoto")]
        public ActionResult UpdatePhoto([FromBody] UpdatePhotoForm form)
        {
            var userId = this.tokenRecovery.RecoveryId(this.tokenRecovery.RecoveryToken(HttpContext));
            return Ok(photoService.Update(form, userId));
        }

        [HttpPost("addPhoto")]
        public ActionResult addPhoto([FromForm] AddPhotoForm form)
        {
            var userId = this.tokenRecovery.RecoveryId(this.tokenRecovery.RecoveryToken(HttpContext));
            var ret = photoService.Create(form, userId);
            return Ok(ret);
        }


        [HttpPost("all")]
        public ActionResult getAll([FromBody] Pagination pag)
        {

            try
            {
                var userId = this.tokenRecovery.RecoveryId(this.tokenRecovery.RecoveryToken(HttpContext));
                var photos = photoService.GetAll(pag, userId);

                return Ok(new
                {

                    Metadata = new
                    {
                        Page = pag,
                        PagCount = 150,
                    },
                    Data = photos

                });


            }
            catch (Exception e)
            {

                logger.LogError("Unexpected error: " + e.StackTrace);
                return BadRequest(localizer["ERROR_DEFAULT"]);

            }
        }

        [HttpGet("allTags/{folderId}")]
        public ActionResult getAllTagsNotInFolder(int folderId)
        {
            var userId = this.tokenRecovery.RecoveryId(this.tokenRecovery.RecoveryToken(HttpContext));

            var ret = this.photoService.GetAllTags(userId, folderId);

            return Ok(new
            {
                data = ret
            });
        }

        //Returns all files but the ones in the folder
        [HttpPost("all/{folderId}")]
        public ActionResult getAllNotInFolder([FromBody] Pagination pag, int folderId)
        {

            try
            {
                var userId = this.tokenRecovery.RecoveryId(this.tokenRecovery.RecoveryToken(HttpContext));
                var photos = photoService.GetAll(pag, userId, folderId);


                return Ok(new
                {
                    Metadata = new
                    {
                        Page = pag,
                        PagCount = 150,
                    },
                    Data = photos
                });


            }
            catch (Exception e)
            {

                logger.LogError("Unexpected error: " + e.StackTrace);
                return BadRequest(localizer["ERROR_DEFAULT"]);

            }
        }
        [HttpGet("delete/{photoId}")]
        public ActionResult delete(int photoId)
        {
            var userId = this.tokenRecovery.RecoveryId(this.tokenRecovery.RecoveryToken(HttpContext));
            return Ok(photoService.Delete(photoId, userId));
        }

        [HttpPost("addTag")]
        public ActionResult addTag([FromBody] TagForm tagForm)
        {
            try
            {
                var userId = this.tokenRecovery.RecoveryId(this.tokenRecovery.RecoveryToken(HttpContext));

                var response = new
                {
                    Metadata = new { },
                    Data = photoService.AddTag(tagForm, userId)
                };
                return Ok(response);
            }
            catch (Exceptions.NotFoundException)
            {
                return BadRequest("No se ha encontrado la foto que se desea actualizar.");

            }
            catch (System.Exception e)
            {
                logger.LogError("EXCEPCION NO CONTROLADA: " + e.StackTrace);
                return BadRequest("Ha ocurrido un error no esperado.");
            }
        }

        [HttpPost("updateTag")]
        public ActionResult updateTag([FromBody] TagFormUpdate tagForm)
        {
            try
            {
                var userId = this.tokenRecovery.RecoveryId(this.tokenRecovery.RecoveryToken(HttpContext));

                var response = new
                {
                    Metadata = new { },
                    Data = photoService.UpdateTag(tagForm, userId)
                };
                return Ok(response);
            }
            catch (Exceptions.NotFoundException)
            {
                return BadRequest("No se ha encontrado la foto que se desea actualizar.");

            }
            catch (System.Exception e)
            {
                logger.LogError("EXCEPCION NO CONTROLADA: " + e.StackTrace);
                return BadRequest("Ha ocurrido un error no esperado.");
            }
        }

        [HttpPost("removeTag")]
        public ActionResult removeTag([FromBody] TagFormDelete tagForm)
        {
            try
            {
                var userId = this.tokenRecovery.RecoveryId(this.tokenRecovery.RecoveryToken(HttpContext));

                var response = new
                {
                    Metadata = new { },
                    Data = photoService.RemoveTag(tagForm, userId)
                };
                return Ok(response);
            }
            catch (Exceptions.NotFoundException e)
            {
                return BadRequest(e.Message);

            }
            catch (System.Exception e)
            {
                logger.LogError("EXCEPCION NO CONTROLADA: " + e.StackTrace);
                return BadRequest("Ha ocurrido un error no esperado.");
            }
        }

        [HttpGet("imageFile/{fileId}")]
        public IActionResult BannerImage(int fileId)
        {

            if (fileId <= 0)
            {
                var file = System.IO.Path.Combine(appSettings.BasePathFolder, "defaultPhoto.jpg");

                return PhysicalFile(file, "image/jpeg");
            }

            try
            {
                var userId = this.tokenRecovery.RecoveryId(this.tokenRecovery.RecoveryToken(HttpContext));
                var photo = photoService.GetPhoto(fileId, userId);

                if (photo == null)
                {
                    var file_ = System.IO.Path.Combine(appSettings.BasePathFolder, "defaultPhoto.jpg");

                    return PhysicalFile(file_, "image/jpeg");
                    
                }

                var file = System.IO.Path.Combine(appSettings.BasePathFolder, photo.Url);

                return PhysicalFile(file, "image/jpeg");
            }
            catch (Exception e)
            {
                var file = System.IO.Path.Combine(appSettings.BasePathFolder, "defaultPhoto.jpg");

                return PhysicalFile(file, "image/jpeg");
            }


        }



        /*[HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, [FromBody]UpdatePhotoForm form)
        {
            var photo = photoService.Get(id);

            if (photo == null)
            {
                return NotFound();
            }

            photoService.Update(id, form);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id, [FromBody]DeletePhotoForm form)
        {
            var photo = photoService.Get(id);

            if (photo == null)
            {
                return NotFound();
            }

            photoService.Remove(form);

            return NoContent();
        }*/
    }
}