using LHDTV.Models.DbEntity;

using Microsoft.EntityFrameworkCore;

namespace LHDTV.Repo
{

    public interface IFolderRepo : ICrudRepo<FolderDb, int>
    {
        //FolderRepo AddPhotoToFolder(FolderDb f , PhotoDb p);
    }
}