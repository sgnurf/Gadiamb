using GadiamBlazor.Shared.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using Xunit;
using IdentityUser = ElCamino.AspNetCore.Identity.AzureTable.Model.IdentityUser;

namespace GadiamBlazor.Server.Tests.Controllers
{
    public class AccountsControllerTests
    {
        [Fact]
        public async void Details_UserManagerReturnsNullUser_ReturnsOkWithEmptyAccountModel()
        {
            AccountsController accountsController = MakeAccountsController(null);

            IActionResult actionResult = await accountsController.Details();

            Assert.IsType<OkObjectResult>(actionResult);
            AccountModel accountModel = (actionResult as OkObjectResult).Value as AccountModel;
            Assert.Null(accountModel.UserName);
        }

        [Fact]
        public async void Details_UserManagerReturnsUser_ReturnsOkWithMatchingAccountModel()
        {
            IdentityUser identityUser = new IdentityUser
            {
                Email = "email@email.com",
                UserName = "UserName",
                EmailConfirmed = true
            };

            AccountsController accountsController = MakeAccountsController(identityUser);

            IActionResult actionResult = await accountsController.Details();

            Assert.IsType<OkObjectResult>(actionResult);
            AccountModel accountModel = (actionResult as OkObjectResult).Value as AccountModel;
            Assert.Equal(identityUser.UserName, accountModel.UserName);
            Assert.Equal(identityUser.Email, accountModel.Email);
            Assert.Equal(identityUser.EmailConfirmed, accountModel.EmailConfirmed);
        }

        private static AccountsController MakeAccountsController(IdentityUser identityUserReturnedByUserManager)
        {
            var userStoreMock = new Mock<IUserStore<IdentityUser>>();
            var userManagerMock = new Mock<UserManager<IdentityUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

            userManagerMock.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync(identityUserReturnedByUserManager);
            AccountsController accountsController = new AccountsController(userManagerMock.Object);
            accountsController.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
            return accountsController;
        }
    }
}