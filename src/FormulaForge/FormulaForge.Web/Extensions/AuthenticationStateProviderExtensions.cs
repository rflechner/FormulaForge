using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace FormulaForge.Web.Extensions;

public static class AuthenticationStateProviderExtensions
{
    extension(AuthenticationStateProvider provider)
    {
        public async Task<string?> GetUserIdAsync()
        {
            var authState = await provider.GetAuthenticationStateAsync();
            var user = authState.User;
            return user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? user.Identity?.Name;
        }
    }
}