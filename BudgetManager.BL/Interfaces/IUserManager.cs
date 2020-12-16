using BudgetManager.DL;

namespace BudgetManager.BL
{
    public interface IUserManager : ICRUDRepository<User>
    {
        User GetByName(string userName);
    }
}