using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class TicketStatus
    {
        [Key]
        public long SN { get; set; }

        public string Values { get; set; }
    }
}
