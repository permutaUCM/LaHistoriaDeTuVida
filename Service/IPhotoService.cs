using LHDTV.Models.ViewEntity;
using LHDTV.Models.Forms;

using System.Collections.Generic;
namespace LHDTV.Service
{
    public interface IPhotoService
    {
        PhotoView GetPhoto(int id, int userId);

        PhotoView Create(AddPhotoForm form, int userId);

        // List<PhotoView> GetAll();

        PhotoView Delete(int photoId, int userId);

        PhotoView Update(UpdatePhotoForm form, int userId);

        //Returns the diferent type tags for a photo
        ICollection<PhotoTagsTypesView> GetTagTypes();

        //PhotoMetadata GetMetadata();

        PhotoView AddTag(TagForm form, int userId);
        PhotoView RemoveTag(TagFormDelete form, int userId);

        PhotoView UpdateTag(TagFormUpdate form, int userId);

        List<PhotoView> GetAll(Pagination pagination, int userId);
        List<LHDTV.Models.DbEntity.TagDb> GetAllTags(int userId, int folderId);


        List<PhotoView> GetAll(Pagination pagination, int userId, int folderId);

    }
}