using LHDTV.Models.DbEntity;

using System.Collections.Generic;
namespace LHDTV.Repo
{

    public interface IFolderRepo : ICrudRepo<FolderDb, int>
    {
        FolderDb AddPhotoToFolder(FolderDb f , PhotoDb p, int userId);

        FolderDb deletePhotoToFolder(int folderId , PhotoDb p, int userId);
        bool ExistsPhoto(int f, int p);


        FolderDb updateDefaultPhotoToFolder(int folderId, PhotoDb p, int userId);
        List<LHDTV.Models.DbEntity.PhotoTransition> GetTransitionMetadata();
    }
}