using AutoMapper;
using System.Linq;
namespace LHDTV.Profiles
{
    public class PhotoProfile : Profile
    {
        public PhotoProfile()
        {
            CreateMap<LHDTV.Models.DbEntity.PhotoDb, LHDTV.Models.ViewEntity.PhotoView>()
            .ForMember(dest => dest.Folders, opt => opt.MapFrom(src => src.PhotosFolder.Select(pf => new LHDTV.Models.DbEntity.FolderDb()
            {
                AutoStart = pf.Folder.AutoStart,
                DefaultPhoto = pf.Folder.DefaultPhoto,
                Deleted = pf.Folder.Deleted,
                Id = pf.Folder.Id,
                PhotosTags = pf.Folder.PhotosTags,
                ShowTime = pf.Folder.ShowTime,
                Title = pf.Folder.Title,
                Transition = pf.Folder.Transition,
                User = pf.Folder.User,

            }).ToList()));
            CreateMap<LHDTV.Models.DbEntity.TagDb, LHDTV.Models.ViewEntity.TagView>();
            CreateMap<LHDTV.Models.DbEntity.PhotoTagsTypes, LHDTV.Models.ViewEntity.PhotoTagsTypesView>();
            CreateMap<LHDTV.Models.DbEntity.Extra, LHDTV.Models.ViewEntity.ExtraView>();
        }
    }

}