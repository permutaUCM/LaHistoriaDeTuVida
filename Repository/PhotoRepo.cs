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

        public PhotoDb getPhoto(int id)
        {
            var photo = fakeRepo.Where(p => p.Id == id && !p.Deleted).SingleOrDefault();

            return photo;
        }

        public PhotoDb Create(PhotoDb entity)
        {
            entity.Id = fakeRepo.Count + 1;
            fakeRepo.Add(entity);

            return entity;
        }
        public PhotoDb Read(int id)
        {
            throw new NotImplementedException();
        }

        //Actualiza un elemento
        public PhotoDb Update(PhotoDb entity)
        {

            fakeRepo.First(p => p.Id == entity.Id).Tittle = entity.Tittle;

            return entity;

        }
        //Borrado fisico
        public PhotoDb Delete(int id)
        {
            return null;
            /*if(fakeRepo.Remove(p => p.Id == id)) return p;
            else return null;*/


        }

        public List<PhotoDb> GetAll()
        {
            return fakeRepo.Where(photo => !photo.Deleted).ToList();
        }

    }
}