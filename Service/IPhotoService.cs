using LHDTV.Models.ViewEntity;
using LHDTV.Models.Forms;

using System.Collections.Generic;
namespace LHDTV.Service{
    public interface IPhotoService
    {
        PhotoView GetPhoto(string id);

        PhotoView Create(AddPhotoForm form);
    
        List<PhotoView> GetAll();
    
        PhotoView Delete (string photoId);
    }
}