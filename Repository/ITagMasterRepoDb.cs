using LHDTV.Models.DbEntity;

using System.Collections.Generic;
namespace LHDTV.Repo
{

    public interface ITagMasterRepoDb : ICrudRepo<TagDb, int>
    {

         TagDb Read(int id , int userId);

         TagDb Create(TagDb entity,int userId);

         TagDb Delete(int id,int userId);

         TagDb Update(TagDb entity, int userId);
         List<TagDb> GetAll(LHDTV.Models.Forms.Pagination pagination, int userId);
     
    }
}