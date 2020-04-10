using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using LHDTV.Helpers;
using AutoMapper;
using LHDTV.Models.ViewEntity;
using LHDTV.Models.Forms;
using LHDTV.Models.DbEntity;
using LHDTV.Repo;
using Microsoft.Extensions.Configuration;
using SimpleCrypto;

namespace LHDTV.Service
{
    public class UserService : IUserService
    {

        private readonly IUserRepoDb userRepoDb;

        private readonly IMapper mapper;

        private readonly string basePath;

        private const string BASEPATHCONF = "userRoutes:uploadRoute";

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<UserDb> _users = new List<UserDb>
        { 
            new UserDb { Id = 1, FirstName = "Jose", LastName1 = "Sanchez",LastName2 = "Sanchez",
                     Username = "Xose", Password = "12345",Nickname="Xose", Email="jose@gmail.com",
                        Dni="88888888L"} 
        };

        private readonly AppSettings appSettings;

        public UserService(IOptions<AppSettings> _appSettings,IUserRepoDb _userRepoDb,
                             IMapper _mapper, IConfiguration _configuration)
        {
            appSettings = _appSettings.Value;
            userRepoDb = _userRepoDb;
            mapper = _mapper;
            basePath = _configuration.GetValue<string>(BASEPATHCONF);

        }

        public UserView Create (AddUserForm user){

            // si existe un usuario con el nick, hay error.
            //var user_aux =  userRepoDb.ReadNick(user.Nickname);
         /*   var user_aux =  userRepoDb.ReadNick(user.Nickname);

           if(user_aux.Nickname == user.Nickname){
                
               return null;

            }
            */


            // cifrar la contraseña

            ICryptoService cryptoService = new PBKDF2();

            string passwordEncriptada = cryptoService.Compute(user.Password);

            string salt = cryptoService.GenerateSalt();

         // user.Password = Cifrar.(password);

            UserDb userPOJO = new UserDb()
            {
                
                FirstName=user.FirstName,
                LastName1=user.LastName1,
                LastName2=user.LastName2,
                Password=passwordEncriptada,
                Nickname=user.Nickname,
                Email   =user.Email,
                Dni     =user.Dni,
                Token = salt,
                Deleted =false                   
     


            };

           var userRet = userRepoDb.Create(userPOJO);
           var usertemp = mapper.Map<UserView>(userRet);
            
         

            return usertemp;


        }

        //modicar 
        //eliminar


        public UserView Authenticate(string username, string password)
        {

            //cifrar la contraseña 

            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);
            
            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var app_secret = appSettings.Secret;
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                // 7 variable de configuracion --
                Expires = DateTime.UtcNow.AddDays(7),
                // cambiar por el 512
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            var userMap = mapper.Map<UserView>(user);

            return userMap;
        }

    }
}