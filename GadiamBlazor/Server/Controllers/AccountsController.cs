using GadiamBlazor.Shared.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using IdentityUser = ElCamino.AspNetCore.Identity.AzureTable.Model.IdentityUser;

[Route("api/[controller]/[action]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly UserManager<IdentityUser> userManager;

    public AccountsController(UserManager<IdentityUser> userManager)
    {
        this.userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Details()
    {
        IdentityUser user = await userManager.GetUserAsync(HttpContext.User);
        if (user == null)
        {
            return Ok(new AccountModel());
        }

        return Ok(new AccountModel
        {
            UserName = user.UserName,
            Email = user.Email,
            EmailConfirmed = user.EmailConfirmed
        });
    }
}