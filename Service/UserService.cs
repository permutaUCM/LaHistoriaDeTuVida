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
using System.Security.Cryptography;
using System.IO;

namespace LHDTV.Service
{
    public class UserService : IUserService
    {

        private readonly IUserRepoDb userRepoDb;

        private readonly IMapper mapper;

        private readonly string basePath;

        private const string BASEPATHCONF = "userRoutes:uploadRoute";

        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
      /*  private List<UserDb> _users = new List<UserDb>
        {
            new UserDb { Id = 1, Name = "Jose", LastName1 = "Sanchez",LastName2 = "Sanchez",
                     Password = "12345",Nickname="Xose", Email="jose@gmail.com",
                        Dni="88888888L"}
        };*/

        private readonly AppSettings appSettings;

        public UserService(IOptions<AppSettings> _appSettings, IUserRepoDb _userRepoDb,
                             IMapper _mapper, IConfiguration _configuration)
        {
            appSettings = _appSettings.Value;
            userRepoDb = _userRepoDb;
            mapper = _mapper;
            basePath = _configuration.GetValue<string>(BASEPATHCONF);

        }

        public UserView Create(AddUserForm user)
        {

            // si existe un usuario con el nick, hay error.
            //var user_aux =  userRepoDb.ReadNick(user.Nickname);
            /*   var user_aux =  userRepoDb.ReadNick(user.Nickname);

              if(user_aux.Nickname == user.Nickname){

                  return null;

               }
               */


            // cifrar la contraseña

            var passwordEncriptada = encrypt(user.Password,appSettings.PassworSecret);


            // user.Password = Cifrar.(password);

            UserDb userPOJO = new UserDb()
            {

                Name = user.FirstName,
                LastName1 = user.LastName1,
                LastName2 = user.LastName2,
                Password = passwordEncriptada,
                Nickname = user.Nickname,
                Email = user.Email,
                Dni = user.Dni,
                // Token = salt,
                Deleted = false



            };

            var userRet = userRepoDb.Create(userPOJO);
            var usertemp = mapper.Map<UserView>(userRet);



            return usertemp;


        }


        private string encrypt(string s1, string EncryptionKey)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(s1);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    s1 = Convert.ToBase64String(ms.ToArray());
                }
            }
            return s1;
        }


        /*  public UserView Update (){





          }*/


        //eliminar

        /* public UserView Delete (){





         }*/

        public UserView Authenticate(string username, string password)
        {

            //cifrar la contraseña 
            password = encrypt(password,appSettings.PassworSecret);
            
            var user = userRepoDb.Authenticate(username,password);
           

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
            
           

            // remove password before returning
            user.Password = null;

            var userMap = mapper.Map<UserView>(user);

            var token = tokenHandler.CreateToken(tokenDescriptor);
            userMap.Token = tokenHandler.WriteToken(token);

            return userMap;
        }

    }
}