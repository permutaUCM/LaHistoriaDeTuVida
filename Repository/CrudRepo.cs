using System.Collections.Generic;
using LHDTV.Models.Forms;
namespace LHDTV.Repo
{
    public interface ICrudRepo<T, K>
    {
        T Create(T entity);
        T Read(K id);
        T Update(T entity);

        //Borrado fisico 
        T Delete(K entity);

        List<T> GetAll(Pagination Pag, int user);
    }
}