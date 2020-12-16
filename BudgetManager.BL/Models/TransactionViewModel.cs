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
