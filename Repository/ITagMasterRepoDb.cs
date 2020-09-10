using LHDTV.Models.DbEntity;

using System.Collections.Generic;
namespace LHDTV.Repo
{

    public interface ITagMasterRepoDb : ICrudRepo<PhotoTagsTypes, string>
    {

        //  TagDb Read(int id , int userId);

        //  TagDb Create(TagDb entity,int userId);

        //  TagDb Delete(string title,int userId);

        //  TagDb Update(TagDb entity, int userId);
        //  List<TagDb> GetAll(LHDTV.Models.Forms.Pagination pagination, int userId);

        List<Extra> GetAllExtras();

        Extra CreateExtra(string extraName);

        

     
    }
}