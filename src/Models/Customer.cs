using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace src.Models
{
    public class Customer : BaseEntity
    {
        public Customer()
        {
            this.thumbUrl = "/images/no-image-available.png";
            this.customerType = Enum.CustomerType.Internal;
        }
        public Guid customerId { get; set; }
        [Display(Name = "Customer Name")]
        [StringLength(100)]
        [Required]
        public string customerName { get; set; }
        [StringLength(200)]
        [Display(Name = "Description")]
        public string description { get; set; }
        [StringLength(255)]
        [Display(Name = "Thumb Url")]
        public string thumbUrl { get; set; }
        [Display(Name = "Customer Type")]
        public Enum.CustomerType customerType { get; set; }

        [Display(Name = "Email")]
        [StringLength(100)]
        public string email { get; set; }

        public Guid organizationId { get; set; }
        public Organization organization { get; set; }

        //contacts
        public ICollection<Contact> contacts { get; set; }
    }
}
