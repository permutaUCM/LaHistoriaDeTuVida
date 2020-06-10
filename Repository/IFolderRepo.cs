using LHDTV.Models.DbEntity;

using System.Collections.Generic;
namespace LHDTV.Repo
{

    public interface IFolderRepo : ICrudRepo<FolderDb, int>
    {
        FolderDb AddPhotoToFolder(FolderDb f, PhotoDb p, int userId);

        FolderDb deletePhotosToFolder(int folderId, List<PhotoDb> p, int userId);
        bool ExistsPhoto(int f, int p);


        FolderDb updateDefaultPhotoToFolder(FolderDb folder, PhotoDb p, int userId);
        List<LHDTV.Models.DbEntity.PhotoTransition> GetTransitionMetadata();

        FolderDb Read(int id, LHDTV.Models.Forms.Pagination p, int userId);

    }
}