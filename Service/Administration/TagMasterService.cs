
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
            if (ex1 == null && form.Extra1 != null && form.Extra1 != "")
            {
                ex1 = adminRepo.CreateExtra(form.Extra1);
            }
            if (ex2 == null && form.Extra2 != null && form.Extra2 != "")
            {
                ex2 = adminRepo.CreateExtra(form.Extra2);
            }
            if (ex3 == null && form.Extra3 != null && form.Extra3 != "")
            {
                ex3 = adminRepo.CreateExtra(form.Extra3);
            }


            PhotoTagsTypes tagPOJO = new PhotoTagsTypes()
            {

                Name = form.Title,

                Extra1Name = (ex1 == null) ? null : ex1.Name,
                Extra2Name = (ex2 == null) ? null : ex2.Name,
                Extra3Name = (ex3 == null) ? null : ex3.Name,
                Icon = form.Icon

            };

            var TagRet = adminRepo.Create(tagPOJO, 0);
            TagRet.Extra1 = ex1;
            TagRet.Extra1Name = (ex1 != null) ? ex1.Name : null;
            TagRet.Extra2 = ex2;
            TagRet.Extra2Name = (ex2 != null) ? ex2.Name : null;
            TagRet.Extra3 = ex3;
            TagRet.Extra3Name = (ex3 != null) ? ex3.Name : null;
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

            if (t.Extra1 == null && tag.Extra1 != null && tag.Extra1 != "")
            {
                t.Extra1 = adminRepo.CreateExtra(tag.Extra1);
            }
            if (t.Extra2 == null && tag.Extra2 != null && tag.Extra2 != "")
            {
                t.Extra2 = adminRepo.CreateExtra(tag.Extra2);
            }
            if (t.Extra3 == null && tag.Extra3 != null && tag.Extra3 != "")
            {
                t.Extra3 = adminRepo.CreateExtra(tag.Extra3);
            }

            t.Icon = tag.Icon;
            t.Name = tag.Title.Trim();

            var tagRet = adminRepo.Update(t, userId);
            tagRet.Extra1 = t.Extra1;
            tagRet.Extra1Name = (t.Extra1 != null) ? t.Extra1.Name : null;
            tagRet.Extra2 = t.Extra2;
            tagRet.Extra2Name = (t.Extra2 != null) ? t.Extra2.Name : null;
            tagRet.Extra3 = t.Extra3;
            tagRet.Extra3Name = (t.Extra3 != null) ? t.Extra3.Name : null;
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