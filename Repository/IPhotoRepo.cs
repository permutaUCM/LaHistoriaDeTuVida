using LHDTV.Models.DbEntity;

using System.Collections.Generic;
namespace LHDTV.Repo
{

    public interface IPhotoRepo : ICrudRepo<PhotoDb, int>
    {
        ICollection<PhotoTagsTypes> getTagTypes();
        void RemoveTag(TagDb tag);
        void UpdateTag(TagDb tag);

    }
}