using System;
using System.Linq;
using System.Collections.Generic;
using LHDTV.Models.DbEntity;
using Microsoft.EntityFrameworkCore;
namespace LHDTV.Repo
{
    public class PhotoRepoDb : IPhotoRepo
    {

        private List<PhotoDb> fakeRepo;
        public PhotoRepoDb(Fakes.Fakes _fake)
        {
            fakeRepo = _fake.photos;
        }

        public PhotoDb Create(PhotoDb entity)
        {
            using (var ctx = new LHDTVContext())
            {
                ctx.Photo.Add(entity);
                ctx.SaveChanges();
                return entity;
            }


        }
        public PhotoDb Read(int id)
        {
            using (var ctx = new LHDTVContext())
            {
                var photo = ctx.Photo.Include(p => p.Tag).ThenInclude(t => t.Properties).FirstOrDefault(p => p.Id == id);
                return photo;
            }
        }
        public PhotoDb Update(PhotoDb entity)
        {
            using (var ctx = new LHDTVContext())
            {
                ctx.Photo.Update(entity);
                ctx.SaveChanges();

                return entity;
            }
        }

        //Si borra correctamente devuelve un null
        public PhotoDb Delete(int id)
        {
            using (var ctx = new LHDTVContext())
            {
                var photo = ctx.Remove(id);
                ctx.SaveChanges();

                return null;
            }
        }

        public List<PhotoDb> GetAll()
        {
            using (var ctx = new LHDTVContext())
            {
                return ctx.Photo.Include(p => p.Tag).Where(photo => !photo.Deleted).ToList();
            }
        }

        
    }
}