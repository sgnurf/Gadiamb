using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace GadiamBlazor.Client.Authentication
{
    public interface IAuthenticationStateBuilder
    {
        Task<AuthenticationState> GetAuthenticationStateAsync();
    }
}