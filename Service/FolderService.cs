
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

        private readonly IMapper mapper;

        private readonly string basePath;

        private const string BASEPATHCONF = "folderRoutes:uploadRoute";
        
        public FolderService(IFolderRepo _folderRepo, IMapper _mapper, IConfiguration _configuration)
        {
            folderRepo = _folderRepo;
            mapper = _mapper;
            basePath = _configuration.GetValue<string>(BASEPATHCONF);

        }

        public FolderView GetFolder(int id)
        {

            var folder = folderRepo.Read(id);
            var folderRet = mapper.Map<FolderView>(folder);
                
            return folderRet;
        }

        // crea un carpeta vacia
        public FolderView Create (AddFolderForm folder){

                FolderDb folderPOJO = new FolderDb()
                {

                        DefaultPhoto = null,
                        Title = folder.Title,
                        Photos = null,
                        Deleted = false


                };

                var folderRet = folderRepo.Create(folderPOJO);
                var folderTemp = mapper.Map<FolderView>(folderRet);

                return folderTemp;

        }

        //añade una coleccion de fotos a una carpeta

        public FolderView AddPhotoToFolder(PhotoDb photo , int folderId){

            var folder = folderRepo.Read(folderId);
            if(folder == null)
            {
                return null;
            }
            else{

                
                // No se como devolver la foto para comprobar si existe
                //if(folder.Photos.Select(p => photo.Id))


            }

        }

        public FolderView Delete(int folderId){

            var folder = folderRepo.Read(folderId);
            if(folder == null)
            {
                return null;
            }

            folder.Deleted = true;

            var folderRet = folderRepo.Update(folder);
            var folderMap = mapper.Map<FolderView>(folderRet);

            return folderMap;

        }

        public FolderView Update(UpdateFolderForm folder){

                var  f = folderRepo.Read(folder.id);
                if(f == null)return null;

                f.Title = folder.Title;

                var folderRet = folderRepo.Update(f);
                var folderTemp = mapper.Map<FolderView>(folderRet);

                return folderTemp;

        }



    }




}