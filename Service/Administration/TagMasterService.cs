
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


        public PhotoTagsTypesView Create(AddPhotoTagForm form, int userId)
        {

            var types = photoRepo.getTagTypes();
            var myTagType = types.FirstOrDefault(t => t.Name == form.Title);
            var extras = adminRepo.GetAllExtras();
            if (myTagType != null) return null;

            var ex1 = extras.Where(ext => form.Extra1 == ext.Name).FirstOrDefault();
            var ex2 = extras.Where(ext => form.Extra2 == ext.Name).FirstOrDefault();
            var ex3 = extras.Where(ext => form.Extra3 == ext.Name).FirstOrDefault();
            if (form.Extra1 != null && form.Extra1 != "" && ex1 == null){
                return null;
            }
            if (form.Extra2 != null && form.Extra2 != "" && ex2 == null){
                return null;
            }
            if (form.Extra3 != null && form.Extra3 != "" && ex3 == null){
                return null;
            }


            PhotoTagsTypes tagPOJO = new PhotoTagsTypes()
            {

                Name = form.Title,

                Extra1Name = (ex1 == null) ? null : ex1.Name,
                Extra2Name = (ex2 == null) ? null : ex2.Name,
                Extra3Name = (ex3 == null) ? null : ex3.Name

            };
            
            var TagRet = adminRepo.Create(tagPOJO, 0);
            var tagTemp = mapper.Map<PhotoTagsTypesView>(TagRet);

            return tagTemp;


        }


        // bool Remove(string id){




        // }

        public PhotoTagsTypesView Delete(string tagName, int userId)
        {

            var tag = adminRepo.Read(tagName, userId);

            if (tag == null)
            {
                return null;
            }


            var tagRet = adminRepo.Delete(tagName, userId);
            var tagMap = mapper.Map<PhotoTagsTypesView>(tagRet);

            return tagMap;

        }

        public PhotoTagsTypesView Update(AddPhotoTagForm tag, int userId)
        {

            var t = adminRepo.Read(tag.Title, userId);
            var extras = adminRepo.GetAllExtras();
            if (t == null)
            {
                return null;
            }

            t.Extra1 = extras.FirstOrDefault(ext => ext.Name == tag.Extra1.Trim());
            t.Extra2 = extras.FirstOrDefault(ext => ext.Name == tag.Extra2.Trim());
            t.Extra3 = extras.FirstOrDefault(ext => ext.Name == tag.Extra3.Trim());

            
            t.Name = tag.Title.Trim();

            var tagRet = adminRepo.Update(t, userId);
            var tagTemp = mapper.Map<PhotoTagsTypesView>(tagRet);

            return tagTemp;

        }
        public PhotoTagsTypesView Read(string title, int userId)
        {

            var tag = adminRepo.Read(title, userId);
            var tagRet = mapper.Map<PhotoTagsTypesView>(tag);
            return tagRet;

        }

        public List<PhotoTagsTypesView> ReadAll(Pagination pagination, int userId)
        {

            var tags = adminRepo.GetAll(pagination, userId);
            var allTags = tags.Select(f => this.mapper.Map<PhotoTagsTypesView>(f)).ToList();

            return allTags;
        }
        public List<ExtraView> GetAllExtras()
        {
            var extras = adminRepo.GetAllExtras();
            var allExtras = extras.Select(ext => this.mapper.Map<ExtraView>(ext)).ToList();
            return allExtras;
        }

    }


}