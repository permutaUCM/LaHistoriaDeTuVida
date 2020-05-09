
using AutoMapper;

public class FolderProfile : Profile
{
    public FolderProfile()
    {
        CreateMap<LHDTV.Models.DbEntity.FolderDb, LHDTV.Models.ViewEntity.FolderView>()
            .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.DefaultPhoto.Url));

        CreateMap<LHDTV.Models.DbEntity.PhotoTransition, LHDTV.Models.ViewEntity.PhotoTransitionView>();
    }

}