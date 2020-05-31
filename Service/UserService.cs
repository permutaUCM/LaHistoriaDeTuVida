using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using AutoMapper;
using LHDTV.Models.ViewEntity;
using LHDTV.Models.Forms;
using LHDTV.Models.DbEntity;
using LHDTV.Repo;
using LHDTV.Exceptions;
using LHDTV.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MimeKit;

namespace LHDTV.Service
{
    public class UserService : IUserService
    {

        private readonly IUserRepoDb userRepo;

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

        public UserService(IOptions<AppSettings> _appSettings, IUserRepoDb _userRepo,
                             IMapper _mapper, IConfiguration _configuration)
        {
            appSettings = _appSettings.Value;
            userRepo = _userRepo;
            mapper = _mapper;
            basePath = _configuration.GetValue<string>(BASEPATHCONF);

        }


        public UserView GetUser(int id)
        {  //Repasar?¿?¿
            var user = userRepo.Read(id, 0);
            var userRet = mapper.Map<UserView>(user);
            return userRet;
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

            var passwordEncriptada = encrypt(user.Password, appSettings.PassworSecret);


            // user.Password = Cifrar.(password);

            UserDb userPOJO = new UserDb()
            {

                Name = user.FirstName.Trim(),
                LastName1 = user.LastName1.Trim(),
                LastName2 = user.LastName2.Trim(),
                Password = passwordEncriptada,
                Nickname = user.Nickname.Trim(),
                Email = user.Email.Trim(),
                Dni = user.Dni.Trim(),
                // Token = salt,
                Deleted = false



            };



            var userRet = userRepo.Create(userPOJO, 0);
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


        public UserView UpdateInfo(UpdateUserForm user, int id)
        {



            var user_bbdd = userRepo.Read(id, 0);
            if (user_bbdd == null)
            {

                throw new NotFoundException("Usuario no valido");
            }

            var newPasswordEncrypt = encrypt(user.NewPassword, appSettings.PassworSecret);
            var oldPasswordEncrypt = encrypt(user.OldPassword, appSettings.PassworSecret);

            if (oldPasswordEncrypt != user_bbdd.Password)
            {
                //crear nueva exception
                throw new NotFoundException("Contraseña Invalida");

            }

            user_bbdd.LastName1 = user.LastName1.Trim() ?? user_bbdd.LastName1;
            user_bbdd.LastName2 = user.LastName2.Trim() ?? user_bbdd.LastName2;
            user_bbdd.Name = user.FirstName.Trim() ?? user_bbdd.Name;
            user_bbdd.Email = user.Email.Trim() ?? user_bbdd.Email;
            user_bbdd.Password = user.NewPassword ?? user_bbdd.Password;



            var user_ret = userRepo.Update(user_bbdd, 0);
            var userTemp = mapper.Map<UserView>(user_ret);

            return userTemp;

        }




        public UserView Delete(string dni)
        {

            var user = userRepo.ReadDni(dni, 0);

            if (user == null)
            {

                return null;
            }

            user.Deleted = true;

            var userRet = userRepo.Update(user, 0);
            var userMap = mapper.Map<UserView>(userRet);

            return userMap;
        }

        public UserView Authenticate(string username, string password)
        {

            //cifrar la contraseña 
            password = encrypt(password, appSettings.PassworSecret);

            var user = userRepo.Authenticate(username, password, 0);


            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var app_secret = appSettings.Secret;
            var key = Encoding.UTF8.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
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

        public bool RequestPasswordRecovery(RequestPasswordRecoveryForm passwordRecoveryForm)
        {

            var user = userRepo.ReadNick(passwordRecoveryForm.Nick, 0);
            if (user == null || user.Email != passwordRecoveryForm.Email)
            {
                return false;
            }

            var expDate = DateTime.UtcNow.AddHours(appSettings.TokenLifeSpan);
            var newToken = CreateToken();

            user.RecovertyToken = newToken;
            user.ExpirationTokenDate = expDate;

            userRepo.Update(user, 0);

            //TODO: Send email

            sendMessage(createMessage());

            return true;
        }

        public bool PasswordRecovery(PasswordRecoveryForm passwordRecovery)
        {

            var user = userRepo.ReadNick(passwordRecovery.Nick, 0);
            if (user == null || user.Email != passwordRecovery.Email || user.RecovertyToken != passwordRecovery.Token ||
            user.ExpirationTokenDate < DateTime.UtcNow)
            {
                return false;
            }

            var passwEncrypt = encrypt(passwordRecovery.NewPassword, appSettings.PassworSecret);

            user.Password = passwEncrypt;
            user.RecovertyToken = null;

            userRepo.Update(user, 0);

            return true;
        }

        private string CreateToken()
        {

            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());

        }

        private MimeKit.MimeMessage createMessage()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Auryn", "Auryn.noreply@gmail.com"));
            message.To.Add(new MailboxAddress("Pedro", "pedagova@gmail.com"));
            message.Subject = "Recuperación de contraseña - Auryn";

            message.Body = new TextPart("plain")
            {
                Text = @"Hey Pedro,

                What are you up to this weekend? Monica is throwing one of her parties on
                Saturday and I was hoping you could make it.

                Will you be my +1?

                -- Joey
                "
            };

            return message;
        }

        private bool sendMessage(MimeMessage msg)
        {

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.friends.com", 587, false);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("Auryn.noreply@gmail.com", "1a@11111");

                client.Send(msg);
                client.Disconnect(true);
            }
            return true;
        }



    }
}