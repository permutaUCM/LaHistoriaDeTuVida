using LHDTV.Models.ViewEntity;
using LHDTV.Models.DbEntity;
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
        
        public PhotoView GetPhoto(string id){  //Repasar?¿?¿
            var photo = photoRepo.getPhoto(id);
            var photoret = mapper.Map<PhotoView>(photo); 
            return photoret;
        }

        //creamos una nueva foto y la devolvemos?¿ para tratarla?¿
        public PhotoView Create(PhotoDb photo){
            photoRepo.Create(photo);
            return photo;
        }
        public PhotoView Update(PhotoDb photo){
            photoRepo.Update(photo);
            return photo;
        }

        public PhotoView Delete(PhotoDb photo){
            photoRepo.Delete(photo);
            return photo;
        }


    }
}