namespace CardsBot.Models
{
    public class BotUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime FirstDate { get; set; }
        public long TId { get; set; }
        public bool Banned { get; set; }
        public string Status { get; set; }
        public bool BotIsBanned { get; set; }
    }
}
