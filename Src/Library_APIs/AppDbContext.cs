using Library_APIs.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Library_APIs
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) {
        
        
        
        
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Book>(entity =>
            {
                   entity.Property(e => e.Title).HasMaxLength(150);

                   entity.Property(e=>e.ISBN).IsRequired();
                   entity.HasIndex(e=>e.ISBN).IsUnique();

                   


            });


            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.Email).IsRequired().HasMaxLength(200);

                entity.HasIndex(e=>e.Email).IsUnique();

                entity.Property(e=>e.IsActive).HasDefaultValue(true);

              


            });

            //modelBuilder.Entity<Loan>(entity =>
            //{
            //    entity.Property(e => e.BorrowedAt).HasDefaultValueSql("NOW()");

            //    entity.Property(e => e.DueDate).HasDefaultValueSql("NOW() + INTERVAL '7 days'"); ;






            //});

            modelBuilder.Entity<Member>().HasQueryFilter(m => m.IsActive == true);

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }

        public DbSet<Loan> Loans { get; set; }

        public DbSet<AuditLog> AuditLogs { get; set; }

    }
}
