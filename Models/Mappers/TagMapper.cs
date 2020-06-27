using AutoMapper;

    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<LHDTV.Models.DbEntity.PhotoTagsTypes, LHDTV.Models.ViewEntity.PhotoTagsTypesView>();
        }

    }