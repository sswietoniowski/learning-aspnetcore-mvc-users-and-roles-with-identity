using learning_aspnetcore_mvc_users_and_roles_with_identity.DataAccess;
using learning_aspnetcore_mvc_users_and_roles_with_identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace learning_aspnetcore_mvc_users_and_logins.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly AppDbContext _dbContext;

        public CustomerController(ILogger<CustomerController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Orders()
        {
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var roleName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? throw new Exception("Role not found");
            var customer = _dbContext.Users.FirstOrDefault(u => u.UserName == userName) ?? throw new Exception("User not found");

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
}
