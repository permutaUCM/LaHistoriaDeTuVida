
using System.Collections.Generic;
using LHDTV.Models.Forms;
using LHDTV.Models.ViewEntity;
namespace LHDTV.Service
{

    public interface ITagMasterService
    {

        // PhotoTagsTypesView Create(AddPhotoTagForm tag,int userId);
        PhotoTagsTypesView Create(AddPhotoTagForm form, int userId);
        PhotoTagsTypesView Update(AddPhotoTagForm tag, int userId);
        PhotoTagsTypesView Delete(string tagName, int userId);

        PhotoTagsTypesView Read(string title, int userId);

        List<PhotoTagsTypesView> ReadAll(Pagination pagination, int userId);

        List<ExtraView> GetAllExtras();
        // // bool Remove(string id);
        // PhotoTagsTypesView Delete(int tagId, int userId);
        // PhotoTagsTypesView Update(AddPhotoTagForm tag,int userId);

        // // PhotoTagsTypesView Read(string id);
        // PhotoTagsTypesView Read(string id,int userId);

        // List<PhotoTagsTypesView> ReadAll(Pagination pagination, int userId); 
    }
}