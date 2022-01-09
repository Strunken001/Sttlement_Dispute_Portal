using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class Ticket : BaseEntity
    {
        public Ticket()
        {
            this.ticketStatus = Enum.TicketStatus.Unassigned;
            this.ticketType = Enum.TicketType.Complaint;
            this.ticketPriority = Enum.TicketPriority.Low;

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ticketId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ticketNo { get; set; }

        //public  DateTime CreateAt { get; set; }

        [StringLength(500)]
        [Display(Name = "Problem Description")]
        public string description { get; set; }

        [StringLength(500)]
        [Display(Name = "Comments")]
        public string comments { get; set; }

        [Display(Name = "Ticket Name")]
        public string ticketName { get; set; }

        [Display(Name = "Customer ID")]
        public Guid customerId { get; set; }

        [Display(Name = "Assign To")]
        public string supportAgentId { get; set; }

        [Display(Name = "Contact ID")]
        public Guid contactId { get; set; }

        [Display(Name = "Status")]
        public Enum.TicketStatus ticketStatus { get; set; }

        [Display(Name = "Email")]
        public string email { get; set; }

        [Display(Name = "Ticket Type")]
        public Enum.TicketType ticketType { get; set; }

        [Display(Name = "Ticket Priority")]
        public Enum.TicketPriority ticketPriority { get; set; }

        [Display(Name = "Category")]
        public Enum.TicketChannel ticketChannel { get; set; }

        [Display(Name = "Channel")]
        public Enum.TicketChannel ticketSource { get; set; }

        [Display(Name = "Customer Location")]
        public string customerLocation { get; set; }

        [Display(Name = "SubCategory")]
        public Enum.SubCategory1 subCategory1 { get; set; }

        [Display(Name = "SubCategory")]
        public Enum.SubCategory2 subCategory2 { get; set; }

        [Display(Name = "SubCategory")]
        public Enum.SubCategory3 subCategory3 { get; set; }

        public string paymentReference { get; set; }

        public string sessionID { get; set; }

        public string accountNumber { get; set; }

        public string date { get; set; }

        public decimal amount { get; set; }

        public string billerRef { get; set; }

        public string meterNumber { get; set; }

        public string assignedTo { get; set; }

        public string rating { get; set; }

        public Guid organizationId { get; set; }

        public Organization organization { get; set; }

    }
}
