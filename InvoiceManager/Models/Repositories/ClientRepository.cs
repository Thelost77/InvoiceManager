using InvoiceManager.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        public void Delete(int id, string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var clientToDelete = context.Clients
                    .Single(x => x.Id == id && x.UserId == userId);
                context.Clients.Remove(clientToDelete);

                context.SaveChanges();
            }
        }

        public void Add(Client client)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Clients.Add(client);
                context.SaveChanges();
            }
        }


    }
}