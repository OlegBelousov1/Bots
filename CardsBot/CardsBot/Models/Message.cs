﻿namespace CardsBot.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public MessageType MessageType { get; set; }    
    }
}
