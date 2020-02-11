using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using LHDTV.Service;
using LHDTV.Models.ViewEntity;
using Microsoft.Extensions.Localization;
using LHDTV.Models.Forms;
using System.Linq;
using LHDTV.Models.Forms;

namespace LHDTV.Controllers{
    [ApiController]
    [Route("api/photo")]
    public class PhotoController: ControllerBase{

        private readonly IPhotoService photoService;
        private readonly IStringLocalizer<PhotoController> localizer;

        public PhotoController(IPhotoService _photoService, IStringLocalizer<PhotoController> _localizer)
        {
            photoService = _photoService;
            localizer = _localizer;
        }
    
        [HttpPost]
        public ActionResult getPhoto([FromBody]PhotoForm form){

            var id = form.id;
            if(id == null || id.Length < 3){
                return BadRequest("El id no puede tener menos de 3 caracteres.");
            }
            var photo = photoService.GetPhoto(id);

            if(photo == null){
                return BadRequest(localizer["photoNotFound"].Value);
            }
            return Ok(photo);
        }


        [HttpPost("addPhoto")]
        public ActionResult addPhoto([FromBody]AddPhotoForm form){ //Preguntar?¿?¿
            var ret = photoService.Create(form);
            return Ok(ret);
        }
        

        [HttpGet("all")]
        public ActionResult getAll(){
            return Ok(photoService.GetAll());
        }

        [HttpGet("delete/{photoId}")]
        public ActionResult delete(string photoId){
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