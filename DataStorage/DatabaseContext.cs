using Microsoft.EntityFrameworkCore;
using DataEntities;

namespace DataStorage
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountStatement> AccountStatements { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DatabaseContext(DbContextOptions options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable(nameof(User));
            builder.Entity<Account>().ToTable(nameof(Account));
            builder.Entity<AccountStatement>().ToTable(nameof(AccountStatement));
            builder.Entity<Category>().ToTable(nameof(Category));
        }
    }
}
