using financing_project.Models;
using Microsoft.EntityFrameworkCore;

namespace financing_project.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Financing> Financing { get; set; }
        public DbSet<Installment> Installment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Financings)
                .WithOne(e => e.Customer)
                .HasForeignKey(e => e.Cpf)
                .HasPrincipalKey(e => e.Cpf);

            //modelBuilder.Entity<Financing>()
            //    .HasMany(e => e.Installments)
            //    .WithOne(e => e.Financing)
            //    .HasForeignKey(e => e.Id)
            //    .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<Installment>()
                .HasOne(e => e.Financing)
                .WithMany(e => e.Installments)
                .HasForeignKey(e => e.FinancingId)
                .HasPrincipalKey(e => e.Id);
        }
    }
}