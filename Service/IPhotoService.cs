using LHDTV.Models.ViewEntity;

namespace LHDTV.Service{
    public interface IPhotoService
    {
        PhotoView GetPhoto(string id);
    }
}