using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InstaDirect.Models
{
    public class BotUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime FirstDate { get; set; }
        public long TId { get; set; }
        public bool Banned { get; set; }
        public DateTime LastTake { get; set; }
    }
} 