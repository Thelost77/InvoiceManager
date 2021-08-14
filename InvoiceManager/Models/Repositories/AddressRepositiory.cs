using InvoiceManager.Models.Converters;
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

        public void Add(Address address)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Addresses.Add(address);
                context.SaveChanges();
            }
        }
        public void Edit(Address address)
        {
            using (var context = new ApplicationDbContext())
            {
                var addressToEdit = context.Addresses.Single(x => x.Id == address.Id);
                addressToEdit.Id = address.Id;
                addressToEdit.Street = address.Street;
                addressToEdit.Number = address.Number;
                addressToEdit.City = address.City;
                addressToEdit.PostalCode = address.PostalCode;
                context.SaveChanges();
            }
        }



    }
}