namespace learning_aspnetcore_mvc_users_and_roles_with_identity.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}