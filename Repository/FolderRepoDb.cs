
using System.Linq;
using System.Collections.Generic;
using LHDTV.Models.DbEntity;
using LHDTV.Models.Forms;
using Microsoft.EntityFrameworkCore;



namespace LHDTV.Repo
{


    public class FolderRepoDb : IFolderRepo
    {
        private List<FolderDb> fakerepo;

        public FolderRepoDb(Fakes.Fakes _fake)
        {
            fakerepo = _fake.folders;


        }

        public FolderDb Create(FolderDb entity)
        {
            using (var ctx = new LHDTVContext())
            {
                ctx.Folder.Add(entity);
                ctx.SaveChanges();
                return entity;
            }

        }


        public FolderDb Read(int id)
        {
            using (var ctx = new LHDTVContext())
            {
                var folder = ctx.Folder.Include(f => f.PhotosTags).Include(f => f.Photos).ThenInclude(p => p.Tag).FirstOrDefault(f => f.Id == id);
                return folder;
            }

        }

        public FolderDb Read(int id,Pagination p)
        {
            using (var ctx = new LHDTVContext())
            {

                var photos = ctx.Photo.Include(p => p.Tag).Where(photo => !photo.Deleted).
                            Skip((p.Page - 1) * p.TamPag).Take(p.TamPag).ToList();
                var folder = ctx.Folder.Include(f => f.PhotosTags).Include(f => f.Photos).ThenInclude(p => p.Tag).FirstOrDefault(f => f.Id == id);
                folder.Photos=photos;
                return folder;
            }

        }
        // update (datos generales)
        public FolderDb Update(FolderDb entity)
        {

            using (var ctx = new LHDTVContext())
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
                // control de errores??
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
                var res = ctx.Folder.Include(f => f.DefaultPhoto).ToList();
                return res;
            }

        }
        // recibe una lista de fotos la mete en la carpeta, funcion pendiente

        public FolderDb AddPhotoToFolder(FolderDb EntFolder, PhotoDb photo)
        {


            using (var ctx = new LHDTVContext())
            {
                EntFolder.Photos.Add(photo);
                ctx.Folder.Update(EntFolder);
                ctx.SaveChanges();
                return EntFolder;
            }

        }


        //eliminar una foto de una carpeta (dado una id de carpeta y una id de photo)

        public FolderDb deletePhotoToFolder(int Id, PhotoDb p)
        {

            using (var ctx = new LHDTVContext())
            {

                var photo = ctx.Folder.Include(f => f.Photos).FirstOrDefault(f => f.Id == Id);
                ctx.Folder.Remove(photo);
                ctx.SaveChanges();
                return null;
            }

        }



        //Mirar el actualizar photo por defecto

        public FolderDb updateDefaultPhotoToFolder(int Id, PhotoDb p)
        {

            using (var ctx = new LHDTVContext())
            {

                var photo = ctx.Folder.Include(f => f.DefaultPhoto).FirstOrDefault(f => f.Id == Id);

                photo.DefaultPhoto = p;
                ctx.Folder.Update(photo);
                ctx.SaveChanges();
                return null;


            }



        }

        public bool ExistsPhoto(int folderId, int photoId)
        {

            using (var ctx = new LHDTVContext())
            {

                return ctx.Folder.Include(f => f.Photos).Where(f => f.Id == folderId).Where(f => f.Photos.Select(p => p.Id).ToList().Contains(photoId)).SingleOrDefault() == null;
            }

        }


    }





}