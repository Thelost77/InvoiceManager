using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InvoiceManager.Models.Domains
{
    public class InvoicePosition
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole Lp jest wymagane.")]
        public int Lp { get; set; }
        public int InvoiceId { get; set; }

        [Display(Name = "Wartość")]
        [Required(ErrorMessage = "Pole Wartość jest wymagane.")]
        public decimal Value { get; set; }

        [Display(Name = "Produkt")]
        [Required(ErrorMessage = "Pole Produkt jest wymagane.")]
        public int ProductId { get; set; }

        [Display(Name = "Ilość")]
        [Required(ErrorMessage = "Pole Ilość jest wymagane.")]
        public int Quantity { get; set; }



        public Invoice Invoice { get; set; }
        public Product Product { get; set; }
    }
}