using learning_aspnetcore_mvc_users_and_roles_with_identity.Entities;
using Microsoft.EntityFrameworkCore;

namespace learning_aspnetcore_mvc_users_and_roles_with_identity.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
    }
}
