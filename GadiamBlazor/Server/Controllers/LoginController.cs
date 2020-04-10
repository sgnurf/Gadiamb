using GadiamBlazor.Shared.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityUser = ElCamino.AspNetCore.Identity.AzureTable.Model.IdentityUser;

[Route("api/[controller]/[action]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly SignInManager<IdentityUser> signInManager;
    private readonly UserManager<IdentityUser> userManager;

    public LoginController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> LoginProviders()
    {
        return Ok((await signInManager.GetExternalAuthenticationSchemesAsync())
            .Select(s => new SigninProviderModel
            {
                Name = s.Name,
                DisplayName = s.DisplayName
            }));
    }

    [HttpPost]
    public IActionResult ExternalLogin([FromForm] string provider)
    {
        var redirectUrl = Url.Action("ExternalLoginCallback", "Login");
        var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return Challenge(properties, provider);
    }

    [HttpGet]
    public async Task<IActionResult?> ExternalLoginCallback(string? returnUrl = null, string? remoteError = null)
    {
        if (remoteError != null)
        {
            return Redirect("/login");
        }
        var info = await signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            return null;
        }

        var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
        if (result == null || result.IsNotAllowed)
        {
            return Redirect("/login");
        }

        if (result.Succeeded)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return Redirect("/");
        }

        return Redirect("/login/ExternalLoginRegistration");
    }

    [HttpPost]
    public async Task<IActionResult> ExternalLoginRegister(ExternalLoginConfirmationModel externalLoginConfirmationModel)
    {
        var info = await signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            throw new ApplicationException("Error loading external login information during confirmation.");
        }

        var user = new IdentityUser
        {
            UserName = externalLoginConfirmationModel.UserName,
            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user);
        if (result.Succeeded)
        {
            result = await userManager.AddLoginAsync(user, info);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: false);
                return Ok();
            }
        }
        return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> ExternalLoginDetails()
    {
        var info = await signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            throw new ApplicationException("Error loading external login information during confirmation.");
        }

        return Ok(new ExternalLoginConfirmationModel()
        {
            UserName = null,
            Provider = info.LoginProvider
        });
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return Redirect("/");
    }

    //Old JWT code
    //[HttpPost]
    //public async Task<IActionResult> Login([FromBody] LoginModel login)
    //{
    //    var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

    //    if (!result.Succeeded) return BadRequest(new LoginResult { Successful = false, Error = "Username and password are invalid." });

    //    var claims = new[]
    //    {
    //        new Claim(ClaimTypes.Name, login.Email)
    //    };

    //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
    //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    //    var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));

    //    var token = new JwtSecurityToken(
    //        _configuration["JwtIssuer"],
    //        _configuration["JwtAudience"],
    //        claims,
    //        expires: expiry,
    //        signingCredentials: creds
    //    );

    //    return Ok(new LoginResult { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
    //}
}