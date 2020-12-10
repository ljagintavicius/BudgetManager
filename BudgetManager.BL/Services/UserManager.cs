using BudgetManager.DL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.BL
{
    public class UserManager : IUserManager
    {
        public int Add(User entity)
        {
            int key;
            using (var context = new BudgetContext())
            {
                context.Users.Add(entity);
                context.SaveChanges();
                key = entity.UserId;
            }
            return key;
        }

        public void Delete(int key)
        {
            User entity;
            using (var context = new BudgetContext())
            {
                entity = context.Users.Find(key);
                context.Users.Remove(entity);
                context.SaveChanges();
            }
        }

        public User Get(int key)
        {
            User entity;
            using (var context = new BudgetContext())
            {
                entity = context.Users.Where(z => z.UserId == key).FirstOrDefault();
            }
            return entity;
        }

        public List<User> GetAll()
        {
            List<User> entityList;
            using (var context = new BudgetContext())
            {
                entityList = context.Users.ToList();
            }
            return entityList;
        }

        public void Update(User entity)
        {
            using (var context = new BudgetContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public User SelectUserByName(string userName)
        {
            User user;
            using (var context = new BudgetContext())
            {
                user = context.Users.Where(z => z.Name == userName).FirstOrDefault();
            }
            return user;
        }
    }
}
