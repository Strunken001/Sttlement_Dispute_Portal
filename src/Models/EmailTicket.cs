using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class EmailTicket 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TicketNo { get; set; }

        [StringLength(500)]
        [Display(Name = "Sender")]
        public string EmailFrom { get; set; }

        [StringLength(500)]
        [Display(Name = "Destination Email")]
        public string EmailTo { get; set; }

        [Display(Name = "Subject")]
        public string EmailSubject { get; set; }

        [Display(Name = "Message")]
        public string EmailContent { get; set; }

        [Display(Name = "AssignedTo")]
        public string AssignedTo { get; set; }

        [Display(Name = "Email Date")]
        public DateTimeOffset EmailDateTime { get; set; }

        [Display(Name = "Reply Status")]
        public int ReplyStatus { get; set; }

        [Display(Name = "Rating")]
        public string rating { get; set; }

        [Display(Name = "Replied By")]
        public string RepliedBy { get; set; }

        [Display(Name = "Reply Customer")]
        public string ReplyCustomer { get; set; }

    }
}
