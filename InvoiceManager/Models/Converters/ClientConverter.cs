using InvoiceManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InvoiceManager.Models.Converters
{
    public static class ClientConverter
    {
        public static CurrentClient ToCurrentClient(this Client client)
        {
            return new CurrentClient
            {
                Id = client.Id,
                Name = client.Name,
                AddressId = client.AddressId,
                Email = client.Email,
                UserId = client.UserId
            };
        }
    }
}