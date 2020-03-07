
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
                        PhotosTags = null,
                        Deleted = false


                };

                var folderRet = folderRepo.Create(folderPOJO);
                var folderTemp = mapper.Map<FolderView>(folderRet);

                return folderTemp;

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


        //añade una coleccion de fotos a una carpeta
        // primera version se añade de una en una
        
        public FolderView addPhotoToFolder(int folderId,PhotoDb photo ){

            var f = folderRepo.Read(folderId);
            if(f == null)
            {
                return null;
            }
            else{

                if(folderRepo.Read(f.Id).PhotosTags.ContainsKey(photo))return null;
             
                // esto valdria para una foto ¿bucle para una lista de fotos?
                folderRepo.Update(f).PhotosTags.Add(photo,null);
                var folderRet = folderRepo.Update(f);
                var folderTemp = mapper.Map<FolderView>(folderRet);

                return folderTemp;
            }
        }

        public FolderView deletePhotoToFolder(int folderId , PhotoDb p){

            var f = folderRepo.Read(folderId);

            if(f == null)return null;
            else{
                                
              /*  if(folderRepo.Read(folderId).PhotosTags.ContainsKey(p))
                    folderRepo.Read(folderId).PhotosTags.Remove(p);*/

                    if(!folderRepo.Read(folderId).PhotosTags.ContainsKey(p))return null;

                   folderRepo.Read(folderId).PhotosTags.Remove(p);
            
                   var folderRet = folderRepo.Update(f);
                   var folderTemp = mapper.Map<FolderView>(folderRet);


                return folderTemp;

            }

        }

        // actualizar photo por defecto
        
        public FolderView updateDefaultPhotoToFolder(int folderId, PhotoDb p){

                
            var f = folderRepo.Read(folderId);

            if(f == null)return null;
            else{
                                
              /*  if(folderRepo.Read(folderId).PhotosTags.ContainsKey(p))
                    folderRepo.Read(folderId).PhotosTags.Remove(p);*/

               //     if(!folderRepo.Read(folderId).PhotosTags.ContainsKey(p))return null;

                   f.DefaultPhoto = p;

                   var folderRet = folderRepo.Update(f);
                   var folderTemp = mapper.Map<FolderView>(folderRet);


                return folderTemp;

            }


        }





    }




}