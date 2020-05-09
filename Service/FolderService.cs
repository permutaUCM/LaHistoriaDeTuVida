
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


namespace LHDTV.Service
{


    public class FolderService : IFolderService
    {

        private readonly IFolderRepo folderRepo;
        private readonly IPhotoRepo photoRepo;

        private readonly IMapper mapper;

        private readonly string basePath;

        private const string BASEPATHCONF = "folderRoutes:uploadRoute";

        public FolderService(IFolderRepo _folderRepo,
                            IMapper _mapper,
                            IConfiguration _configuration,
                            IPhotoRepo _photoRepo)
        {
            folderRepo = _folderRepo;
            mapper = _mapper;
            basePath = _configuration.GetValue<string>(BASEPATHCONF);
            photoRepo = _photoRepo;
        }

        public FolderView GetFolder(int id, int userId)
        {

            var folder = folderRepo.Read(id, userId);
            var folderRet = mapper.Map<FolderView>(folder);

            return folderRet;
        }

        // crea un carpeta vacia
        public FolderView Create(AddFolderForm folder, int userId)
        {

            FolderDb folderPOJO = new FolderDb()
            {

                DefaultPhoto = null,
                Title = folder.Title.Trim(),
                PhotosTags = null,
                Deleted = false


            };

            var folderRet = folderRepo.Create(folderPOJO, userId);
            var folderTemp = mapper.Map<FolderView>(folderRet);

            return folderTemp;

        }


        public FolderView Delete(int folderId, int userId)
        {

            var folder = folderRepo.Read(folderId, userId);
            if (folder == null)
            {
                return null;
            }

            folder.Deleted = true;

            var folderRet = folderRepo.Update(folder, userId);
            var folderMap = mapper.Map<FolderView>(folderRet);

            return folderMap;

        }

        public FolderView Update(UpdateFolderForm folder, int userId)
        {

            var f = folderRepo.Read(folder.id, userId);
            if (f == null) return null;

            f.Title = folder.Title;
            f.Transition = folder.Transition;
            f.AutoStart = folder.AutoStart;
            f.ShowTime = folder.TransitionTime;
            
            var folderRet = folderRepo.Update(f, userId);
            var folderTemp = mapper.Map<FolderView>(folderRet);

            return folderTemp;

        }

        //añade una coleccion de fotos a una carpeta
        // primera version se añade de una en una

        public FolderView addPhotoToFolder(int folderId, PhotoDb photo, int userId)
        {

            var f = folderRepo.Read(folderId, userId);
            if (f == null)
            {
                return null;
            }


            if (f.Photos.Where(p => p.Id == photo.Id).SingleOrDefault() != null) return null;

            // esto valdria para una foto ¿bucle para una lista de fotos?
            f.Photos.Add(photo);

            // intentamos conseguir una lista de tags de photos, que no estan en la lista de tags de la carpeta. 
            var nocontainstags = photo.Tag.Where(t => f.Photos.Where(p => p.Tag.Select(pt => pt.Title).Contains(t.Title)).FirstOrDefault() == null).Select(t => t.Title).ToList();
            //var nocontainstags = photo.Tag.Where(t => f.Photos.Where(p => p.Tag.Select(pt => pt).Contains(t)).FirstOrDefault() == null).ToList();
            foreach (var t in nocontainstags)
                f.PhotosTags.Add(new FileTags()
                {

                    Title = t,


                });


            var folderRet = folderRepo.Update(f, userId);
            var folderTemp = mapper.Map<FolderView>(folderRet);

            return folderTemp;

        }


        public FolderView deletePhotoToFolder(int folderId, PhotoDb photo, int userId)
        {

            var f = folderRepo.Read(folderId, userId);

            if (f == null) return null;


            /*  if(folderRepo.Read(folderId).PhotosTags.ContainsKey(p))
                  folderRepo.Read(folderId).PhotosTags.Remove(p);*/

            if (f.Photos.Where(p => p.Id == photo.Id).SingleOrDefault() == null) return null;

            f.Photos.Remove(photo);
            // Obtener el listado de tags a eliminar
            var tagsaeliminar = photo.Tag.Where(t => f.Photos.Where(p => p.Tag.Select(pt => pt.Title).Contains(t.Title)).FirstOrDefault() == null).Select(t => t.Title).ToList();
            //var tagsaeliminar = photo.Tag.Where(t => f.Photos.Where(p => p.Tag.Select(pt => pt).Contains(t)).FirstOrDefault() == null).ToList();

            foreach (var t in tagsaeliminar)
                f.PhotosTags.Remove(new FileTags()
                {

                    Title = t,

                });

            var folderRet = folderRepo.Update(f, userId);

            var folderTemp = mapper.Map<FolderView>(folderRet);


            return folderTemp;



        }

        // actualizar photo por defecto

        public FolderView updateDefaultPhotoToFolder(int folderId, PhotoDb p, int userId)
        {


            var f = folderRepo.Read(folderId, userId);

            if (f == null) return null;


            /*  if(folderRepo.Read(folderId).PhotosTags.ContainsKey(p))
                  folderRepo.Read(folderId).PhotosTags.Remove(p);*/

            //     if(!folderRepo.Read(folderId).PhotosTags.ContainsKey(p))return null;

            f.DefaultPhoto = p;

            // comprobar que la photo existe en la carpeta, comprobar si la foto esta eliminada,foto no es nula
            // lanzar excepciones en vez de nulos.

            var folderRet = folderRepo.updateDefaultPhotoToFolder(folderId, p, userId);
            var folderTemp = mapper.Map<FolderView>(folderRet);


            return folderTemp;

        }

        public List<FolderView> GetAll(Pagination pagination, int userId)
        {
            var folders = folderRepo.GetAll(pagination, userId);
            return folders.Select(f => this.mapper.Map<FolderView>(f)).ToList();
        }


        public LHDTV.Models.FolderMetadata GetMetadata()
        {

            var metaData = new LHDTV.Models.FolderMetadata();
            var transitionsMetadata = folderRepo.GetTransitionMetadata();

            metaData.Transitions = transitionsMetadata.Select(tm => mapper.Map<PhotoTransitionView>(tm)).ToList();

            var types = folderRepo.GetType();
            metaData.Types = this.photoRepo.getTagTypes().Select(t => mapper.Map<PhotoTagsTypesView>(t)).ToList();
            return metaData;
        }
    }
}