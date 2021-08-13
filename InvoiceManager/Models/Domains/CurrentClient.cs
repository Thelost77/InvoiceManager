using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InvoiceManager.Models.Domains
{
    public class CurrentClient
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public int AddressId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public Address Address { get; set; }
        public ApplicationUser User { get; set; }
    }
}