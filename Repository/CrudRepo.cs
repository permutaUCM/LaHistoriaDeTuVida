using System.Collections.Generic;

namespace LHDTV.Repo
{
    public interface ICrudRepo<T, K>
    {
        T Create(T entity);
        T Read(K id);
        T Update(T entity);

        //Borrado fisico 
        T Delete(K entity);

        List<T> GetAll();
    }
}