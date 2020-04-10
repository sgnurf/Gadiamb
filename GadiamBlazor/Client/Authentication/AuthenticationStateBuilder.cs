using GadiamBlazor.Client.ApiServices;
using GadiamBlazor.Shared.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GadiamBlazor.Client.Authentication
{
    public class AuthenticationStateBuilder : IAuthenticationStateBuilder
    {
        private readonly IAccountsApi accountsApi;

        public AuthenticationStateBuilder(IAccountsApi accountsApi)
        {
            this.accountsApi = accountsApi;
        }

        public async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            AccountModel? account = await accountsApi.GetAccountDetailsAsync();

            ClaimsIdentity identity =
                account?.UserName != null
                ? new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Email, account.Email),
                    new Claim(ClaimTypes.Name, account.UserName)
                    }, "apiauth")
                : new ClaimsIdentity();

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
    }
}