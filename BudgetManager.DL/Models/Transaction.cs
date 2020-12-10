﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManager.DL.Models;

namespace BudgetManager.DL.Models
{
    [Table("Transactions")]
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public int TransactionCategoryId { get; set; }
        public virtual TransactionCategory TransactionCategory { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Sum { get; set; }
    }
}
