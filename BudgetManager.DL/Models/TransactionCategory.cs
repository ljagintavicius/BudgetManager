using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetManager.DL.Models
{
    [Table("TransactionCategories")]
    public class TransactionCategory
    {
        [Key]
        public int TransactionCategoryId { get; set; }
        public ETransactionType TransactionType { get; set; }
        public string TransactionCategoryName { get; set; }
        public List<Transaction> Transactions { get; set; }

    }
}