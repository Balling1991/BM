using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace BallingMaskiner.Models
{
    public class IntraDbContext : DbContext
    {
        public IntraDbContext() : base("name=IntraDbContext")
        {
            // Get the ObjectContext related to this DbContext
            ObjectContext objectContext = (this as IObjectContextAdapter).ObjectContext;

            // Sets the command timeout for all the commands
            objectContext.CommandTimeout = 120;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasMany(m => m.Contacts)
                .WithOptional()
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Customer>()
                .HasMany(x => x.Machines)
                .WithOptional()
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Customer>()
                .HasMany(x => x.Quotations)
                .WithOptional()
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Customer>()
                .HasMany(x => x.Visits)
                .WithOptional()
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Machine>()
                .HasMany(m => m.SpareParts)
                .WithOptional()
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<SparePart>()
                .HasRequired(x => x.FileData)
                .WithMany()
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Prospect>()
                .HasRequired(x => x.FileData)
                .WithMany()
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Quotation>()
                .HasRequired(x => x.FileData)
                .WithMany()
                .WillCascadeOnDelete(true);
        }

        public DbSet<SparePart> SpareParts { get; set; }
        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<Visits> Visits { get; set; }
        public DbSet<Manual> Manuals { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Prospect> Prospects { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<FileData> FileDatas { get; set; }
    }
}
