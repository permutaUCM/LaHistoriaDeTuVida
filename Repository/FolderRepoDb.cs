using System;
using System.Linq;
using System.Collections.Generic;
using LHDTV.Models.DbEntity;
using Microsoft.EntityFrameworkCore;



namespace LHDTV.Repo
{

   public class FolderRepoDb : IFolderRepo
   {
       private List<FolderDb> fakerepo;

       public FolderRepoDb(Fakes.Fakes _fake)
       {
            fakerepo=_fake.folders;


       }

       public FolderDb Create (FolderDb entity)
       {
            using(var ctx= new LHDTVContext())
            {
                ctx.Folder.Add(entity);
                ctx.SaveChanges();
                return entity;
            }

       }

       public FolderDb Read (int id)
       {
           using (var ctx = new LHDTVContext())
           {
               var folder = ctx.Folder.Include(p => p.Tag).ThenInclude(t => t.Properties).FirstOrDefault(p => p.Id == id);
                return folder;
           }

       }

       public FolderDb Update(FolderDb entity)
       {

           using(var ctx = new LHDTVContext())
           {
               ctx.Folder.Update(entity);
               ctx.SaveChanges();

                return entity;
           }

       }

       public FolderDb Delete(int id)
       {
           using (var ctx = new LHDTVContext())
           {
               var folder = ctx.Remove(id);
                ctx.SaveChanges();

                return null;
           }


       }

        // repasar
       public List<FolderDb> GetAll()
       {
           using (var ctx = new LHDTVContext())
           {
               return ctx.Folder.Include(p => p.Tag).Where(folder => !folder.Deleted).ToList();
           }

       }

   }





}