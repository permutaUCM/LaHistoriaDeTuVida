
using System;
using System.Linq;
using System.Collections.Generic;
using LHDTV.Models.DbEntity;
using Microsoft.EntityFrameworkCore;


namespace LHDTV.Repo
{

   public class UserRepoDb : IUserRepoDb
   {

       public UserRepoDb()
       {

       }

       public UserDb Create (UserDb entity, int userId)
       {
            using(var ctx= new LHDTVContext())
            {
                ctx.User.Add(entity);
                ctx.SaveChanges();
                return entity;
            }

       }


       public UserDb Read (int id, int userId)
       {
           using (var ctx = new LHDTVContext())
           {
               var user = ctx.User.Include(u => u).ThenInclude(v => v).FirstOrDefault(u => u.Id == id);

               return user;
           }

       }

       // Check nickname
       public UserDb ReadNick (string nick, int userId)       
       {
           using (var ctx = new LHDTVContext())
           {
               var n = ctx.User.Where(u => u.Nickname == nick).SingleOrDefault();
               //var n = ctx.User.Include(u => u).ThenInclude(v => v).FirstOrDefault(u => u.Id == id);

               return n;
           }

       }

              // Check nickname
       public UserDb ReadDni (string dni, int userId)       
       {
           using (var ctx = new LHDTVContext())
           {
               var n = ctx.User.Where(u => u.Dni == dni).SingleOrDefault();
               //var n = ctx.User.Include(u => u).ThenInclude(v => v).FirstOrDefault(u => u.Id == id);

               return n;
           }

       }

       public UserDb Authenticate (string nick,string password, int userId)       
       {
           using (var ctx = new LHDTVContext())
           {
               var n = ctx.User.Where(u => u.Nickname == nick && u.Password == password).SingleOrDefault();
               //var n = ctx.User.Include(u => u).ThenInclude(v => v).FirstOrDefault(u => u.Id == id);

               return n;
           }

       }
        // update (datos generales)
       public UserDb Update(UserDb entity, int userId)
       {

           using(var ctx = new LHDTVContext())
           {
               ctx.User.Update(entity);
               ctx.SaveChanges();

                return entity;
           }

       }

       
       public UserDb Delete(int id, int userId)
       {
           using (var ctx = new LHDTVContext())
           {
               // control de errores??
               var user = ctx.Remove(id);
                ctx.SaveChanges();

                return null;
           }


       }

           public List<UserDb> GetAll(LHDTV.Models.Forms.Pagination pagination, int userId)
        {
            using (var ctx = new LHDTVContext())
            {
                return ctx.User.Include(u => u).Where(user => !user.Deleted).ToList();
            }
        }


   }
}