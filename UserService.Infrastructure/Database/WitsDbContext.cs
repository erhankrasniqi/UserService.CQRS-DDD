using Microsoft.EntityFrameworkCore; 
using UserService.Domain.Aggregates.UsersAggregates;

namespace UserService.Infrastructure.Database
{
    public class WitsDbContext : DbContext
    {
        public DbSet<Users> Users { get; set; }  

        public WitsDbContext(DbContextOptions<WitsDbContext> options)
            : base(options)
        {
            //
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

             modelBuilder.Entity<Users>()
                .HasIndex(u => u.TenantId)
                .HasDatabaseName("IX_Users_TenantId");

            modelBuilder.Entity<Users>()
                .HasIndex(u => u.Username)
                .IsUnique()
                .HasDatabaseName("IX_Users_Username");

            modelBuilder.Entity<Users>()
                .HasIndex(u => u.AccountId) 
                .HasDatabaseName("IX_Users_AccountId");

                    }

    }
}
