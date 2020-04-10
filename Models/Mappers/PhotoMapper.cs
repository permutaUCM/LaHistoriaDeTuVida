using AutoMapper;

namespace LHDTV.Profiles
{
    public class PhotoProfile : Profile
    {
        public PhotoProfile()
        {
            CreateMap<LHDTV.Models.DbEntity.PhotoDb, LHDTV.Models.ViewEntity.PhotoView>();
            CreateMap<LHDTV.Models.DbEntity.TagDb, LHDTV.Models.ViewEntity.TagView>();
            CreateMap<LHDTV.Models.DbEntity.PhotoTagsTypes, LHDTV.Models.ViewEntity.PhotoTagsTypesView>();
            CreateMap<LHDTV.Models.DbEntity.Extra, LHDTV.Models.ViewEntity.ExtraView>();
        }
    }

}