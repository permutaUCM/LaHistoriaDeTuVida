using LHDTV.Models.ViewEntity;
using LHDTV.Models.Forms;

namespace LHDTV.Service
{


    public interface IUserService
    {

        UserView Create(AddUserForm user);

        UserView Authenticate(string username, string password);

        bool RequestPasswordRecovery(RequestPasswordRecoveryForm passwordRecoveryForm);

        bool PasswordRecovery(PasswordRecoveryForm passwordRecovery);

        UserView UpdateInfo(UpdateUserForm user);
    }



}
