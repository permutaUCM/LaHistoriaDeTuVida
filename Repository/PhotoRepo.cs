using System;
using System.Linq;
using System.Collections.Generic;
using LHDTV.Models.DbEntity;
namespace LHDTV.Repo
{
    public class PhotoRepo : IPhotoRepo
    {

        private List<PhotoDb> fakeRepo = new List<PhotoDb>(){
            new PhotoDb(){
                Id = "Photo1",
                UploadDate = DateTime.Now,
                Url = "https://photo1.jpg"
            },
            new PhotoDb(){
                Id = "Photo2",
                UploadDate = DateTime.Now,
                Url = "https://photo2.jpg"
            }
        };

        public PhotoDb getPhoto(string id){
            var photo = fakeRepo.Where(p => p.Id == id).SingleOrDefault();

            return photo;
        }
    }
}