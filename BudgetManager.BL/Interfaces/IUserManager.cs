using BudgetManager.DL;

namespace BudgetManager.BL
{
    public interface IUserManager : ICRUDRepository<User>
    {
        User SelectUserByName(string userName);
    }
}