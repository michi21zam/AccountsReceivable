using System.Data.Entity;

namespace AccountsReceivable.Models
{
    // Code First DbContext. On first run, EF will create the database
    // automatically from these models (see Web.config connection string).
    public class AccountsReceivableContext : DbContext
    {
        public AccountsReceivableContext()
            : base("name=AccountsReceivableContext")
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Receivable> Receivables { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Avoid multiple cascade delete paths between Customer/Employee -> Receivable
            modelBuilder.Entity<Receivable>()
                .HasRequired(r => r.Customer)
                .WithMany(c => c.Receivables)
                .HasForeignKey(r => r.CustomerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Receivable>()
                .HasRequired(r => r.Employee)
                .WithMany(e => e.Receivables)
                .HasForeignKey(r => r.EmployeeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Payment>()
                .HasRequired(p => p.Receivable)
                .WithMany(r => r.Payments)
                .HasForeignKey(p => p.ReceivableId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
