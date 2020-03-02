
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

        public FolderView Create (AddFolderForm form){



        }

        public FolderView Delete(int folderId){

            

        }

        public FolderView Update(UpdateFolderForm folder){


        }

        public List<FolderView> GetAll(){



        }
    }




}