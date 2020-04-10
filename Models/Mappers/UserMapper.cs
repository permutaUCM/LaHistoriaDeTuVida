using AutoMapper;

    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<LHDTV.Models.DbEntity.UserDb, LHDTV.Models.ViewEntity.UserView>();
        }

    }