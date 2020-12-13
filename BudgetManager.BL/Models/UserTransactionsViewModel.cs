using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetManager.BL.Models
{
    public class UserTransactionsViewModel
    {
        public string UserName { get; set; }
        public decimal Income { get; set; }
        public decimal Expenses { get; set; }

    }
}
