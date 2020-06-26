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


    public class TagMasterService : ITagMasterService
    {
        
        private readonly ITagMasterRepoDb adminRepo;
    
        private readonly IMapper mapper;

        private readonly string basePath;

        private const string BASEPATHCONF = "folderRoutes:uploadRoute";

        public TagMasterService(TagMasterRepoDb _adminRepo,
                            IMapper _mapper,
                            IConfiguration _configuration
                            )
        {
            adminRepo = _adminRepo;
            mapper = _mapper;
            basePath = _configuration.GetValue<string>(BASEPATHCONF);
        }

        
        public PhotoTagsTypesView Create (AddPhotoTagForm form,int userId){

            TagDb tagPOJO = new TagDb()
            {
                
              Title = form.Title,
              Type =form.Type,
              Extra1 = form.Extra1,
              Extra2 = form.Extra2,
              Extra3 = form.Extra3                
              
            };

            var TagRet = adminRepo.Create(tagPOJO, 0);
            var tagTemp = mapper.Map<PhotoTagsTypesView>(TagRet);

            return tagTemp;

        
        }

        
        // bool Remove(string id){




        // }

        public PhotoTagsTypesView Delete(int tagId, int userId){

            var tag = adminRepo.Read(tagId,userId);

            if(tag == null){
                return null;
            }

            tag.Deleted = true;

            var tagRet = adminRepo.Update(tag,userId);
            var tagMap = mapper.Map<PhotoTagsTypesView>(tagRet);

            return tagMap;

        }

        public PhotoTagsTypesView Update(AddPhotoTagForm tag,int userId){

            var t = adminRepo.Read(tag.Id,userId);
            if(t == null){
                return null;
            }

            t.Title = tag.Title.Trim();
            t.Type = tag.Type.Trim();

            var tagRet = adminRepo.Update(t, userId);
            var tagTemp = mapper.Map<PhotoTagsTypesView>(tagRet);

            return tagTemp;

        }
        public PhotoTagsTypesView Read(int id,int userId){

            var tag = adminRepo.Read(id,userId);
            var tagRet = mapper.Map<PhotoTagsTypesView>(tag);
            return tagRet;

        }

        public List<PhotoTagsTypesView> ReadAll(Pagination pagination, int userId){

            var tags = adminRepo.GetAll(pagination, userId);
            var allTags = tags.Select(f => this.mapper.Map<PhotoTagsTypesView>(f)).ToList();

            return allTags;

        }


    }


}