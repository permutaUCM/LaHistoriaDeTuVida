
using LHDTV.Models.DbEntity;
using LHDTV.Models.ViewEntity;
using LHDTV.Models.Forms;

using System.Collections.Generic;

namespace LHDTV.Service{

    public interface IFolderService
    {

        FolderView GetFolder(int Id);

        FolderView Create (AddFolderForm folder);

        FolderView Delete (int FolderId);

        FolderView Update(UpdateFolderForm form);

        List<FolderView> GetAll();
        
    }



}
