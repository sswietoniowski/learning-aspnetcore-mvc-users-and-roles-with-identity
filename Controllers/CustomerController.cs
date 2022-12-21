using System.Security.Claims;
using learning_aspnetcore_mvc_users_and_roles_with_identity.DataAccess;
using learning_aspnetcore_mvc_users_and_roles_with_identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace learning_aspnetcore_mvc_users_and_roles_with_identity.Controllers;

[Authorize(Roles = "Customer")]
public class CustomerController : Controller
{
    private readonly ILogger<CustomerController> _logger;
    private readonly AppDbContext _dbContext;

    public CustomerController(ILogger<CustomerController> logger, AppDbContext dbContext)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public IActionResult Orders()
    {
        var userName = User.FindFirstValue(ClaimTypes.Name);
        var customer = _dbContext.Users.FirstOrDefault(u => u.UserName == userName) 
                       ?? throw new Exception("User not found");
        var roleName = User.Claims.FirstOrDefault(c => c is {Type: ClaimTypes.Role, Value: "Customer"})?.Value 
                       ?? throw new Exception("Role not found");

        var orders = _dbContext.Orders
            .Where(o => o.UserId == customer.Id)
            .Include(o => o.User)
            .ToList()
            .Select(
                o => new OrderVM
                {
                    ProductName = o.Product,
                    Quantity = o.Quantity,
                    Price = o.Price,
                    Total = o.Quantity * o.Price,
                    CustomerName = userName,
                    RoleName = roleName
                });

        return View(orders);
    }
}