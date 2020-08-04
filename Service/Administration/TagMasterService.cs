
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

        private readonly IPhotoRepo photoRepo;
    
        private readonly IMapper mapper;

        private readonly string basePath;

        private const string BASEPATHCONF = "tagRoutes:uploadRoute";

        public TagMasterService(ITagMasterRepoDb _adminRepo,
                            IMapper _mapper,
                            IConfiguration _configuration,
                            IPhotoRepo _photoRepo
                            )
        {
            adminRepo = _adminRepo;
            mapper = _mapper;
            basePath = _configuration.GetValue<string>(BASEPATHCONF);
            photoRepo = _photoRepo;
        }

        
        public PhotoTagsTypesView Create (AddPhotoTagForm form,int userId){
            
            var types = photoRepo.getTagTypes();
            var myTagType = types.First(t => t.Name == form.Extra1);

            if (myTagType == null) return null;// exception not found o parecido 400 

            
            PhotoTagsTypes tagPOJO = new PhotoTagsTypes()
            {
                
            Name = form.Title,
            
  
            Extra1 = myTagType.Extra1,
            Extra2 = myTagType.Extra2,
            Extra3 = myTagType.Extra3           
              
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
            // t.Extra1 = tag.Extra1.Trim();
            // t.Extra2 = tag.Extra2.Trim();
            // t.Extra3 = tag.Extra3.Trim();
            t.Extra1 = null;
            t.Extra2 = null;
            t.Extra3 = null;


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