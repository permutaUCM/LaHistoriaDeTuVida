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
                var photo = ctx.Photo.Include(p => p).ThenInclude(t => t).Include(p => p.Tag).FirstOrDefault(p => p.Id == id);
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

        public void RemoveTag(TagDb tag)
        {
            using (var ctx = new LHDTVContext())
            {
                ctx.TagDb.Remove(tag);
                ctx.SaveChanges();
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
        
        public List<PhotoDb> GetAll(LHDTV.Models.Forms.Pagination pagination, int userId)
        {
            using (var ctx = new LHDTVContext())
            {
                var res = ctx.Photo.Where(p => p.Id == userId)
                .Include(p => p)
                .Skip((pagination.Page - 1) * pagination.TamPag)
                .Take(pagination.TamPag)
                .ToList();

                return res;
            }
        }

        public ICollection<PhotoTagsTypes> getTagTypes()
        {
            using (var ctx = new LHDTVContext())
            {
                return ctx.TagTypeMaster.Include(t => t.Extra1).Include(t => t.Extra2).Include(t => t.Extra3).ToList();
            }
        }
        public void UpdateTag(TagDb tag)
        {
            using (var ctx = new LHDTVContext())
            {
                ctx.TagDb.Update(tag);
                ctx.SaveChanges();
            }
        }


    }
}