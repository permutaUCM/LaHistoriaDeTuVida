using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel.DataAnnotations.Schema;
using LHDTV.Models.ViewEntity;
using LHDTV.Models.DbEntity;
using LHDTV.Repo;
using LHDTV.Models.Forms;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;



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

        public PhotoView GetPhoto(int id, int userId)
        {  //Repasar?¿?¿
            var photo = photoRepo.Read(id, userId);
            var photoret = mapper.Map<PhotoView>(photo);
            return photoret;
        }



        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ClientGuid { get; set; }

        //creamos una nueva foto y la devolvemos?¿ para tratarla?¿
        public PhotoView Create(AddPhotoForm photo, int userId)
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

            var photoRet = photoRepo.Create(photoPOJO, userId);
            var photoTemp = mapper.Map<PhotoView>(photoRet);

            return photoTemp;
        }
        public PhotoView Update(UpdatePhotoForm photo, int userId)
        {


            var c = photoRepo.Read(photo.PhotoId,   userId);
            if (c == null)
            {
                return null;
            }

            c.Title = photo.Title.Trim();
            c.Caption = photo.Caption.Trim();

            var photoRet = photoRepo.Update(c,   userId);
            var photoTemp = mapper.Map<PhotoView>(photoRet);

            return photoTemp;

        }

        //Borrado lógico
        public PhotoView Delete(int photoId, int userId)
        {
            var photo = photoRepo.Read(photoId,   userId);

            if (photo == null)
            {
                return null;
            }

            photo.Deleted = true;

            var photoRet = photoRepo.Update(photo,   userId);
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

        public List<PhotoView> GetAll(Pagination pagination,int userId){
            var photos = photoRepo.GetAll(pagination, userId); 
            if(photos == null){
                return new List<PhotoView>();
            }
            return photos.Select(p => this.mapper.Map<PhotoView>(p)).ToList();
        }



        public PhotoView AddTag(TagForm form, int userId)
        {
            var photo = photoRepo.Read(form.PhotoId,   userId);

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

            var ret = photoRepo.Update(photo,   userId);

            return mapper.Map<PhotoView>(ret);

        }

        public PhotoView UpdateTag(TagFormUpdate form, int userId)
        {
            var photo = photoRepo.Read(form.PhotoId,   userId);


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

            photoRepo.UpdateTag(tag,   userId);

            return mapper.Map<PhotoView>(photo);

        }

        public PhotoView RemoveTag(TagFormDelete form, int userId)
        {
            var photo = photoRepo.Read(form.PhotoId,   userId);

            if (photo == null)
            {
                throw new Exceptions.NotFoundException("No se ha encontrado la fotografía solicitada");
            }

            var tag = photo.Tag.Where(t => t.Id == form.TagId).SingleOrDefault();

            if (tag == null)
            {
                throw new Exceptions.NotFoundException("El tag que desea eliminar no está asociado a la fotografía encontrada");
            }

            photoRepo.RemoveTag(tag,   userId);
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