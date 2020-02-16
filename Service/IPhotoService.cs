using LHDTV.Models.ViewEntity;
using LHDTV.Models.Forms;

using System.Collections.Generic;
namespace LHDTV.Service{
    public interface IPhotoService
    {
        PhotoView GetPhoto(int id);

        PhotoView Create(AddPhotoForm form);
    
        List<PhotoView> GetAll();
    
        PhotoView Delete (int photoId);

        PhotoView Update (UpdatePhotoForm form);
    }
}