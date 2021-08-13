using InvoiceManager.Models.Converters;
using InvoiceManager.Models.Domains;
using Microsoft.Owin.BuilderProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Xml.Linq;

namespace InvoiceManager.Models.Repositories
{
    public class ClientRepository
    {
        public List<Client> GetClients(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Clients.Where(x => x.UserId == userId).ToList();                
            }
        }

        public Client GetClient(int clientId, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Clients.Single(x => x.Id == clientId && x.UserId == userId);
            }
        }
        public List<CurrentClient> GetCurrentClients(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.CurrentClients.Where(x => x.UserId == userId).ToList();

            }
        }
        public void Delete(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var clientToDelete = context.CurrentClients
                    .Single(x => x.Id == id && x.UserId == userId);
                context.CurrentClients.Remove(clientToDelete);

                context.SaveChanges();
            }
        }

        public void Add(Client client)
        {
            using (var context = new ApplicationDbContext())
            {
                context.CurrentClients.Add(client.ToCurrentClient());
                context.Clients.Add(client);
                context.SaveChanges();
            }
        }

        public void Edit(Client client)
        {
            using (var context = new ApplicationDbContext())
            {
                var currentClientToEdit = context.CurrentClients.Single(x => x.Id == client.Id && x.UserId == client.UserId);
                var clientToEdit = context.Clients.Single(x => x.Id == client.Id && x.UserId == client.UserId);
                clientToEdit.Id = client.Id;
                clientToEdit.Name = client.Name;
                clientToEdit.AddressId = client.AddressId;
                clientToEdit.Email = client.Email;
                currentClientToEdit = clientToEdit.ToCurrentClient();
                context.SaveChanges();
            }
        }
    }
}