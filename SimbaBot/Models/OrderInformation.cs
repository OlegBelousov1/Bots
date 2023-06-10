namespace SimbaBot.Models
{
    public class OrderInformation
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public int Price { get; set; }

        public OrderInformation(string title, string descr, string text, int price)
        {
            Title = title;
            Description = descr;
            Text = text;
            Price = price;
        }

        public OrderInformation()
        {
            
        }
    }
}
