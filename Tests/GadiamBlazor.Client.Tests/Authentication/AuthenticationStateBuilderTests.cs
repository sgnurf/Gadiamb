using GadiamBlazor.Client.ApiServices;
using GadiamBlazor.Client.Authentication;
using GadiamBlazor.Client.Extensions;
using GadiamBlazor.Shared.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Moq;
using Xunit;

namespace GadiamBlazor.Client.Tests.Authentication
{
    public class AuthenticationStateBuilderTests
    {
        private AccountModel VALID_ACCOUNT_MODEL = new AccountModel()
        {
            Email = "email@test.com",
            UserName = "testUserName"
        };

        [Fact]
        public async void GetAuthenticationStateAsync_ApiReturnsNull_ReturnsStateNotAuthenticated()
        {
            AuthenticationStateBuilder authenticationStateBuilder =
                new TestContext()
                .WithAccountModel(null)
                .BuildAuthenticationStateBuilder();

            AuthenticationState authenticationState = await authenticationStateBuilder.GetAuthenticationStateAsync();

            Assert.False(authenticationState.User.Identity.IsAuthenticated);
            Assert.Empty(authenticationState.User.Claims);
            Assert.Null(authenticationState.User.Identity.Name);
        }

        [Fact]
        public async void GetAuthenticationStateAsync_ApiReturnsValidAccount_ReturnsAutheticatedState()
        {
            AuthenticationStateBuilder authenticationStateBuilder =
                new TestContext()
                .WithAccountModel(VALID_ACCOUNT_MODEL)
                .BuildAuthenticationStateBuilder();

            AuthenticationState authenticationState = await authenticationStateBuilder.GetAuthenticationStateAsync();

            Assert.True(authenticationState.User.Identity.IsAuthenticated);
        }

        [Fact]
        public async void GetAuthenticationStateAsync_ApiReturnsValidAccount_AssignsAccountValuesToClaims()
        {
            AuthenticationStateBuilder authenticationStateBuilder =
                new TestContext()
                .WithAccountModel(VALID_ACCOUNT_MODEL)
                .BuildAuthenticationStateBuilder();

            AuthenticationState authenticationState = await authenticationStateBuilder.GetAuthenticationStateAsync();

            Assert.Equal(VALID_ACCOUNT_MODEL.UserName, authenticationState.User.Identity.Name);
            Assert.Equal(VALID_ACCOUNT_MODEL.Email, authenticationState.User.Email());
            Assert.Equal(VALID_ACCOUNT_MODEL.UserName, authenticationState.User.Name());
        }

        [Fact]
        public async void GetAuthenticationStateAsync_ApiReturnsValidAccount_ApiAuthenticationTypeSet()
        {
            AuthenticationStateBuilder authenticationStateBuilder =
                new TestContext()
                .WithAccountModel(VALID_ACCOUNT_MODEL)
                .BuildAuthenticationStateBuilder();

            AuthenticationState authenticationState = await authenticationStateBuilder.GetAuthenticationStateAsync();

            Assert.Equal("apiauth", authenticationState.User.Identity.AuthenticationType);
        }

        private class TestContext
        {
            private AccountModel? accountModel;

            public TestContext WithAccountModel(AccountModel? accountModel)
            {
                this.accountModel = accountModel;
                return this;
            }

            public AuthenticationStateBuilder BuildAuthenticationStateBuilder()
            {
                var accountsApiMock = new Mock<IAccountsApi>();
                accountsApiMock.Setup(m => m.GetAccountDetailsAsync())
                    .ReturnsAsync(accountModel);

                return new AuthenticationStateBuilder(accountsApiMock.Object);
            }
        }
    }
}