namespace SimbaBot.Models
{
    public class Withdrawal
    {
        public int Id { get; set; }
        public long Tid { get; set; }
        public DateTime DateWithdrawal { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
