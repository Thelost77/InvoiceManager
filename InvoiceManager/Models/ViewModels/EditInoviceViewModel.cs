using InvoiceManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceManager.Models.ViewModels
{
    public class EditInoviceViewModel
    {
        public Invoice Invoice { get; set; }
        public List<Client> Clients { get; set; }
        public List<MethodOfPayment> MethodOfPayments { get; set; }
        public string Heading { get; set; }
    }
}