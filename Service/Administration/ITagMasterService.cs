
using System.Collections.Generic;
using LHDTV.Models.Forms;
using LHDTV.Models.ViewEntity;
namespace LHDTV.Service
{

    public interface ITagMasterService
    {

        PhotoTagsTypesView Create(AddPhotoTagForm form);
        bool Remove(string id);
        PhotoTagsTypesView Update(AddPhotoTagForm form);
        PhotoTagsTypesView Read(string id);

        List<PhotoTagsTypesView> ReadAll(); 
    }
}