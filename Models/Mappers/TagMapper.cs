using AutoMapper;

    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<LHDTV.Models.DbEntity.TagDb, LHDTV.Models.ViewEntity.PhotoTagsTypesView>();
        }

    }