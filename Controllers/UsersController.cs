using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LHDTV.Service;
using LHDTV.Models.DbEntity;
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
    //[Authorize]
    [ApiController]
    [Route("api/user")]
    public class UsersController : ControllerBase
    {
        private IUserService userService;

        private readonly IStringLocalizer<UsersController> localizer;

        private readonly ILogger<UsersController> logger;

        private readonly string basePath;

        private const string BASEPATHCONF = "userRoutes:uploadRoute";



        public UsersController(IUserService _userService, IStringLocalizer<UsersController> _localizer,
                          ILogger<UsersController> _logger, IConfiguration _configuration)
        {
            userService = _userService;
            localizer = _localizer;
            logger = _logger;
            basePath = _configuration.GetValue<string>(BASEPATHCONF);
        }

        [HttpPost("addUser")]
        public ActionResult addUser ([FromForm]AddUserForm form){

            var ret = userService.Create(form);
            return Ok(ret);

        }

        [HttpPost("deleteUser")]
        public ActionResult deleteUser (string dni){

            var ret = userService.Delete(dni);
            return Ok(ret);

        }

        [HttpPost("updateUser")]
        public ActionResult updateUser ([FromForm]UpdateUserForm form){

            var ret = userService.UpdateInfo(form);
            return Ok(ret);

        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromForm]UserDb userParam)
        {
            var user = userService.Authenticate(userParam.Nickname, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        /**
            Update the user entity to be ready for the credential recovery.
        */
        [AllowAnonymous]
        [HttpPost("requestPasswordRecovery")]
        public IActionResult RequestPasswordRecovery([FromBody] RequestPasswordRecoveryForm passwordRecoveryForm){

            var ok = userService.RequestPasswordRecovery( passwordRecoveryForm);

            if(ok){
                return Ok(new {
                    Metadata = new {},
                    Data = new {
                        mesage = localizer["RequestPasswordRecoveryOk"].Value
                    }
                });
            }

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("passwordRecovery")]
        public IActionResult passwordRecovery([FromBody] PasswordRecoveryForm passwordRecoveryForm){
            
            var ok = userService.PasswordRecovery( passwordRecoveryForm);
            if(ok){
                return Ok(new {
                    Metadata = new {},
                    Data = new {
                        message = localizer["PasswordRecoveryOk"].Value
                    }
                });
            }else{
                return BadRequest();
            }
        }



    }
}