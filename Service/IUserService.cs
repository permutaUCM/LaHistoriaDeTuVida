using LHDTV.Models.ViewEntity;
using LHDTV.Models.Forms;

namespace LHDTV.Service
{
    
    
   public interface IUserService
    {

        UserView Create (AddUserForm user);

        UserView Authenticate(string username, string password);
    }
    
    

}
