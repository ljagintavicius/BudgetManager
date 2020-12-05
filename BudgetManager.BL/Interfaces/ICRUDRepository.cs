using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
