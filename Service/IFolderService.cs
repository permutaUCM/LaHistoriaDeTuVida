
using LHDTV.Models.DbEntity;
using LHDTV.Models.ViewEntity;
using LHDTV.Models.Forms;

using System.Collections.Generic;

namespace LHDTV.Service{

    public interface IFolderService
    {

        FolderView GetFolder(int Id, int userId);

        FolderView Create (AddFolderForm folder, int userId);

        FolderView Delete (int FolderId, int userId);

        FolderView Update(UpdateFolderForm form, int userId);


        List<FolderView> GetAll(Pagination pagination, int userId);

        FolderView addPhotoToFolder(int folderId, PhotoDb photo, int userId);

        FolderView deletePhotoToFolder(int folderId, PhotoDb photo, int userId);

        FolderView updateDefaultPhotoToFolder(int folderId, PhotoDb p, int userId);
 
        LHDTV.Models.FolderMetadata GetMetadata();
    }



}
