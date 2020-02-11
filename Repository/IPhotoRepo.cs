using LHDTV.Models.DbEntity;
namespace LHDTV.Repo
{

    public interface IPhotoRepo : ICrudRepo<PhotoDb, string>
    {
        PhotoDb getPhoto(string id);
        PhotoDB Create(PhotoDb photo);
        PhotoDB Update(PhotoDb photo);
        PhotoDb Delete(PhotoDb photo);
        
    }
}