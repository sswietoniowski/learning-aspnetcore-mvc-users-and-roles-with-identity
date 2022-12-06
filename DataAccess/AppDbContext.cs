using learning_aspnetcore_mvc_users_and_roles_with_identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace learning_aspnetcore_mvc_users_and_roles_with_identity.DataAccess;

public class AppDbContext : IdentityDbContext<User, Role, int>
{
    public override DbSet<User> Users => Set<User>();
    public override DbSet<Role> Roles => Set<Role>();
    public DbSet<Order> Orders => Set<Order>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}