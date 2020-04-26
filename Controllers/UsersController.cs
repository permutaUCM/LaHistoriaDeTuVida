using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LHDTV.Service;
using Microsoft.Extensions.Localization;
using LHDTV.Models.Forms;
using LHDTV.Exceptions;
using Microsoft.Extensions.Logging;
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

        private ITokenRecoveryService tokenService;



        public UsersController(IUserService _userService, IStringLocalizer<UsersController> _localizer,
                          ILogger<UsersController> _logger, IConfiguration _configuration 
                                    ,ITokenRecoveryService _tokenService)
        {
            userService = _userService;
            localizer = _localizer;
            logger = _logger;
            basePath = _configuration.GetValue<string>(BASEPATHCONF);
            tokenService = _tokenService;
        }

        [HttpPost("addUser")]
        public ActionResult addUser ([FromBody]AddUserForm form){

            var ret = userService.Create(form);
            return Ok(ret);

        }

        [HttpGet("deleteUser")]
        public ActionResult deleteUser (string dni){

            var ret = userService.Delete(dni);
            return Ok(ret);

        }

        [Authorize]
        [HttpPost("updateUser")]
        public ActionResult updateUser ([FromBody]UpdateUserForm form){

            try{
                               
                var id = tokenService.RecoveryId(tokenService.RecoveryToken(HttpContext));
                var ret = userService.UpdateInfo(form,id);
                
                return Ok(ret);


            }catch(NotFoundException){

                 return BadRequest(new { message = "Usuario no encontrado" });


            }catch(WrongPasswordException pswex){
                
                return BadRequest(new { message = "Password is incorrect" });
                // userService ;
                 

            }

            

        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateForm userParam)
        {
            var user = userService.Authenticate(userParam.NickName, userParam.Password);

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