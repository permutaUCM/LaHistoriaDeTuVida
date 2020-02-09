using LHDTV.Models.ViewEntity;

using LHDTV.Repo;

using AutoMapper;
namespace LHDTV.Service
{

    public class PhotoService : IPhotoService{

        private readonly IPhotoRepo photoRepo;

        private readonly IMapper mapper;
        public PhotoService(IPhotoRepo _photoRepo, IMapper _mapper){
            photoRepo = _photoRepo;
            mapper = _mapper;
        }
        
        public PhotoView GetPhoto(string id){
            var photo = photoRepo.getPhoto(id);
            var photoret = mapper.Map<PhotoView>(photo);
            return photoret;
        }


    }
}