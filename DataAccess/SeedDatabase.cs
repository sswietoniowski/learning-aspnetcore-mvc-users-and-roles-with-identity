using learning_aspnetcore_mvc_users_and_roles_with_identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace learning_aspnetcore_mvc_users_and_roles_with_identity.DataAccess;

public static class SeedDatabase
{
    private class RoleDto
    {
        public string Name { get; set; } = default!;
    }

    private class UserDto
    {
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Role { get; set; } = default!;
    }

    private static readonly List<RoleDto> Roles = new()
    {
        new RoleDto { Name = "Customer" },
        new RoleDto { Name = "Employee" },
        new RoleDto { Name = "Manager" }
    };

    private static readonly List<UserDto> Users = new()
    {
        new UserDto
        {
            UserName = "jdoe",
            Email = "john.doe@getnada.com",
            Password = "P@ssw0rd",
            Role = "Customer"
        },
        new UserDto
        {
            UserName = "afox",
            Email = "alice.fox@getnada.com",
            Password = "P@ssw0rd",
            Role = "Customer"
        }
    };

    private static readonly List<Order> Orders = new()
    {
        new Order
        {
            Product = "Product 1",
            Quantity = 1,
            Price = 1.0m,
            UserId = -1
        },
        new Order
        {
            Product = "Product 2",
            Quantity = 2,
            Price = 2.0m,
            UserId = -1
        }
    };

    public static async Task SeedAsync(this IApplicationBuilder app)
    {
        var context = app.ApplicationServices
            .CreateScope().ServiceProvider
            .GetRequiredService<AppDbContext>();

        if ((await context.Database.GetPendingMigrationsAsync()).Any())
        {
            await context.Database.MigrateAsync();
        }

        var userManager = app.ApplicationServices
            .CreateScope().ServiceProvider
            .GetRequiredService<UserManager<User>>();
        var roleManager = app.ApplicationServices
            .CreateScope().ServiceProvider
            .GetRequiredService<RoleManager<Role>>();

        await roleManager.SeedRolesAsync();
        await userManager.SeedUsersAsync();
        await context.SeedOrdersAsync();
    }

    private static async Task SeedUsersAsync(this UserManager<User> userManager)
    {
        async Task AddNewUser(UserDto userDto)
        {
            if (await userManager.FindByNameAsync(userDto.UserName) == null)
            {
                User newUser = new()
                {
                    UserName = userDto.UserName,
                    Email = userDto.Email
                };

                IdentityResult result = await userManager.CreateAsync(newUser, userDto.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newUser, userDto.Role);
                }
            }
        }

        foreach (var user in Users)
        {
            await AddNewUser(user);
        }
    }

    private static async Task SeedRolesAsync(this RoleManager<Role> roleManager)
    {
        async Task AddNewRole(RoleDto roleDto)
        {
            if (!await roleManager.RoleExistsAsync(roleDto.Name))
            {
                await roleManager.CreateAsync(new Role { Name = roleDto.Name });
            }
        }

        foreach (var role in Roles)
        {
            await AddNewRole(role);
        }
    }

    private static async Task SeedOrdersAsync(this AppDbContext context)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.UserName == "jdoe");

        if (user == null)
            return;

        if (context.Orders.Any())
            return;

        foreach (var order in Orders)
        {
            order.UserId = user.Id;

            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
        }
    }
}