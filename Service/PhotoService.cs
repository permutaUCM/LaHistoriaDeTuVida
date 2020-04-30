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
using System.ComponentModel.DataAnnotations.Schema;



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



        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ClientGuid { get; set; }

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
                Url = path.Trim(),
                UploadDate = DateTime.UtcNow,
                Deleted = false,
                Title = photo.Title.Trim(),
                Size = file.Length,

                Caption = photo.Caption.Trim(),
                Tag = photo.Tags.Select(tg => new TagDb()
                {
                    Type = tg.Type,
                    Title = tg.Title
                }).ToList()
            };




            var photoRet = photoRepo.Create(photoPOJO);
            var id = photoRet.Id;

 

            var photoTemp = mapper.Map<PhotoView>(photoRet);

            return photoTemp;
        }
        public PhotoView Update(UpdatePhotoForm photo)
        {


            var c = photoRepo.Read(photo.PhotoId);
            if (c == null)
            {
                return null;
            }

            c.Title = photo.Title.Trim();
            c.Caption = photo.Caption.Trim();

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

        public ICollection<PhotoTagsTypesView> GetTagTypes()
        {
            return this.photoRepo.getTagTypes().Select(t => mapper.Map<PhotoTagsTypesView>(t)).ToList();
        }


        // public List<PhotoView> GetAll()
        // {
        //     var listPhotos = photoRepo.GetAll();
        //     if (listPhotos == null)
        //     {
        //         return new List<PhotoView>();
        //     }
        //     var listPhotosView = listPhotos.Select(p => mapper.Map<PhotoView>(p)).ToList();
        //     return listPhotosView;
        // }

        public List<PhotoView> GetAll(Pagination pagination, int userId)
        {
            var photos = photoRepo.GetAll(pagination, userId);
            return photos.Select(p => this.mapper.Map<PhotoView>(p)).ToList();
        }



        public PhotoView AddTag(TagForm form)
        {
            var photo = photoRepo.Read(form.PhotoId);

            if (photo == null)
            {
                throw new Exceptions.NotFoundException("No se ha encontrado la fotografía solicitada");
            }

            photo.Tag.Add(new TagDb()
            {
                Title = form.Title,
                Type = form.Type,
                Extra1 = form.Extra1,
                Extra2 = form.Extra2,
                Extra3 = form.Extra3,
            });

            var ret = photoRepo.Update(photo);

            return mapper.Map<PhotoView>(ret);

        }

        public PhotoView UpdateTag(TagFormUpdate form)
        {
            var photo = photoRepo.Read(form.PhotoId);
            

            if (photo == null)
            {
                throw new Exceptions.NotFoundException("No se ha encontrado la fotografía solicitada");
            }


            var tag = photo.Tag.Where(t => t.Id == form.TagId).SingleOrDefault();

            if (tag == null)
            {
                throw new Exceptions.NotFoundException("El tag que desea eliminar no está asociado a la fotografía encontrada");
            }

            tag.Title = form.Title;
            tag.Extra1 = form.Extra1;
            tag.Extra2 = form.Extra2;
            tag.Extra3 = form.Extra3;
            tag.Type = form.Type;

            photoRepo.UpdateTag(tag);

            return mapper.Map<PhotoView>(photo);

        }

        public PhotoView RemoveTag(TagFormDelete form)
        {
            var photo = photoRepo.Read(form.PhotoId);

            if (photo == null)
            {
                throw new Exceptions.NotFoundException("No se ha encontrado la fotografía solicitada");
            }

            var tag = photo.Tag.Where(t => t.Id == form.TagId).SingleOrDefault();

            if (tag == null)
            {
                throw new Exceptions.NotFoundException("El tag que desea eliminar no está asociado a la fotografía encontrada");
            }

            photoRepo.RemoveTag(tag);
            var ok = photo.Tag.Remove(tag);

            return mapper.Map<PhotoView>(photo);

        }

        private bool uploadFile(IFormFile file, out string route)
        {


            long size = file.Length;

            if (size < 0)
            {
                route = "";
                return false;
            }

            var guid = System.Guid.NewGuid();           

            var extension = file.FileName.Split(".");

            var fileName = Path.Combine(guid + "." + extension);

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