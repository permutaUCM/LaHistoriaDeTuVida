using LHDTV.Models.ViewEntity;
using LHDTV.Models.Forms;

namespace LHDTV.Service
{


    public interface IUserService
    {

        UserView GetUser(int Id);
        UserView Create(AddUserForm user);

        UserView Delete(string dni);

        UserView UpdateInfo(UpdateUserForm user,int id);

        UserView Authenticate(string username, string password);

        bool RequestPasswordRecovery(RequestPasswordRecoveryForm passwordRecoveryForm);

        bool PasswordRecovery(PasswordRecoveryForm passwordRecovery);
    }



}
