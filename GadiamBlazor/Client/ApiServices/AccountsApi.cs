using GadiamBlazor.Shared.Authentication;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace GadiamBlazor.Client.ApiServices
{
    public class AccountsApi : IAccountsApi
    {
        private readonly HttpClient httpClient;

        public AccountsApi(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<AccountModel?> GetAccountDetailsAsync()
        {
            return await httpClient.GetJsonAsync<AccountModel>("/api/accounts/details");
        }
    }
}