using System;
using LHDTV.Models.ViewEntity;
using LHDTV.Models.DbEntity;
using LHDTV.Repo;
using LHDTV.Models.Forms;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;

using ExifLib;

namespace LHDTV.Service
{

    public class PhotoService : IPhotoService
    {

        private readonly IPhotoRepo photoRepo;
        private readonly IMapper mapper;
        private readonly string basePath;
        private const string BASEPATHCONF = "photoRoutes:uploadRoute";

        public PhotoService(IPhotoRepo _photoRepo, IMapper _mapper, IConfiguration _configuration)
        {
            photoRepo = _photoRepo;
            mapper = _mapper;
            basePath = _configuration.GetValue<string>(BASEPATHCONF);

        }

        public PhotoView GetPhoto(int id)
        {  //Repasar?¿?¿
            var photo = photoRepo.Read(id);
            var photoret = mapper.Map<PhotoView>(photo);
            return photoret;
        }

        //creamos una nueva foto y la devolvemos?¿ para tratarla?¿
        public PhotoView Create(AddPhotoForm photo)
        {

            var file = photo.File;
            string path;
            if (!uploadFile(photo.File, out path))
            {
                return null;
            }

            PhotoDb photoPOJO = new PhotoDb()
            {
                Url = path,
                UploadDate = DateTime.UtcNow,
                Deleted = false,
                Title = photo.Title,
                Size = file.Length,
                caption = photo.Caption,
                Tag = photo.Tags.Select(tg => new TagDb()
                {
                    Type = tg.Type,
                    Title = tg.Title,
                    Properties = tg.Properties.Select(tgprop =>
                        new TagPropDb()
                        {
                            propKey = tgprop.Key,
                            propVal = tgprop.Value

                        }).ToList()
                }).ToList()
            };

            var photoRet = photoRepo.Create(photoPOJO);
            var photoTemp = mapper.Map<PhotoView>(photoRet);

            return photoTemp;
        }
        public PhotoView Update(UpdatePhotoForm photo)
        {


            var c = photoRepo.Read(photo.id);
            if (c == null)
            {
                return null;
            }

            c.Title = photo.Title;


            var photoRet = photoRepo.Update(c);
            var photoTemp = mapper.Map<PhotoView>(photoRet);

            return photoTemp;

        }

        //Borrado lógico
        public PhotoView Delete(int photoId)
        {
            var photo = photoRepo.Read(photoId);

            if (photo == null)
            {
                return null;
            }

            photo.Deleted = true;

            var photoRet = photoRepo.Update(photo);
            var photoMap = mapper.Map<PhotoView>(photoRet);

            return photoMap;


        }

        public List<PhotoView> GetAll()
        {
            var listPhotos = photoRepo.GetAll();
            if (listPhotos == null)
            {
                return new List<PhotoView>();
            }
            var listPhotosView = listPhotos.Select(p => mapper.Map<PhotoView>(p)).ToList();
            return listPhotosView;
        }

        private bool uploadFile(IFormFile file, out string route)
        {


            long size = file.Length;

            if (size < 0)
            {
                route = "";
                return false;
            }

            double dobletemp = new Random().Next(1, 1000000);
            var extension = file.FileName.Split(".");

            var fileName = DateTime.UtcNow.Ticks + "_" + Math.Round(dobletemp) + "." + extension[extension.Length - 1];

            var filePath = Path.Combine(basePath, fileName);

            if (!Directory.Exists(basePath))
            {
                route = "";
                return false;
            }
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
                route = fileName;
                return true;
            }


        }


    }
}