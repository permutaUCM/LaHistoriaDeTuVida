using LHDTV.Models.DbEntity;
using System.Collections.Generic;

namespace LHDTV.Repo
{

    public interface IPhotoRepo : ICrudRepo<PhotoDb, int>
    {
        ICollection<PhotoTagsTypes> getTagTypes();
        void RemoveTag(TagDb tag, int userId);
        void UpdateTag(TagDb tag, int userId);

        List<PhotoDb> GetAll(LHDTV.Models.Forms.Pagination Pag, int userId, int folderId);

        List<TagDb> getAllTags(int userId, int folderId);
    }
}