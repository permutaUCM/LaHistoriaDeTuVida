
using LHDTV.Models.DbEntity;
using LHDTV.Models.ViewEntity;
using LHDTV.Models.Forms;
using System.Collections.Generic;
namespace LHDTV.Service
{

    public interface IFolderService
    {

        FolderView GetFolder(int Id, int userId);

        FolderView Create (AddFolderForm folder, int userId);

        FolderView Delete (int FolderId, int userId);

        FolderView Update(UpdateFolderForm form, int userId);


        List<FolderView> GetAll(Pagination pagination, int userId);

        FolderView addPhotoToFolder(int folderId, int photo, int userId);

        FolderView deletePhotoToFolder(int folderId, List<int> photo, int userId);

        FolderView updateDefaultPhotoToFolder(int folderId, int p, int userId);
 
        LHDTV.Models.FolderMetadata GetMetadata();
    }



}
