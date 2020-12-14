using Microsoft.EntityFrameworkCore;
using DataEntities;

namespace DataStorage
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Cash> Cashes { get; set; }
        public DbSet<Deposit> Deposits { get; set; }

        public DatabaseContext(DbContextOptions options)
            : base(options)
        { 
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable(nameof(User));
            builder.Entity<Wallet>().ToTable(nameof(Wallet));
            builder.Entity<Card>().ToTable(nameof(Card));
            builder.Entity<Cash>().ToTable(nameof(Cash));
            builder.Entity<Deposit>().ToTable(nameof(Deposit));
        }
    }
}
