using System;
using System.Linq;
using System.Collections.Generic;
using LHDTV.Models.DbEntity;
namespace LHDTV.Repo
{
    public class PhotoRepo : IPhotoRepo
    {

        private List<PhotoDb> fakeRepo;
        public PhotoRepo(Fakes.Fakes _fake)
        {
            fakeRepo = _fake.photos;
        }

        public PhotoDb getPhoto(string id)
        {
            var photo = fakeRepo.Where(p => p.Id == id && !p.Deleted ).SingleOrDefault();

            return photo;
        }

        public PhotoDb Create(PhotoDb entity)
        {
            entity.Id = fakeRepo.Count + 1 + "";
            fakeRepo.Add(entity);

            return entity;
        }
        public PhotoDb Read(string id)
        {
            throw new NotImplementedException();
        }
        public PhotoDb Update(PhotoDb entity)
        {
            throw new NotImplementedException();
        }
        public PhotoDb Delete(string id)
        {
            fakeRepo.First(p => p.Id == id).Deleted = true;
            
            return fakeRepo.First(p => p.Id == id);
        }

        public List<PhotoDb> GetAll()
        {
            return fakeRepo.Where(photo => !photo.Deleted ).ToList();
        }

    }
}