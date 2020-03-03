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
               var folder = ctx.Folder.Include(f => f.Photos).FirstOrDefault(f => f.Id == id);
                return folder;
           }

       }
        // update (datos generales)
       public FolderDb Update(FolderDb entity)
       {

           using(var ctx = new LHDTVContext())
           {
               ctx.Folder.Update(entity);
               ctx.SaveChanges();

                return entity;
           }

       }

       // recibe una lista de fotos la mete en la carpeta, funcion pendiente
            //Falta añadir una photo a una folder

        public FolderDb AddphotoToFolder (FolderDb EntFolder ,PhotoDb EntPhoto)
        {

               using(var ctx= new LHDTVContext())
            {
                ctx.Folder.Add(EntFolder,EntPhoto);
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

       //eliminar una foto de una carpeta (dado una id de carpeta y una id de photo)


        // repasar
       public List<FolderDb> GetAll()
       {
           using (var ctx = new LHDTVContext())
           {
               var res = ctx.Folder.Include(f => f.DefaultPhoto).ToList();
               return res;
           }

       }

       //Mirar el actualizar photo por defecto

   }





}