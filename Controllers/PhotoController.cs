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
    [Route("api/photo")]
    public class PhotoController : ControllerBase
    {

        private readonly IPhotoService photoService;
        private readonly IStringLocalizer<PhotoController> localizer;

        private readonly ILogger<PhotoController> logger;

        private readonly string basePath;

        private const string BASEPATHCONF = "photoRoutes:uploadRoute";

        public PhotoController(IPhotoService _photoService, IStringLocalizer<PhotoController> _localizer,
                          ILogger<PhotoController> _logger, IConfiguration _configuration)
        {
            photoService = _photoService;
            localizer = _localizer;
            logger = _logger;
            basePath = _configuration.GetValue<string>(BASEPATHCONF);

        }

        [HttpPost]
        public ActionResult getPhoto([FromBody]PhotoForm form)
        {

            logger.LogInformation("getphoto {@form}", form);

            var id = form.id;

            var photo = photoService.GetPhoto(id);

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
                double dobletemp = new Random().Next(1, 1000000);
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
        public ActionResult UpdatePhoto([FromBody]UpdatePhotoForm form)
        {
            return Ok(photoService.Update(form));
        }

        [HttpPost("addPhoto")]
        public ActionResult addPhoto([FromForm]AddPhotoForm form){ 

            form.Tags = new List<TagForm>(){
                new TagForm() {
                    Title = "T123", 
                    Type = "RESTAURANT",
                    Properties = new List<KeyValuePair<string, string>>(){
                        KeyValuePair.Create("p1", "pv1"),
                        KeyValuePair.Create("p2", "pv2")
                    }
                }
            };

            var ret = photoService.Create(form);
            return Ok(ret);
        }


        [HttpGet("all")]
        public ActionResult getAll()
        {
            return Ok(photoService.GetAll());
        }

        [HttpGet("delete/{photoId}")]
        public ActionResult delete(int photoId){
            return Ok(photoService.Delete(photoId));
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