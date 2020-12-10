using BudgetManager.DL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetManager.DL
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public virtual List<Transaction> Transactions { get; set; }
    }
}
