using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using System;

namespace GadiamBlazor.Server.Tests.Controllers
{
    public class LoginControllerTests
    {

        private class TestContext
        {
            //private Mock<SignInManager<IdentityUser>> BuildSignInManager()
            //{
            //    var userManager = MockHelpers.MockUserManager<PocoUser>();
            //    var claimsManager = new Mock<IUserClaimsPrincipalFactory<IdentityUser>>();
            //    var identityOptions = new Mock<IOptions<IdentityOptions>>();
            //    identityOptions.Setup(a => a.Value).Returns(new IdentityOptions());
            //    var options = new Mock<IOptions<SecurityStampValidatorOptions>>();
            //    options.Setup(a => a.Value).Returns(new SecurityStampValidatorOptions { ValidationInterval = TimeSpan.Zero });
            //    var contextAccessor = new Mock<IHttpContextAccessor>();
            //    contextAccessor.Setup(a => a.HttpContext).Returns(new Mock<HttpContext>().Object);
            //    var signInManager = new Mock<SignInManager<IdentityUser>>(userManager.Object,
            //        contextAccessor.Object, claimsManager.Object, identityOptions.Object, null, new Mock<IAuthenticationSchemeProvider>().Object, new DefaultUserConfirmation<PocoUser>());

            //}

            //private static LoginController BuildLoginController()
            //{
            //    var userStoreMock = new Mock<IUserStore<IdentityUser>>();
            //    var userManagerMock = new Mock<UserManager<IdentityUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

            //    return new LoginController(userManagerMock.Object);
            //}
        }
    }
}