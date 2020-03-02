
using AutoMapper;

    public class FolderProfile : Profile
    {
        public FolderProfile()
        {
            CreateMap<LHDTV.Models.DbEntity.FolderDb, LHDTV.Models.ViewEntity.FolderView>();
        }

    }