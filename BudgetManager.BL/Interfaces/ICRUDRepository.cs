using System.Collections.Generic;

namespace BudgetManager.BL
{
    public interface ICRUDRepository<TEntity>
    {
        List<TEntity> GetAll();
        TEntity Get(int key);
        int Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(int key);

    }
}
