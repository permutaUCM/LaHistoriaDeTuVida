
using AutoMapper;
using System.Linq;
public class FolderProfile : Profile
{
    public FolderProfile()
    {
        CreateMap<LHDTV.Models.DbEntity.FolderDb, LHDTV.Models.ViewEntity.FolderView>()
            .ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.DefaultPhoto.Url))
            .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.PhotosFolder.Select(pf => new LHDTV.Models.DbEntity.PhotoDb()
            {
                Caption = pf.Photo.Caption,
                City = pf.Photo.City,
                Country = pf.Photo.Country,
                Deleted = pf.Photo.Deleted,
                Id = pf.Photo.Id,
                RealDate = pf.Photo.RealDate,
                Size = pf.Photo.Size,
                Tag = pf.Photo.Tag,
                Title = pf.Photo.Title,
                UploadDate = pf.Photo.UploadDate,
                Url = pf.Photo.Url,
                User = pf.Photo.User,
            })));
        CreateMap<LHDTV.Models.DbEntity.PhotoTransition, LHDTV.Models.ViewEntity.PhotoTransitionView>();
    }

}