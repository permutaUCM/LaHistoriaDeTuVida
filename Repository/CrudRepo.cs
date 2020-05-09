using System.Collections.Generic;
using LHDTV.Models.Forms;
namespace LHDTV.Repo
{
    public interface ICrudRepo<T, K>
    {
        T Create(T entity, int userId);
        T Read(K id, int userId);
        T Update(T entity, int userId);

        //Borrado fisico 
        T Delete(K entity, int userId);

        List<T> GetAll(Pagination Pag, int userId);
    }
}