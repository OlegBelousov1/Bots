using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InstaDirect.Models
{
    public class RefferalsInfo
    {
        public int Id { get; set; }
        public long InviterTId { get; set; }
        public long RefferalTId { get; set; }
        public DateTime JoinTime { get; set; }
    }
}