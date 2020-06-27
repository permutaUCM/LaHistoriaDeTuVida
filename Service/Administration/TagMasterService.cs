
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

            PhotoTagsTypes tagPOJO = new PhotoTagsTypes()
            {
                
              Name = form.Title,
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

        public PhotoTagsTypesView Delete(string tagName, int userId){

            var tag = adminRepo.Read(tagName,userId);

            if(tag == null){
                return null;
            }


            var tagRet = adminRepo.Delete(tag.Name,userId);
            var tagMap = mapper.Map<PhotoTagsTypesView>(tagRet);

            return tagMap;

        }

        public PhotoTagsTypesView Update(AddPhotoTagForm tag,int userId){

            var t = adminRepo.Read(tag.Title,userId);
            if(t == null){
                return null;
            }

            t.Name = tag.Title.Trim();
            t.Extra1 = tag.Extra1.Trim();
            t.Extra2 = tag.Extra2.Trim();
            t.Extra3 = tag.Extra3.Trim();

            var tagRet = adminRepo.Update(t, userId);
            var tagTemp = mapper.Map<PhotoTagsTypesView>(tagRet);

            return tagTemp;

        }
        public PhotoTagsTypesView Read(string title,int userId){

            var tag = adminRepo.Read(title,userId);
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