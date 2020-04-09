using LHDTV.Entities;
using System.Collections.Generic;



namespace LHDTV.Service{
    
    
   public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }
    
    

}
