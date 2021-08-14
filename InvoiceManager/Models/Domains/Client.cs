using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvoiceManager.Models.Domains
{
    public class Client
    {
        public Client()
        {
            Invoices = new Collection<Invoice>();
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Adres")]
        public int AddressId { get; set; }

        [Required]
        [Display(Name = "Adres Email")]
        public string Email { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }


        public Address Address { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        public ApplicationUser User { get; set; }
    }
}