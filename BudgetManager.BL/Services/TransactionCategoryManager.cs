using BudgetManager.DL;
using BudgetManager.DL.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BudgetManager.BL.Services
{
    public class TransactionCategoryManager : ITransactionCategoryManager
    {
        public int Add(TransactionCategory entity)
        {
            int key;
            using (var context = new BudgetContext())
            {
                context.TransactionCategories.Add(entity);
                context.SaveChanges();
                key = entity.TransactionCategoryId;
            }
            return key;
        }

        public void Delete(int key)
        {
            TransactionCategory entity;
            using (var context = new BudgetContext())
            {
                entity = context.TransactionCategories.Find(key);
                context.TransactionCategories.Remove(entity);
                context.SaveChanges();
            }
        }

        public TransactionCategory Get(int key)
        {
            TransactionCategory entity;
            using (var context = new BudgetContext())
            {
                entity = context.TransactionCategories.Where(z => z.TransactionCategoryId == key).FirstOrDefault();
            }
            return entity;
        }

        public List<TransactionCategory> GetAll()
        {
            List<TransactionCategory> entityList;
            using (var context = new BudgetContext())
            {
                entityList = context.TransactionCategories.Include(z=>z.Transactions).ToList();
            }
            return entityList;
        }

        public void Update(TransactionCategory entity)
        {
            using (var context = new BudgetContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
        public TransactionCategory SelectTransactionCategoryrByName(string transactionCategoryName)
        {
            TransactionCategory transactionCategory;
            using (var context = new BudgetContext())
            {
                transactionCategory = context.TransactionCategories.Where(z => z.TransactionCategoryName == transactionCategoryName).FirstOrDefault();
            }
            return transactionCategory;
        }
    }
}
