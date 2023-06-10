namespace SimbaBot.Models
{
    public class BotUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime FirstDate { get; set; }
        public long TId { get; set; }
        public bool Banned { get; set; }
        public UserStatus Status { get; set; }
        public decimal Balance { get; set; }
        public long Inviter { get; set; }
    }
}
