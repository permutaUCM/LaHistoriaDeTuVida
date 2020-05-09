using LHDTV.Models.DbEntity;


namespace LHDTV.Repo
{

    public interface IUserRepoDb : ICrudRepo<UserDb, int>
    {

        UserDb ReadNick(string nick, int userId);
       
       UserDb ReadDni(string nick, int userId);

        UserDb Authenticate(string user,string password, int userId);

    }
}