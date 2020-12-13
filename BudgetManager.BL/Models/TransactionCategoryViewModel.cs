using BudgetManager.DL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.BL.Models
{
    public class TransactionCategoryViewModel
    {
        public string TransactionCategoryName { get; set; }
        public decimal Amount { get; set; }
    }
}
