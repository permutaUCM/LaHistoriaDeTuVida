using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using LHDTV.Entities;
using LHDTV.Helpers;
using AutoMapper;
using LHDTV.Models.ViewEntity;
using LHDTV.Models.Forms;
using LHDTV.Models.DbEntity;
using LHDTV.Repo;

namespace LHDTV.Service
{
    public class UserService : IUserService
    {

        private readonly IUserRepo userRepo;

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

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public UserView Create (AddUserForm user){

            // si existe un usuario con el nick, hay error.
            //var nick =  userRepo.Read(user.id);

         //   if(nick != null){
                
          //      return null;

         //   }

            // cifrar la contraseña

         //   user.Password = Cifrar.(password);

         //   var user_aux = userRepo.create(new Userdb (){


        //        nombre,contra....



         //   });

            
         //   var userMap = mapper.Map<UserView>(user_aux);

            return null;


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
            var app_secret = _appSettings.Secret;
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                // 7 variable de configuracion --
                Expires = DateTime.UtcNow.AddDays(7),
                // cambiar por el 512
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
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