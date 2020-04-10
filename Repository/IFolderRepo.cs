using LHDTV.Models.DbEntity;

using Microsoft.EntityFrameworkCore;

namespace LHDTV.Repo
{

    public interface IFolderRepo : ICrudRepo<FolderDb, int>
    {
        FolderDb AddPhotoToFolder(FolderDb f , PhotoDb p);

        FolderDb deletePhotoToFolder(int folderId , PhotoDb p);

        FolderDb updateDefaultPhotoToFolder(int folderId, PhotoDb p);


    }
}