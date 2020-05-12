
using System;
using System.Linq;
using LHDTV.Models.ViewEntity;
using LHDTV.Models.DbEntity;
using LHDTV.Repo;
using LHDTV.Models.Forms;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;


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
            photoRepo = _photoRepo;
            mapper = _mapper;
            basePath = _configuration.GetValue<string>(BASEPATHCONF);
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

        public FolderView addPhotoToFolder(int folderId, int photoId, int userId)
        {

            try
            {
                var f = folderRepo.Read(folderId, userId);
                if (f == null)
                {
                    return null;
                }

                var p = photoRepo.Read(photoId, userId);

                //var aux = folderRepo.Read(f.Id).Photos;
                if (!folderRepo.ExistsPhoto(folderId, photoId)) return null;


                // esto valdria para una foto ¿bucle para una lista de fotos?
                folderRepo.AddPhotoToFolder(f, p, userId);


                // intentamos conseguir una lista de tags de photos, que no estan en la lista de tags de la carpeta. 
                var nocontainstags = p.Tag.Where(t => f.PhotosFolder.Where(p => p.Photo.Tag.Select(pt => pt.Title).Contains(t.Title)).FirstOrDefault() == null).Select(t => t.Title).ToList();
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
            catch (Exception)
            {

                photoRepo.Delete(photoId, userId);
                return null;
            }


        }


        public FolderView deletePhotoToFolder(int folderId, List<int> photosId, int userId)
        {

            var f = folderRepo.Read(folderId, userId);

            if (f == null) return null;


            /*  if(folderRepo.Read(folderId).PhotosTags.ContainsKey(p))
                  folderRepo.Read(folderId).PhotosTags.Remove(p);*/
            var photosToRemove = new List<PhotoDb>();
            foreach (var photoId in photosId)
            {
                var photo = f.PhotosFolder.Where(p => p.PhotoId == photoId).SingleOrDefault().Photo;
                if (photo == null) return null;

                photosToRemove.Add(photo);

                var tagsaeliminar = photo.Tag.Where(t => f.PhotosFolder.Where(p => p.Photo.Tag.Select(pt => pt.Title).Contains(t.Title)).FirstOrDefault() == null).Select(t => t.Title).ToList();
                foreach (var t in tagsaeliminar)
                    f.PhotosTags.Remove(new FileTags()
                    {

                        Title = t,

                    });
            }

            // Obtener el listado de tags a eliminar
            //var tagsaeliminar = photo.Tag.Where(t => f.Photos.Where(p => p.Tag.Select(pt => pt).Contains(t)).FirstOrDefault() == null).ToList();
            
            //Si se ha eliminado la foto por defecto, establecer como foto por defecto a la 1º foto no eliminada
            if(photosToRemove.Contains(f.DefaultPhoto)){

                var firstNonDeletedPhoto = f.PhotosFolder.FirstOrDefault(pf => !photosToRemove.Contains(pf.Photo));

                if(firstNonDeletedPhoto != null)
                    f.DefaultPhoto = firstNonDeletedPhoto.Photo;
                else
                    f.DefaultPhoto = null;
            }
            folderRepo.deletePhotosToFolder(folderId, photosToRemove, userId);
            var folderRet = folderRepo.Update(f, userId);

            var folderTemp = mapper.Map<FolderView>(folderRet);


            return folderTemp;
        }

        // actualizar photo por defecto

        public FolderView updateDefaultPhotoToFolder(int folderId, int photoId, int userId)
        { 


            var f = folderRepo.Read(folderId, userId);

            if (f == null) return null;

            if (f.DefaultPhoto.Id == photoId)
            {
                return mapper.Map<FolderView>(f);
            }

            var p = f.PhotosFolder.FirstOrDefault(pf => pf.PhotoId == photoId);
            /*  if(folderRepo.Read(folderId).PhotosTags.ContainsKey(p))
                  folderRepo.Read(folderId).PhotosTags.Remove(p);*/

            //     if(!folderRepo.Read(folderId).PhotosTags.ContainsKey(p))return null;

            // comprobar que la photo existe en la carpeta, comprobar si la foto esta eliminada,foto no es nula
            // lanzar excepciones en vez de nulos.

            var folderRet = folderRepo.updateDefaultPhotoToFolder(f, p.Photo, userId);
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