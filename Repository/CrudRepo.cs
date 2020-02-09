using System.Collections.Generic;

namespace LHDTV.Repo
{
    public interface ICrudRepo<T, K>
    {
        T Create(T entity);
        T Read(K id);
        T Update(T entity);
        T Delete(K id);

        List<T> GetAll();
    }
}