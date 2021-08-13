using InvoiceManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceManager.Models.Repositories
{
    public class AddressRepositiory
    {
        public Address GetAddress(int addressId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Addresses.Single(x => x.Id == addressId);
            }
        }

        public List<Address> GetAddresses(int addressId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Addresses.ToList();
            }
        }

    }
}