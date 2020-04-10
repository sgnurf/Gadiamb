using GadiamBlazor.Shared.Authentication;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace GadiamBlazor.Client.ApiServices
{
    public class LoginApi : ILoginApi
    {
        private readonly HttpClient httpClient;

        public LoginApi(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task<ExternalLoginConfirmationModel> GetExternalLoginDetails()
        {
            return httpClient.GetJsonAsync<ExternalLoginConfirmationModel>("/api/login/externalLoginDetails");
        }

        public Task ExternalLoginRegister(ExternalLoginConfirmationModel externalLoginConfirmationModel)
        {
            return httpClient.SendJsonAsync(HttpMethod.Post, "/api/login/externalLoginRegister", externalLoginConfirmationModel);
        }
    }
}