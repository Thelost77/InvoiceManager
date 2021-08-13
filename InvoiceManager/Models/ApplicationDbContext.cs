using System.Data.Entity;
using System.Security.Cryptography.X509Certificates;
using InvoiceManager.Models.Domains;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InvoiceManager.Models
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoicePosition> InvoicePositions { get; set; }
        public DbSet<MethodOfPayment> MethodOfPayments { get; set; }
        public DbSet<Product> Products { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.Invoices)
                .WithRequired(x => x.User)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.Clients)
                .WithRequired(x => x.User)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

    }
}