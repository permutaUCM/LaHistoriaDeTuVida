using AutoMapper;

namespace LHDTV.Profiles{
    public class PhotoProfile: Profile{
        public PhotoProfile(){
            CreateMap<LHDTV.Models.DbEntity.PhotoDb, LHDTV.Models.ViewEntity.PhotoView>();
        }
    }
}