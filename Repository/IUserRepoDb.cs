using LHDTV.Models.DbEntity;


namespace LHDTV.Repo
{

    public interface IUserRepoDb : ICrudRepo<UserDb, int>
    {

        UserDb ReadNick(string nick);
       

    }
}