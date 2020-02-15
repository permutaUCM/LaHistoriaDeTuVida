using System;
using LHDTV.Models.ViewEntity;
using LHDTV.Models.DbEntity;
using LHDTV.Repo;
using LHDTV.Models.Forms;

using System.Linq;

using System.Collections.Generic;

using AutoMapper;


using Microsoft.AspNetCore.Http;
namespace LHDTV.Service
{

    public class PhotoService : IPhotoService{

        private readonly IPhotoRepo photoRepo;

        private readonly IMapper mapper;
        public PhotoService(IPhotoRepo _photoRepo, IMapper _mapper){
            photoRepo = _photoRepo;
            mapper = _mapper;
        }
        
        public PhotoView GetPhoto(int id){  //Repasar?¿?¿
            var photo = photoRepo.Read(id);
            var photoret = mapper.Map<PhotoView>(photo); 
            return photoret;
        }

        //creamos una nueva foto y la devolvemos?¿ para tratarla?¿
        public PhotoView Create(AddPhotoForm photo){

            string path;
            if(!uploadFile(photo.File, out path)){
                return null;
            }

            PhotoDb photoPOJO = new PhotoDb(){
                Url = path,
                UploadDate = DateTime.UtcNow,
                Deleted = false,
                Tittle = photo.Tittle
            };

            var photoRet = photoRepo.Create(photoPOJO);
            var photoTemp = mapper.Map<PhotoView>(photoRet);

            return photoTemp;
        }
        public PhotoView Update(UpdatePhotoForm photo){
           
           
            var c = photoRepo.Read(photo.id);
            if(c == null){
                    return null;
            }

            c.Tittle = photo.Tittle;
            

            var photoRet = photoRepo.Update(c);
            var photoTemp = mapper.Map<PhotoView>(photoRet);

            return photoTemp;

        }

        //Borrado lógico
        public PhotoView Delete(int photoId){
            var photo = photoRepo.Read(photoId);

            if(photo == null){
                return null;
            }

            photo.Deleted=true;

            var photoRet = photoRepo.Update(photo);
            var photoMap = mapper.Map<PhotoView>(photoRet);

            return photoMap;


        }

        public List<PhotoView> GetAll(){
            var listPhotos = photoRepo.GetAll();
            if(listPhotos == null){
                return new List<PhotoView>();
            }
            var listPhotosView = listPhotos.Select( p => mapper.Map<PhotoView>(p)).ToList();
            return listPhotosView;
        }

        private bool uploadFile(IFormFile file, out string route){

            route = "estoesunaurl"; 
            return true;
        }


    }
}