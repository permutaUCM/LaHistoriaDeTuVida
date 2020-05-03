
using Microsoft.AspNetCore.Http;

namespace LHDTV.Service
{


    public interface ITokenRecoveryService
    {

        int RecoveryId(string token);

        string RecoveryToken(HttpContext context);

    }



}
