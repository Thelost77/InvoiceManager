using InvoiceManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceManager.Models.ViewModels
{
    public class AddEditClientsViewModel
    {
        public Client Client { get; set; }
        public Address Address { get; set; }
        public string Heading { get; set; }
    }
}