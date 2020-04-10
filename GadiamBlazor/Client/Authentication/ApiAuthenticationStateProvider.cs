using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GadiamBlazor.Client.Authentication
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthenticationStateBuilder authenticationStateBuilder;

        public ApiAuthenticationStateProvider(IAuthenticationStateBuilder authenticationStateBuilder)
        {
            this.authenticationStateBuilder = authenticationStateBuilder;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return authenticationStateBuilder.GetAuthenticationStateAsync();
        }

        public void RefreshCurrentUser()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void MarkUserAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}