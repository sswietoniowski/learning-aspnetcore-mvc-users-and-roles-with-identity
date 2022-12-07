namespace learning_aspnetcore_mvc_users_and_roles_with_identity.Models;

public class OrderVM
{
    public string ProductName { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Total { get; set; }
    public string CustomerName { get; set; } = default!;
    public string RoleName { get; set; } = default!;
}