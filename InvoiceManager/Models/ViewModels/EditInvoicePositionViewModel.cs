using InvoiceManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceManager.Models.ViewModels
{
    public class EditInvoicePositionViewModel
    {
        public InvoicePosition InvoicePosition { get; set; }
        public string Heading { get; set; }
        public List<Product> Products { get; set; }
    }
}