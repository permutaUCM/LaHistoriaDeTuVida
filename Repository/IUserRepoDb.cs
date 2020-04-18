using LHDTV.Models.DbEntity;


namespace LHDTV.Repo
{

    public interface IUserRepoDb : ICrudRepo<UserDb, int>
    {

        UserDb ReadNick(string nick);
       
       UserDb ReadDni(string nick);

        UserDb Authenticate(string user,string password);

    }
}