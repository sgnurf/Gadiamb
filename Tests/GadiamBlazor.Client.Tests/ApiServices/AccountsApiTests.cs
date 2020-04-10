using GadiamBlazor.Client.ApiServices;
using GadiamBlazor.Shared.Authentication;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GadiamBlazor.Client.Tests.ApiServices
{
    public class AccountsApiTests
    {
        [Fact]
        public async void GetAuthenticationStateAsync_ReturnsAccountModel()
        {
            //TODO: Sample mocking for API Services. Not sure it's worth testing
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
               .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("{}") });

            HttpClient httpClient = new HttpClient(handlerMock.Object);
            httpClient.BaseAddress = new System.Uri("http://localhost");

            AccountsApi accountsApi = new AccountsApi(httpClient);

            AccountModel? accountModel = await accountsApi.GetAccountDetailsAsync();

            Assert.IsType<AccountModel>(accountModel);
        }
    }
}