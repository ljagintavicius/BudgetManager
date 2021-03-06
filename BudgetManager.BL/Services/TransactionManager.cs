﻿using BudgetManager.DL;
using BudgetManager.DL.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BudgetManager.BL.Services
{
    public class TransactionManager : ICRUDRepository<Transaction>
    {
        public int Add(Transaction entity)
        {
            int key;
            using (var context = new BudgetContext())
            {
                context.Transactions.Add(entity);
                context.SaveChanges();
                key = entity.TransactionId;
            }
            return key;
        }

        public void Delete(int key)
        {
            Transaction entity;
            using (var context = new BudgetContext())
            {
                entity = context.Transactions.Find(key);
                context.Transactions.Remove(entity);
                context.SaveChanges();
            }
        }

        public Transaction Get(int key)
        {
            Transaction entity;
            using (var context = new BudgetContext())
            {
                entity = context.Transactions.Where(z => z.TransactionId == key).Include(z => z.TransactionCategory).FirstOrDefault();
            }
            return entity;
        }

        public List<Transaction> GetAll()
        {
            List<Transaction> entityList;
            using (var context = new BudgetContext())
            {
                entityList = context.Transactions.Include(z => z.TransactionCategory).Include(z => z.User).ToList();
            }
            return entityList;
        }

        public void Update(Transaction entity)
        {
            using (var context = new BudgetContext())
            {
                Transaction oldEntity = context.Transactions.Find(entity.TransactionId);
                if (oldEntity == null) return;
                context.Entry(oldEntity).CurrentValues.SetValues(entity);
                context.SaveChanges();
            }
        }
    }
}
