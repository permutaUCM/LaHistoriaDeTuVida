
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
    [Route("api/admin")]
    [Authorize(Roles = "ADMIN")]

    public class AdminController : ControllerBase
    {

        private readonly PhotoService _photoService;
        public AdminController(PhotoService photoService){
            _photoService = photoService;
        }

        [HttpPost("tagMaster")]
        public IActionResult getTagMaster(Pagination pag)
        {
            var tags = _photoService.GetTagTypes();

            tags = tags.Skip((pag.NumPag - 1) * pag.TamPag).Take(pag.TamPag).ToList();
            
            return Ok(tags);
        }

    }
}