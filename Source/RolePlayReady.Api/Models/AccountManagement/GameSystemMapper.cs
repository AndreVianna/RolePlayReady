using RolePlayReady.Security.Models;

namespace RolePlayReady.Api.Models.AccountManagement;

internal static class AccountManagementMapper {
    public static Login ToDomain(this LoginRequest request)
        => new() {
            Email = request.Email.Trim(),
            Password = request.Password.Trim(),
        };
}
