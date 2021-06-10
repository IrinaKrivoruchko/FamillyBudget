using Microsoft.EntityFrameworkCore;
using DataEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DataStorage
{
    public class DatabaseContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountStatement> AccountStatements { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminEmail = "admin@mail.ru";
            string adminPassword = "123456";

            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            User adminUser = new User { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

            builder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            builder.Entity<User>().HasData(new User[] { adminUser });
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable(nameof(User));
            builder.Entity<Role>().ToTable(nameof(Role));
            builder.Entity<Account>().ToTable(nameof(Account));
            builder.Entity<AccountStatement>().ToTable(nameof(AccountStatement));
            builder.Entity<Category>().ToTable(nameof(Category));
        }
    }
}
