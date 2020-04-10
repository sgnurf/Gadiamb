using System.Security.Claims;

namespace GadiamBlazor.Client.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string? Email(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.Email)?.Value;
        }

        public static string? Name(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}