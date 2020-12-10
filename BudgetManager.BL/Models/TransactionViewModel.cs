using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.BL
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }
        public string TransactionType { get; set; }
        public string DateTime { get; set; }
        public string TransactionCategory { get; set; }
        public string UserName { get; set; }
        public decimal Amount { get; set; }
    }
}
